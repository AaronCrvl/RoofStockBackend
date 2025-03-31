using Microsoft.EntityFrameworkCore;
using RoofStockBackend.Database.Dados.Objetos;
using System.Collections.Generic;

namespace RoofStockBackend.Contextos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<FechamentoEstoque> FechamentoEstoques { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<EstoqueProduto> EstoqueProdutos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<EstoqueProduto> EstoqueProduto { get; set; }
        public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }
    }
}