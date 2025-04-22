using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcEstoqueProduto
    {
        private readonly Repository<EstoqueProduto> _estoqueProdutoRepository;

        public SrvcEstoqueProduto(AppDbContext context)
        {
            _estoqueProdutoRepository = new Repository<EstoqueProduto>(context);
        }

        public async Task<bool> AdicionarEstoqueProdutoAsync(EstoqueProduto estoqueProduto)
        {
            try
            {
                if (estoqueProduto == null) throw new ArgumentNullException(nameof(estoqueProduto));
                await _estoqueProdutoRepository.AddAsync(estoqueProduto);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar produto ao estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<EstoqueProduto> CarregarEstoqueProdutoPorIdAsync(int estoqueId, int produtoId)
        {
            try
            {
                if (estoqueId <= 0 || produtoId <= 0)
                    throw new ArgumentException("IDs inválidos.");

                //return await _estoqueProdutoRepository.GetByIdAsync(estoqueId, produtoId);
                return await _estoqueProdutoRepository.GetByIdAsync(estoqueId); // TODOOOOOOOOOOOOO
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar produto do estoque: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<EstoqueProduto>> ListarEstoqueProdutosPorEstoqueAsync(int estoqueId)
        {
            try
            {
                if (estoqueId <= 0) throw new ArgumentException("ID do estoque inválido.");

                var estoqueProdutos = await _estoqueProdutoRepository.GetAllAsync();
                return estoqueProdutos.Where(ep => ep.ID_ESTOQUE == estoqueId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar produtos do estoque: {ex.Message}");
                return Enumerable.Empty<EstoqueProduto>();
            }
        }

        public async Task<bool> AlterarEstoqueProdutoAsync(EstoqueProduto estoqueProduto)
        {
            try
            {
                if (estoqueProduto == null) throw new ArgumentNullException(nameof(estoqueProduto));
                await _estoqueProdutoRepository.UpdateAsync(estoqueProduto);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar produto no estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExcluirEstoqueProdutoAsync(int estoqueId, int produtoId)
        {
            try
            {
                if (estoqueId <= 0 || produtoId <= 0) throw new ArgumentException("IDs inválidos.");
                await _estoqueProdutoRepository.DeleteAsync(estoqueId, produtoId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir produto do estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DesativarEstoqueProdutoAsync(int estoqueId, int produtoId)
        {
            try
            {
                if (estoqueId <= 0 || produtoId <= 0) throw new ArgumentException("IDs inválidos.");
                //var estoqueProduto = await _estoqueProdutoRepository.GetByIdAsync(estoqueId, produtoId);
                var estoqueProduto = await _estoqueProdutoRepository.GetByIdAsync(estoqueId);
                if (estoqueProduto == null) return false;

                estoqueProduto.QN_ESTOQUE = 0; // Definindo estoque para zero, por exemplo, como desativado
                await _estoqueProdutoRepository.UpdateAsync(estoqueProduto);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao desativar produto do estoque: {ex.Message}");
                return false;
            }
        }
    }
}
