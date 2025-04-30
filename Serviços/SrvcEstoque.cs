using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Estoque;
using RoofStockBackend.Repositorios;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcEstoque
    {
        private readonly Repository<Estoque> _estoqueRepository;
        private readonly Repository<EstoqueUsuario> _estoqueUsuarioRepository;

        #region Construtor
        public SrvcEstoque(AppDbContext context)
        {
            _estoqueRepository = new Repository<Estoque>(context);
            _estoqueUsuarioRepository = new Repository<EstoqueUsuario>(context);
        }
        #endregion

        #region Métodos Públicos

        #region Métodos EstoqueUsuario
        async Task<IEnumerable<EstoqueUsuario>> RetornarEstoquesVisiveisAtivosPorUsuario(int idUsuario)
        {
            var estoquesAtivosUsuario = await _estoqueUsuarioRepository.GetAllAsync();
            return estoquesAtivosUsuario.Where(estoque => estoque.ID_USUARIO == idUsuario && estoque.IN_ATIVO);
        }
        #endregion

        #region Métodos Estoque
        public async Task<IEnumerable<EstoqueDto>> CarregarEstoquePorUsuario(int idUsuario, int idEmpresa)
        {
            try
            {
                if (idUsuario <= 0)
                    return new List<EstoqueDto> { };

                var estoques = await _estoqueRepository.GetAllAsync();
                var estoquesAtivosUsuario = await RetornarEstoquesVisiveisAtivosPorUsuario(idUsuario);

                if (estoques.Count() == 0 || estoquesAtivosUsuario.Count() == 0)
                    return new List<EstoqueDto> { };

                var estoquesRetorno = estoques.Join(
                    estoquesAtivosUsuario,
                    estoquesBD => estoquesBD.ID_ESTOQUE,
                    estoquesAtivosU => estoquesAtivosU.ID_ESTOQUE,
                    (estoquesBD, estoquesAtivosU) => new { estoquesBD, estoquesAtivosU });

                var listaEstoques = new List<EstoqueDto>();
                foreach (var estq in estoquesRetorno)
                    listaEstoques.Add(new EstoqueDto
                    {
                        idEstoque = estq.estoquesBD.ID_ESTOQUE,
                        ativo = estq.estoquesBD.IN_ATIVO,
                        nomeResponsavel = "",
                        nomeEstoque = estq.estoquesBD.TX_NOME
                    });

                return listaEstoques;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> CriarEstoqueAsync(Estoque estoque)
        {
            try
            {
                if (estoque == null) throw new ArgumentNullException(nameof(estoque));
                await _estoqueRepository.AddAsync(estoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<Estoque> CarregarEstoquePorIdAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                return await _estoqueRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar estoque: {ex.Message}");
                return null;
            }
        }

        public async Task<Estoque> CarregarEstoquePorNomeAsync(string nomeEstoque)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nomeEstoque)) throw new ArgumentException("Nome do estoque inválido.");
                var estoques = await _estoqueRepository.GetAllAsync();
                return estoques.FirstOrDefault(e => e.TX_NOME.Equals(nomeEstoque, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar estoque por nome: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> AlterarEstoqueAsync(Estoque estoque)
        {
            try
            {
                if (estoque == null) throw new ArgumentNullException(nameof(estoque));
                await _estoqueRepository.UpdateAsync(estoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao alterar estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExcluirEstoqueAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                await _estoqueRepository.DeleteAsync(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao excluir estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> AtivarEstoqueAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var estoque = await _estoqueRepository.GetByIdAsync(id);
                if (estoque == null) return false;

                estoque.IN_ATIVO = true;
                await _estoqueRepository.UpdateAsync(estoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ativar estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DesativarEstoqueAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var estoque = await _estoqueRepository.GetByIdAsync(id);
                if (estoque == null) return false;

                estoque.IN_ATIVO = false;
                await _estoqueRepository.UpdateAsync(estoque);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao desativar estoque: {ex.Message}");
                return false;
            }
        }
        #endregion

        #endregion
    }
}