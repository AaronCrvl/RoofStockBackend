using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Repositorios;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcEmpresa
    {
        private readonly Repository<Empresa> _empresaRepository;

        public SrvcEmpresa(AppDbContext context)
        {
            _empresaRepository = new Repository<Empresa>(context);
        }

        public async Task<bool> CriarEmpresaAsync(Empresa empresa)
        {
            try
            {
                if (empresa == null) throw new ArgumentNullException(nameof(empresa));
                await _empresaRepository.AddAsync(empresa);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<Empresa> CarregarEmpresaPorIdAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                return await _empresaRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar empresa: {ex.Message}");
                return null;
            }
        }

        public async Task<Empresa> CarregarEmpresaPorNomeAsync(string nomeEmpresa)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomeEmpresa)) throw new ArgumentException("Nome inválido.");
                var empresas = await _empresaRepository.GetAllAsync();
                return empresas.FirstOrDefault(e => e.TX_RAZAO_SOCIAL == nomeEmpresa);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar empresa por nome: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AlterarEmpresaAsync(Empresa empresa)
        {
            try
            {
                if (empresa == null) throw new ArgumentNullException(nameof(empresa));
                await _empresaRepository.UpdateAsync(empresa);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExcluirEmpresaAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                await _empresaRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DesativarEmpresaAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var empresa = await _empresaRepository.GetByIdAsync(id);
                if (empresa == null) return false;

                empresa.IN_ATIVO = false;
                await _empresaRepository.UpdateAsync(empresa);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao desativar empresa: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AtivarEmpresaAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var empresa = await _empresaRepository.GetByIdAsync(id);
                if (empresa == null) return false;

                empresa.IN_ATIVO = true;
                await _empresaRepository.UpdateAsync(empresa);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ativar empresa: {ex.Message}");
                return false;
            }
        }
    }
}
