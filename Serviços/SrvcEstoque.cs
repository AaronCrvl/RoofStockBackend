using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Estoque;
using RoofStockBackend.Modelos.DTO.Produto;
using RoofStockBackend.Repositorios;
using System.Runtime.InteropServices;
namespace RoofStockBackend.Services
{
    public class SrvcEstoque
    {
        #region Propriedades Privadas
        private readonly AppDbContext _context;
        private readonly Repository<Estoque> _estoqueRepository;
        private readonly Repository<EstoqueUsuario> _estoqueUsuarioRepository;
        private readonly Repository<Produto> _produtoRepository;
        private readonly Repository<EstoqueProduto> _estoqueProdutoRepository;
        private readonly Repository<Marca> _marcaRepository;
        private readonly SrvcEstoqueProduto _srvcEstoqueProduto;
        private readonly IValidator<Estoque> _estoqueValidator;
        #endregion        

        #region Construtor
        public SrvcEstoque(AppDbContext context, IValidator<Estoque> estoqueValidator, SrvcEstoqueProduto srvcEstoqueProduto)
        {
            _context = context;
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
                    .Where(estoque => estoque.estoquesAtivosU.ID_USUARIO == idUsuario && estoque.estoquesAtivosU.IN_ATIVO); 

                var listaProdutosEmEstoque = new List<ProdutoDto>();
                foreach (var estoque in estoquesAtivosUsuario)
                {
                    var produtos = await _srvcEstoqueProduto.CarregarProdutosEstoqueAsync(estoque.ID_ESTOQUE);
                    foreach (ProdutoDto prod in produtos)
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
            using var t = await _context.Database.BeginTransactionAsync();
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
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao criar estoque: {ex.Message}");
                throw;
            }
        }

        public async Task<EstoqueDto> CarregarEstoquePorIdAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var estoqueBD = await _estoqueRepository.GetByIdAsync(id);
                var produtosBD = await _produtoRepository.GetAllAsync();
                var produtosEstoque = (await _estoqueProdutoRepository.GetAllAsync()).Where(p => p.ID_ESTOQUE == id);

                var produtosRetorno = produtosBD.Join(
                    produtosEstoque,
                    prodBD => prodBD.ID_PRODUTO,
                    produtosEst => produtosEst.ID_PRODUTO,
                    (produtosBD, prodEstq) => new { produtosBD, prodEstq });              

                return new EstoqueDto
                {
                    idEstoque = estoqueBD.ID_ESTOQUE,
                    ativo = estoqueBD.IN_ATIVO,
                    nomeEstoque = estoqueBD.TX_NOME,
                    nomeResponsavel = estoqueBD.ID_RESPONSAVEL.ToString(),
                    overviewDiario =
                    {
                        produtosEmEstoque = produtosRetorno.Count(),
                        entradasHoje = 0,
                        vencimentosProximos = 0,
                        promocoesAtivas = produtosRetorno.Where(p => p.produtosBD.IN_PROMOCAO).Count()
                    }
                };
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Erro ao carregar estoque: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AlterarEstoqueAsync(EstoqueAtualizarDto estoqueASerAtualizado)
        {
            using var t = await _context.Database.BeginTransactionAsync();
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
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao alterar estoque: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ExcluirEstoqueAsync(int id)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                await _estoqueRepository.DeleteAsync(id);
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao excluir estoque: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AtivarEstoqueAsync(int id)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var estoque = await _estoqueRepository.GetByIdAsync(id);
                if (estoque == null) return false;

                estoque.IN_ATIVO = true;
                await _estoqueRepository.UpdateAsync(estoque);
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao ativar estoque: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DesativarEstoqueAsync(int id)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                var estoque = await _estoqueRepository.GetByIdAsync(id);
                if (estoque == null) return false;

                estoque.IN_ATIVO = false;
                await _estoqueRepository.UpdateAsync(estoque);
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao desativar estoque: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region Métodos Privados
        private async Task ValidarEstoque(Estoque estoqueBD)
        {
            var validationResult = await _estoqueValidator.ValidateAsync(estoqueBD);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new InvalidDataException($"Erro de validação: {errors}");
            }
        }
        #endregion
    }
}