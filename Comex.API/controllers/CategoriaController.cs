using AutoMapper;
using ComexAPI.Data;
using ComexAPI.Data.Dtos;
using ComexAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ComexAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private ProdutoContext _context;
    private IMapper _mapper;

    public CategoriaController(ProdutoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona uma categoria ao banco de dados
    /// </summary>
    /// <param name="categoriaDto">Objeto com os campos necessários para criação de uma categoria</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarCategoria([FromBody] CreateCategoriaDto categoriaDto)
    {
        Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return CreatedAtAction(nameof(RecuperaCategoriaPorId), new { id = categoria.Id }, categoria);
    }

    /// <summary>
    /// Buscar todas as categorias do banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso encontre as categorias</response>
    [HttpGet]
    public IEnumerable<ReadCategoriaDto> RecuperaCategorias([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _context.Categorias
            .OrderBy(c => c.Id)
            .Skip(skip)
            .Take(take)
            .ProjectTo<ReadCategoriaDto>(_mapper.ConfigurationProvider)
            .ToList();
    }

    /// <summary>
    /// Buscar uma categoria por id do banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso encontre a categoria</response>
    /// <response code="404">Caso a categoria não seja encontrada</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadCategoriaDto))]
    public IActionResult RecuperaCategoriaPorId(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();
        var categoriaDto = _mapper.Map<ReadCategoriaDto>(categoria);
        return Ok(categoriaDto);
    }

    /// <summary>
    /// Atualizar uma categoria por id do banco de dados
    /// </summary>
    /// <param name="id">O ID da categoria a ser atualizada</param>
    /// <param name="UpdateCategoriaDto">Objeto com os campos necessários para atualização de uma categoria</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso atualização seja feita com sucesso</response>
    /// <response code="404">Caso a categoria não seja encontrada</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizarCategoria(int id, [FromBody] UpdateCategoriaDto categoriaDto)
    {
        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();
        _mapper.Map(categoriaDto, categoria);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deletar uma categoria por id do banco de dados
    /// </summary>
    /// <param name="id">O ID da categoria a ser deletada</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso deleção seja feita com sucesso</response>
    /// <response code="404">Caso a categoria não seja encontrada</response>
    /// <response code="400">Caso a categoria não possa ser deletada porque está em uso</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeletaCategoria(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
        if (categoria == null) return NotFound();

        try
        {
            _context.Remove(categoria);
            _context.SaveChanges();
            return NoContent();
        }
        catch (DbUpdateException ex)
        {

            if (ex.InnerException != null && ex.InnerException.Message.Contains("FK_"))
            {
                return BadRequest("Não é possível deletar a categoria porque ela está em uso.");
            }
            throw;
        }
    }

}
