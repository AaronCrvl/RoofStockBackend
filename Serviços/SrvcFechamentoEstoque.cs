using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Repositorios;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcFechamentoEstoque
    {
        private readonly Repository<FechamentoEstoque> _fechamentoEstoqueRepository;

        public SrvcFechamentoEstoque(AppDbContext context)
        {
            _fechamentoEstoqueRepository = new Repository<FechamentoEstoque>(context);
        }

        public async Task<bool> CriarFechamentoEstoqueAsync(FechamentoEstoque fechamentoEstoque)
        {
            try
            {
                if (fechamentoEstoque == null) throw new ArgumentNullException(nameof(fechamentoEstoque));
                await _fechamentoEstoqueRepository.AddAsync(fechamentoEstoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar fechamento de estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<FechamentoEstoque> CarregarFechamentoEstoquePorIdAsync(long id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                return await _fechamentoEstoqueRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar fechamento de estoque: {ex.Message}");
                return null;
            }
        }

        public async Task<FechamentoEstoque> CarregarFechamentoPorEstoqueAsync(long idEstoque)
        {
            try
            {
                if (idEstoque <= 0) throw new ArgumentException("ID de estoque inválido.");
                var fechamentos = await _fechamentoEstoqueRepository.GetAllAsync();
                return fechamentos.FirstOrDefault(fe => fe.ID_ESTOQUE == idEstoque);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar fechamento por estoque: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AlterarFechamentoEstoqueAsync(FechamentoEstoque fechamentoEstoque)
        {
            try
            {
                if (fechamentoEstoque == null) throw new ArgumentNullException(nameof(fechamentoEstoque));
                await _fechamentoEstoqueRepository.UpdateAsync(fechamentoEstoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar fechamento de estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExcluirFechamentoEstoqueAsync(long id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                await _fechamentoEstoqueRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir fechamento de estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DesativarFechamentoEstoqueAsync(long id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var fechamentoEstoque = await _fechamentoEstoqueRepository.GetByIdAsync(id);
                if (fechamentoEstoque == null) return false;

                fechamentoEstoque.IN_ERRO = true; // Exemplo de alteração do status para erro
                await _fechamentoEstoqueRepository.UpdateAsync(fechamentoEstoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao desativar fechamento de estoque: {ex.Message}");
                return false;
            }
        }
    }
}
