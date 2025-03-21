using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class MovimentacaoEstoqueService
    {
        private readonly AppDbContext _context;

        public MovimentacaoEstoqueService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CriarMovimentacaoAsync(MovimentacaoEstoque movimentacao)
        {
            try
            {
                if (movimentacao == null) throw new ArgumentNullException(nameof(movimentacao));

                _context.MovimentacoesEstoque.Add(movimentacao);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar movimentação: {ex.Message}");
                return false;
            }
        }

        public async Task<MovimentacaoEstoque> CarregarMovimentacaoPorIdAsync(long id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                return await _context.MovimentacoesEstoque.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar movimentação: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<MovimentacaoEstoque>> ListarMovimentacoesPorEstoqueAsync(long estoqueId)
        {
            try
            {
                if (estoqueId <= 0) throw new ArgumentException("ID de estoque inválido.");
                return await Task.FromResult(_context.MovimentacoesEstoque.Where(m => m.ID_ESTOQUE == estoqueId).ToList());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar movimentações: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AlterarMovimentacaoAsync(MovimentacaoEstoque movimentacao)
        {
            try
            {
                if (movimentacao == null) throw new ArgumentNullException(nameof(movimentacao));

                _context.MovimentacoesEstoque.Update(movimentacao);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar movimentação: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExcluirMovimentacaoAsync(long id)
        {
            try
            {
                var movimentacao = await _context.MovimentacoesEstoque.FindAsync(id);
                if (movimentacao == null) return false;

                _context.MovimentacoesEstoque.Remove(movimentacao);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir movimentação: {ex.Message}");
                return false;
            }
        }
    }
}
