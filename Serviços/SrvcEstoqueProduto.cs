using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Repositorios;
using FluentValidation;
using RoofStockBackend.Modelos.DTO.Produto;
using Microsoft.EntityFrameworkCore;
using RoofStockBackend.Database.Dados.Enums;
using System.Text.RegularExpressions;

namespace RoofStockBackend.Services
{
    public class SrvcEstoqueProduto
    {
        #region Propriedades Privadas
        private readonly AppDbContext _context;
        private readonly Repository<EstoqueProduto> _estoqueProdutoRepository;
        private readonly Repository<Produto> _produtoRepository;
        private readonly Repository<MovimentacaoEstoque> _movimentacaoEstoqueRepository;
        private readonly Repository<Marca> _marcaRepository;
        private readonly IValidator<Produto> _produtoValidator;
        #endregion

        #region Construtor
        public SrvcEstoqueProduto(AppDbContext context, IValidator<Produto> produtoValidator)
        {
            _context = context;
            _estoqueProdutoRepository = new Repository<EstoqueProduto>(context);
            _produtoRepository = new Repository<Produto>(context);
            _movimentacaoEstoqueRepository = new Repository<MovimentacaoEstoque>(context);
            _marcaRepository = new Repository<Marca>(context);
            _produtoValidator = produtoValidator;
        }
        #endregion

        #region Métodos Públicos
        public async Task<IEnumerable<ProdutoDto>> CarregarProdutosEstoqueAsync(int idEstoque)
        {
            try
            {
                if (idEstoque <= 0)
                    return new List<ProdutoDto>
                    { };

                var produtosAdicionadosAoEstoque = await _estoqueProdutoRepository.GetAllAsync();
                var produtoCadastrados = await _produtoRepository.GetAllAsync();
                var marcasCadastradas = await _marcaRepository.GetAllAsync();

                // Join Produto x Marca
                var produtosEmEstoque = produtoCadastrados.Join(marcasCadastradas,
                prodCad => prodCad.ID_MARCA,
                    marcaCad => marcaCad.ID_MARCA,
                    (prodCad, marcaCad) => new { prodCad, marcaCad });

                // Join EstoqueProduto x Produto
                var produtosInfo = produtosAdicionadosAoEstoque.Join(
                    produtosEmEstoque,
                    prodAdd => prodAdd.ID_PRODUTO,
                    prodCad => prodCad.prodCad.ID_PRODUTO,
                    (prodAdd, prodCad) => new { prodAdd, prodCad })
                    .Where(p => p.prodAdd.ID_ESTOQUE == idEstoque);

                return produtosInfo.Select(prod => new ProdutoDto
                {
                    idProduto = prod.prodCad.prodCad.ID_PRODUTO,
                    idEstoque = prod.prodAdd.ID_ESTOQUE,
                    nomeMarca = prod.prodCad.marcaCad.TX_NOME,
                    nomeProduto = prod.prodCad.prodCad.TX_NOME,
                    promocao = prod.prodCad.prodCad.IN_PROMOCAO,
                    valor = prod.prodCad.prodCad.VALOR,
                });
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Erro ao carregar produtos: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> CadastrarProdutoAsync(ProdutoCadastrarDto produtoASerCadastrado)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                var produtoBD = new Produto
                {
                    ID_MARCA = produtoASerCadastrado.idMarca,
                    TX_NOME = produtoASerCadastrado.nomeProduto,
                    IN_PROMOCAO = produtoASerCadastrado.promocao,
                    VALOR = produtoASerCadastrado.valor
                };

                await ValidarProduto(produtoBD);
                await _produtoRepository.AddAsync(produtoBD);

                await _estoqueProdutoRepository.AddAsync(new EstoqueProduto
                {
                    ID_PRODUTO = produtoASerCadastrado.idProduto,
                    ID_ESTOQUE = produtoASerCadastrado.idEstoque,
                    QN_ESTOQUE = produtoASerCadastrado.quantidade
                });

                await _movimentacaoEstoqueRepository.AddAsync(new MovimentacaoEstoque
                {
                    DT_MOVIMENTACAO = DateTime.Now,
                    ID_ESTOQUE = produtoASerCadastrado.idEstoque,
                    ID_USUARIO = 0,
                    TIPO_MOVIMENTACAO = (long)eTipoMovimentacao.Entrada,
                    IN_PROCESSADO = false
                });

                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao cadastrar produto: {ex.Message}");
                throw;
            }
        }

        public async Task<ProdutoDto> AlterarProdutoAsync(int productId, ProdutoAtualizarDto produtoASerAtualizado)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                var produtoBD = new Produto
                {
                    ID_MARCA = produtoASerAtualizado.idMarca,
                    TX_NOME = produtoASerAtualizado.nomeProduto,
                    IN_PROMOCAO = produtoASerAtualizado.promocao,
                    VALOR = produtoASerAtualizado.valor
                };

                await ValidarProduto(produtoBD);
                await _produtoRepository.UpdateAsync(produtoBD);

                var marca = await _marcaRepository.GetByIdAsync(produtoASerAtualizado.idMarca);
                await t.CommitAsync();
                return new ProdutoDto
                {
                    idProduto = produtoASerAtualizado.idProduto,
                    idEstoque = produtoASerAtualizado.idEstoque,
                    nomeMarca = marca.TX_NOME,
                    nomeProduto = produtoASerAtualizado.nomeProduto,
                    promocao = produtoASerAtualizado.promocao,
                    valor = produtoASerAtualizado.valor
                };
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao alterar produto: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ExcluirProdutoAsync(int idProduto)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                if (idProduto <= 0) return false;
                await _produtoRepository.DeleteAsync(idProduto);
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao deletar produto: {ex.Message}");
                throw;
            }
        }        
        #endregion

        #region Métodos Privados
        private async Task ValidarProduto(Produto produtoBD)
        {
            var validationResult = await _produtoValidator.ValidateAsync(produtoBD);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception($"Erro de validação: {errors}");
            }
        }
        #endregion
    }
}
