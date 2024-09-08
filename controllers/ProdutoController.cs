using AutoMapper;
using ComexAPI.Data;
using ComexAPI.Data.Dtos;
using ComexAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
    /// <param name="produtoDto">Objeto com os campos necessários para criação de um filme</param>
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

    [HttpGet]
    public IEnumerable<ReadProdutoDto> RecuperaProdutos([FromQuery] int skip = 0,
    [FromQuery] int take = 50)
    {
        return _mapper.Map<List<ReadProdutoDto>>(_context.Produtos.Skip(skip).Take(take));
    }

    [HttpGet("{id}")]
    public IActionResult RecuperaProdutoPorId(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
        if (produto == null) return NotFound();
        var filmeDto = _mapper.Map<ReadProdutoDto>(produto);
        return Ok(filmeDto);
    }

    [HttpPut("{id}")]
    public IActionResult AtualizarProduto(int id, [FromBody] UpdateProdutoDto filmeDto)
    {
        var produto = _context.Produtos.FirstOrDefault(filme => filme.Id == id);
        if (produto == null) return NotFound();
        _mapper.Map(filmeDto, produto);
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeletaProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(
            filme => filme.Id == id);
        if (produto == null) return NotFound();
        _context.Remove(produto);
        _context.SaveChanges();
        return NoContent();
    }
}
