using FluentValidation;
using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Movimentação;
using RoofStockBackend.Modelos.DTO.Movimentação_Estoque;
using RoofStockBackend.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcMovimentacaoEstoque
    {
        #region Propriedades Privadas 
        private readonly AppDbContext _context;
        private readonly Repository<MovimentacaoEstoque> _movimentacaoEstoqueRepository;
        private readonly Repository<ItemMovimentacaoEstoque> _itemMovimentacaoEstoqueRepository;
        private readonly Repository<Produto> _produtoRepository;
        private readonly IValidator<MovimentacaoEstoque> _movimentacaoEstoqueValidar;
        private readonly IValidator<ItemMovimentacaoEstoque> _itemMovimentacaoEstoqueValidar;
        #endregion

        #region Construtor
        public SrvcMovimentacaoEstoque(AppDbContext context, IValidator<MovimentacaoEstoque> movimentacaoEstoqueValidar, IValidator<ItemMovimentacaoEstoque> itemMovimentacaoEstoqueValidar)
        {
            _context = context;
            _movimentacaoEstoqueRepository = new Repository<MovimentacaoEstoque>(context);
            _itemMovimentacaoEstoqueRepository = new Repository<ItemMovimentacaoEstoque>(context);
            _produtoRepository = new Repository<Produto>(context);
            _movimentacaoEstoqueValidar = movimentacaoEstoqueValidar;
            _itemMovimentacaoEstoqueValidar = itemMovimentacaoEstoqueValidar;
        }
        #endregion

        #region Métodos Públicos
        public async Task<bool> CriarMovimentacaoAsync(MovimentacaoEstoqueCriarDto movimentacao)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                var movimentacaoEstoqueBD = MovimentacaoEstoque.ConvertDtoToBDObject(movimentacao);
                await ValidarMovimentacaoEstoque(movimentacaoEstoqueBD);
                _context.MovimentacoesEstoque.Add(movimentacaoEstoqueBD);

                var listItemMovimentacaoBD = new List<ItemMovimentacaoEstoque>();
                foreach (ItemMovimentacaoEstoqueDto item in movimentacao.itens)
                {
                    var itemMov = ItemMovimentacaoEstoque.ConvertDtoToBDObject(item);
                    await ValidarItemMovimentacaoEstoque(itemMov);
                    listItemMovimentacaoBD.Add(itemMov);
                }

                foreach (ItemMovimentacaoEstoque item in listItemMovimentacaoBD)
                    _context.ItemMovimentacaoEstoque.Add(item);

                await _context.SaveChangesAsync();
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao criar movimentação: {ex.Message}");
                throw;
            }
        }

        public async Task<MovimentacaoEstoqueDto> CarregarMovimentacaoPorIdAsync(int id)
        {
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");

                var movimentacaoBD = await _movimentacaoEstoqueRepository.GetByIdAsync(id);
                var itensMovimentacaoBD = (await _itemMovimentacaoEstoqueRepository.GetAllAsync()).Where(item => item.ID_MOVIMENTACAO == id);
                var produtosBD = await _produtoRepository.GetAllAsync();

                var produtosRetorno = itensMovimentacaoBD.Join(
                    produtosBD,
                    itensMov => itensMov.ID_PRODUTO,
                    prodBD => prodBD.ID_PRODUTO,
                    (itensMov, prodBD) => new { itensMov, prodBD }
                    );

                return new MovimentacaoEstoqueDto
                {
                    idMovimentacao = movimentacaoBD.ID_MOVIMENTACAO,
                    dataMovimentacao = movimentacaoBD.DT_MOVIMENTACAO,
                    idEstoque = movimentacaoBD.ID_ESTOQUE,
                    idUsuario = movimentacaoBD.ID_USUARIO,
                    processado = movimentacaoBD.IN_PROCESSADO,
                    tipoMovimentacao = movimentacaoBD.TIPO_MOVIMENTACAO,
                    itens = produtosRetorno.Select(item => new ItemMovimentacaoEstoqueDto
                    {
                        idMovimentacao = item.itensMov.ID_MOVIMENTACAO,
                        idItemMovimentacao = item.itensMov.ID_ITEM_MOVIMENTACAO,
                        idProduto = item.itensMov.ID_PRODUTO,
                        nomeProduto = item.prodBD.TX_NOME,
                        quantidadeMovimentacao = item.itensMov.QN_MOVIMENTACAO,
                        cortesias = item.itensMov.CORTESIAS,
                        quebras = item.itensMov.QUEBRAS
                    }).ToList(),
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar movimentação de estoque: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<MovimentacaoEstoqueDto>> ListarMovimentacoesPorEstoqueAsync(long estoqueId)
        {
            try
            {
                if (estoqueId <= 0) throw new ArgumentException("ID de estoque inválido.");

                List<MovimentacaoEstoqueDto> movimentacoesDTO = new List<MovimentacaoEstoqueDto>();
                var movimentacoesBD = (await _movimentacaoEstoqueRepository.GetAllAsync()).Where(mov => mov.ID_ESTOQUE == estoqueId);
                var produtosBD = await _produtoRepository.GetAllAsync();
                foreach (MovimentacaoEstoque movimentacaoBD in movimentacoesBD)
                {
                    var itensMovimentacaoBD = (await _itemMovimentacaoEstoqueRepository.GetAllAsync()).Where(item => item.ID_MOVIMENTACAO == movimentacaoBD.ID_MOVIMENTACAO);
                    var produtosRetorno = itensMovimentacaoBD.Join(
                    produtosBD,
                    itensMov => itensMov.ID_PRODUTO,
                    prodBD => prodBD.ID_PRODUTO,
                    (itensMov, prodBD) => new { itensMov, prodBD }
                    );

                    movimentacoesDTO.Add(new MovimentacaoEstoqueDto
                    {
                        idMovimentacao = movimentacaoBD.ID_MOVIMENTACAO,
                        dataMovimentacao = movimentacaoBD.DT_MOVIMENTACAO,
                        idEstoque = movimentacaoBD.ID_ESTOQUE,
                        idUsuario = movimentacaoBD.ID_USUARIO,
                        processado = movimentacaoBD.IN_PROCESSADO,
                        tipoMovimentacao = movimentacaoBD.TIPO_MOVIMENTACAO,
                        itens = produtosRetorno.Select(item => new ItemMovimentacaoEstoqueDto
                        {
                            idMovimentacao = item.itensMov.ID_MOVIMENTACAO,
                            idItemMovimentacao = item.itensMov.ID_ITEM_MOVIMENTACAO,
                            idProduto = item.itensMov.ID_PRODUTO,
                            nomeProduto = item.prodBD.TX_NOME,
                            quantidadeMovimentacao = item.itensMov.QN_MOVIMENTACAO,
                            cortesias = item.itensMov.CORTESIAS,
                            quebras = item.itensMov.QUEBRAS
                        }).ToList(),
                    });
                }

                return movimentacoesDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar movimentação de estoque por estoqeu específico: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AlterarMovimentacaoAsync(MovimentacaoEstoqueAtualizarDto movimentacao)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                var movimentacaoEstoqueBD = MovimentacaoEstoque.ConvertDtoToBDObject(movimentacao);
                await ValidarMovimentacaoEstoque(movimentacaoEstoqueBD);
                _context.MovimentacoesEstoque.Update(movimentacaoEstoqueBD);

                var listItemMovimentacaoBD = new List<ItemMovimentacaoEstoque>();
                foreach (ItemMovimentacaoEstoqueDto item in movimentacao.itens)
                {
                    var itemMov = ItemMovimentacaoEstoque.ConvertDtoToBDObject(item);
                    await ValidarItemMovimentacaoEstoque(itemMov);
                    listItemMovimentacaoBD.Add(itemMov);
                }

                foreach (ItemMovimentacaoEstoque item in listItemMovimentacaoBD)
                    _context.ItemMovimentacaoEstoque.Update(item);

                await _context.SaveChangesAsync();
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao alterar movimentação: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ExcluirMovimentacaoAsync(long id)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                var movimentacao = await _context.MovimentacoesEstoque.FindAsync(id);
                if (movimentacao == null) return false;

                _context.MovimentacoesEstoque.Remove(movimentacao);
                await _context.SaveChangesAsync();
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao excluir movimentação: {ex.Message}");
                throw;
            }
        }
        #endregion

        #region Métodos Privados
        private async Task ValidarMovimentacaoEstoque(MovimentacaoEstoque movimentacaoBD)
        {
            var validationResult = await _movimentacaoEstoqueValidar.ValidateAsync(movimentacaoBD);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception($"Erro de validação: {errors}");
            }
        }

        private async Task ValidarItemMovimentacaoEstoque(ItemMovimentacaoEstoque itemMovimentacaoBD)
        {
            var validationResult = await _itemMovimentacaoEstoqueValidar.ValidateAsync(itemMovimentacaoBD);
            if (!validationResult.IsValid)
            {
                var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
                throw new Exception($"Erro de validação: {errors}");
            }
        }

        #endregion
    }
}
