
using ComexAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ComexAPI.Data;

public class ProdutoContext : DbContext
{
    public ProdutoContext(DbContextOptions<ProdutoContext> opts) : base(opts)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Endereco>()
            .HasOne(endereco => endereco.Cliente)
            .WithOne(cinema => cinema.Endereco)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Categoria>()
            .HasMany(categoria => categoria.Produtos)
            .WithOne(produto => produto.Categoria)
            .HasForeignKey(produto => produto.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
}