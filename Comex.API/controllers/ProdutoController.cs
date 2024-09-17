using AutoMapper;
using ComexAPI.Data;
using ComexAPI.Data.Dtos;
using ComexAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper.QueryableExtensions;  

namespace ComexAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    private ProdutoContext _context;
    private IMapper _mapper;

    public ProdutoController(ProdutoContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Adiciona um produto ao banco de dados
    /// </summary>
    /// <param name="produtoDto">Objeto com os campos necessários para criação de um produto</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public IActionResult AdicionarProduto([FromBody] CreateProdutoDto produtoDto)
    {
        Produto produto = _mapper.Map<Produto>(produtoDto);
        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return CreatedAtAction(nameof(RecuperaProdutoPorId), new { id = produto.Id }, produto);
    }

    /// <summary>
    /// Buscar todos os produtos do banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso encontre os produtos</response>

    [HttpGet]
    public IEnumerable<ReadProdutoDto> RecuperaProdutos([FromQuery] int skip = 0, [FromQuery] int take = 50)
    {
        return _context.Produtos
            .OrderBy(p => p.Id)
            .Skip(skip)
            .Take(take)
            .ProjectTo<ReadProdutoDto>(_mapper.ConfigurationProvider)
            .ToList();
    }


    /// <summary>
    /// Buscar um produto por id do banco de dados
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso encontre os produtos</response>
    /// <response code="404">Caso o produto não seja encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReadProdutoDto))]
    public IActionResult RecuperaProdutoPorId(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null) return NotFound();
        var filmeDto = _mapper.Map<ReadProdutoDto>(produto);
        return Ok(filmeDto);
    }

    /// <summary>
    /// Atualizar um produto por id do banco de dados
    /// </summary>
    /// <param name="id">O ID do produto a ser atualizado</param>
    /// <param name="UpdateProdutoDto">Objeto com os campos necessários para atualização de um produto</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso atualização seja feita com sucesso</response>
    /// <response code="404">Caso o produto não seja encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult AtualizarProduto(int id, [FromBody] UpdateProdutoDto filmeDto)
    {
        var produto = _context.Produtos.FirstOrDefault(filme => filme.Id == id);
        if (produto == null) return NotFound();
        _mapper.Map(filmeDto, produto);
        _context.SaveChanges();
        return NoContent();
    }

    /// <summary>
    /// Deletar um produto por id do banco de dados
    /// </summary>
    /// <param name="id">O ID do produto a ser deletado</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso deleção seja feita com sucesso</response>
    /// <response code="404">Caso o produto não seja encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletaProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(filme => filme.Id == id);
        if (produto == null) return NotFound();
        _context.Remove(produto);
        _context.SaveChanges();
        return NoContent();
    }

}
