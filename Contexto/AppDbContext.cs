using Microsoft.EntityFrameworkCore;
using RoofStockBackend.Database.Dados.Objetos;
using System.Collections.Generic;

namespace RoofStockBackend.Contextos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Console.WriteLine(options);
        }

        public DbSet<FechamentoEstoque> FechamentoEstoques { get; set; }
        public DbSet<ItemFechamentoEstoque> ItemFechamentoEstoque { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<EstoqueProduto> EstoqueProdutos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<EstoqueProduto> EstoqueProduto { get; set; }
        public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }
        public DbSet<ItemMovimentacaoEstoque> ItemMovimentacaoEstoque { get; set; }

        public DbSet<EstoqueUsuario> EstoqueUsuario { get; set; }
    }
}