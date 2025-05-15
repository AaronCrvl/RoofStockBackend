using FluentValidation;
using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Estoque;
using RoofStockBackend.Modelos.DTO.Produto;
using RoofStockBackend.Repositorios;
namespace RoofStockBackend.Services
{
    public class SrvcEstoque
    {
        private readonly Repository<Estoque> _estoqueRepository;
        private readonly Repository<EstoqueUsuario> _estoqueUsuarioRepository;
        private readonly Repository<Produto> _produtoRepository;
        private readonly Repository<EstoqueProduto> _estoqueProdutoRepository;
        private readonly Repository<Marca> _marcaRepository;
        private readonly SrvcEstoqueProduto _srvcEstoqueProduto;
        private readonly IValidator<Estoque> _estoqueValidator;

        #region Construtor
        public SrvcEstoque(AppDbContext context, IValidator<Estoque> estoqueValidator, SrvcEstoqueProduto srvcEstoqueProduto) 
        {
            _estoqueRepository = new Repository<Estoque>(context);
            _estoqueUsuarioRepository = new Repository<EstoqueUsuario>(context);
            _produtoRepository = new Repository<Produto>(context);
            _estoqueProdutoRepository = new Repository<EstoqueProduto>(context);
            _marcaRepository = new Repository<Marca>(context);
            _estoqueValidator = estoqueValidator;
            _srvcEstoqueProduto = srvcEstoqueProduto;
        }
        #endregion

        #region Métodos Públicos

        #region Métodos Estoque
        public async Task<IEnumerable<EstoqueDto>> CarregarEstoquePorUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                    return new List<EstoqueDto> { };

                var estoques = await _estoqueRepository.GetAllAsync();
                var estoquesAtivosUsuario = await _estoqueUsuarioRepository.GetAllAsync();
                if (estoques.Count() == 0 || estoquesAtivosUsuario.Count() == 0)
                    return new List<EstoqueDto> { };

                var estoquesRetorno = estoques.Join(
                    estoquesAtivosUsuario,
                    estoquesBD => estoquesBD.ID_ESTOQUE,
                    estoquesAtivosU => estoquesAtivosU.ID_ESTOQUE,
                    (estoquesBD, estoquesAtivosU) => new { estoquesBD, estoquesAtivosU })
                    .Where(estoque => estoque.estoquesAtivosU.ID_USUARIO == idUsuario && estoque.estoquesAtivosU.IN_ATIVO); ;

                var listaProdutosEmEstoque = new List<ProdutoDto>();
                foreach (var estoque in estoquesAtivosUsuario)
                {
                    var produtos = await _srvcEstoqueProduto.CarregarProdutosEstoqueAsync(estoque.ID_ESTOQUE);
                    foreach(ProdutoDto prod in produtos)
                        listaProdutosEmEstoque.Add(prod);
                }

                return estoquesRetorno.Select(estq => new EstoqueDto
                {
                    idEstoque = estq.estoquesBD.ID_ESTOQUE,
                    ativo = estq.estoquesBD.IN_ATIVO,
                    nomeResponsavel = "",
                    nomeEstoque = estq.estoquesBD.TX_NOME,
                    overviewDiario =
                    {
                        produtosEmEstoque = listaProdutosEmEstoque.Where(prod => prod.idEstoque == estq.estoquesBD.ID_ESTOQUE).Count(),
                        entradasHoje = 0,
                        promocoesAtivas = listaProdutosEmEstoque.Where(prod => prod.idEstoque == estq.estoquesBD.ID_ESTOQUE && prod.promocao).Count(),
                        vencimentosProximos= 0,
                    }
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<bool> CriarEstoqueAsync(EstoqueCadastrarDto estoqueASerCadastrado)
        {
            try
            {
                var estoqueBD = new Estoque
                {
                    ID_EMPRESA = 0,
                    ID_RESPONSAVEL = estoqueASerCadastrado.idResponsavel,
                    TX_NOME = estoqueASerCadastrado.nomeEstoque,
                    IN_ATIVO = true
                };

                await ValidarEstoque(estoqueBD);
                await _estoqueRepository.AddAsync(estoqueBD);

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

        public async Task<bool> AlterarEstoqueAsync(EstoqueAtualizarDto estoqueASerAtualizado)
        {
            try
            {
                var estoqueBD = new Estoque
                {
                    ID_EMPRESA = 0,
                    ID_RESPONSAVEL = estoqueASerAtualizado.idResponsavel,
                    TX_NOME = estoqueASerAtualizado.nomeEstoque,
                    IN_ATIVO = true,                                        
                };

                await ValidarEstoque(estoqueBD);
                await _estoqueRepository.UpdateAsync(estoqueBD);
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

        private async Task ValidarEstoque(Estoque estoqueBD)
        {
            var validationResult = await _estoqueValidator.ValidateAsync(estoqueBD);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception($"Erro de validação: {errors}");
            }
        }
        #endregion   

        #endregion
    }
}