using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RoofStockBackend.Contextos;
using RoofStockBackend.Database.Dados.Objetos;
using RoofStockBackend.Modelos.DTO.Fechamento_Estoque;
using RoofStockBackend.Modelos.DTO.Movimentação_Estoque;
using RoofStockBackend.Repositorios;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStockBackend.Services
{
    public class SrvcFechamentoEstoque
    {
        #region Propriedades Privadas
        private readonly AppDbContext _context;
        private readonly Repository<FechamentoEstoque> _fechamentoEstoqueRepository;
        private readonly Repository<ItemFechamentoEstoque> _itemFechamentoEstoqueRepository;
        private readonly SrvcEstoqueProduto _srvcProduto;
        private readonly IValidator<FechamentoEstoque> _fechamentoEstoqueValidar;
        private readonly IValidator<ItemFechamentoEstoque> _itemFechamentoEstoqueValidar;
        #endregion

        #region Construtor
        public SrvcFechamentoEstoque(AppDbContext context, IValidator<FechamentoEstoque> fechamentoEstoqueValidar, IValidator<ItemFechamentoEstoque> itemFechamentoEstoqueValidar, IValidator<Produto> produtoValidar)
        {
            _fechamentoEstoqueRepository = new Repository<FechamentoEstoque>(context);
            _itemFechamentoEstoqueRepository = new Repository<ItemFechamentoEstoque>(context);
            _srvcProduto = new SrvcEstoqueProduto(context, produtoValidar);
            _fechamentoEstoqueValidar = fechamentoEstoqueValidar;
            _itemFechamentoEstoqueValidar = itemFechamentoEstoqueValidar;
        }
        #endregion

        #region Métodos Públicos
        public async Task<bool> CriarFechamentoEstoqueAsync(FechamentoEstoqueCriarDto fechamentoEstoqueDto)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                var fechamentoEstoqueBD = FechamentoEstoque.ConvertDtoToObj(fechamentoEstoqueDto);
                await _fechamentoEstoqueValidar.ValidateAsync(fechamentoEstoqueBD);
                _context.FechamentoEstoques.Add(fechamentoEstoqueBD);

                var listaItensFechamento = new List<ItemFechamentoEstoque>();
                foreach (ItemFechamentoEstoqueDto item in fechamentoEstoqueDto.itens)
                {
                    var itemBD = new ItemFechamentoEstoque
                    {
                        ID_FECHAMENTO = item.idFechamentoEstoque,
                        ID_PRODUTO = item.idProduto,
                        IN_DIVERGENCIA = item.divergencia,
                        QN_DIVERGENCIA = item.quantidadeDivergencia,
                        QN_CORTESIAS = item.cortesias,
                        QN_QUEBRAS = item.quebras,
                        QN_FINAL = item.quantidadeFinal
                    };

                    await _itemFechamentoEstoqueValidar.ValidateAsync(itemBD);
                    listaItensFechamento.Add(itemBD);
                }

                await _context.SaveChangesAsync();
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao criar fechamento: {ex.Message}");
                throw;
            }
        }

        public async Task<FechamentoEstoqueDto> CarregarFechamentoEstoquePorIdAsync(int id)
        {
            try
            {
                var fechamentoEstoqueBD = await _fechamentoEstoqueRepository.GetByIdAsync(id);
                if (fechamentoEstoqueBD.ID_FECHAMENTO <= 0)
                    throw new ArgumentException("O fechamento de estoque buscado não foi encontrado.");

                var fechamentoDto = await ConstruirDtoRetorno(fechamentoEstoqueBD);
                return fechamentoDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar fechamento de estoque: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<FechamentoEstoqueDto>> CarregarFechamentoPorEstoqueAsync(int idEstoque)
        {
            try
            {
                if (idEstoque <= 0)
                    throw new ArgumentException("O fechamento de estoque buscado não foi encontrado.");

                var listaFechamentoEstoqueDto = new List<FechamentoEstoqueDto>();
                var listaFechamentoEstoqueBD = (await _fechamentoEstoqueRepository.GetAllAsync()).Where(item => item.ID_ESTOQUE == idEstoque);
                foreach (FechamentoEstoque fechamentoBD in listaFechamentoEstoqueBD)
                {
                    FechamentoEstoqueDto fechamentoDto = await ConstruirDtoRetorno(fechamentoBD);
                    listaFechamentoEstoqueDto.Add(fechamentoDto);
                }

                return listaFechamentoEstoqueDto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar fechamento por estoque: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AlterarFechamentoEstoqueAsync(FechamentoEstoqueAtualizarDto fechamentoDto)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                if (fechamentoDto.idFechamentoEstoque <= 0)
                    throw new ArgumentException("O fechamento de estoque a ser alterado não foi encontrado.");

                var fechamentoEstoqueBD = FechamentoEstoque.ConvertDtoToObj(fechamentoDto);
                await _fechamentoEstoqueValidar.ValidateAsync(fechamentoEstoqueBD);
                await _fechamentoEstoqueRepository.UpdateAsync(fechamentoEstoqueBD);

                foreach (ItemFechamentoEstoqueDto itemDto in fechamentoDto.itens)
                {
                    var itemBD = new ItemFechamentoEstoque
                    {
                        ID_FECHAMENTO = itemDto.idFechamentoEstoque,
                        ID_PRODUTO = itemDto.idProduto,
                        QN_FINAL = itemDto.quantidadeFinal,
                        QN_CORTESIAS = itemDto.cortesias,
                        QN_QUEBRAS = itemDto.quebras,
                        IN_DIVERGENCIA = itemDto.divergencia,
                        QN_DIVERGENCIA = itemDto.quantidadeDivergencia
                    };
                    await _itemFechamentoEstoqueValidar.ValidateAsync(itemBD);
                    await _itemFechamentoEstoqueRepository.UpdateAsync(itemBD);
                }

                await _context.SaveChangesAsync();
                await t.CommitAsync();                
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao alterar fechamento de estoque: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ExcluirFechamentoEstoqueAsync(int id)
        {
            using var t = await _context.Database.BeginTransactionAsync();
            try
            {
                if (id <= 0) throw new ArgumentException("ID inválido.");
                await _fechamentoEstoqueRepository.DeleteAsync(id);
                await _itemFechamentoEstoqueRepository.DeleteAsync(id);

                await _context.SaveChangesAsync();
                await t.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await t.RollbackAsync();
                Console.WriteLine($"Erro ao excluir fechamento de estoque: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Métodos Privados
        private async Task<FechamentoEstoqueDto> ConstruirDtoRetorno(FechamentoEstoque fechamentoBD)
        {
            try
            {
                if (fechamentoBD.ID_FECHAMENTO <= 0)
                    throw new Exception("O fechamento de estoque buscado não foi encontrado.");

                var fechamentoDto = new FechamentoEstoqueDto()
                {
                    idFechamentoEstoque = fechamentoBD.ID_FECHAMENTO,
                    idEstoque = fechamentoBD.ID_ESTOQUE,
                    erro = fechamentoBD.IN_ERRO,
                    dataFechamento = fechamentoBD.DT_FECHAMENTO,
                    dataInicioPeriodo = fechamentoBD.DT_INICIO_PERIODO,
                    dataFinalPeriodo = fechamentoBD.DT_FINAL_PERIODO,
                };

                var itensFechamentoEstoqueBD = (await _itemFechamentoEstoqueRepository.GetAllAsync()).Where(item => item.ID_FECHAMENTO == fechamentoBD.ID_FECHAMENTO);
                foreach (ItemFechamentoEstoque itemBD in itensFechamentoEstoqueBD)
                    fechamentoDto.itens.Add(new ItemFechamentoEstoqueDto
                    {
                        idFechamentoEstoque = itemBD.ID_FECHAMENTO,
                        idProduto = itemBD.ID_PRODUTO,
                        nomeProduto = await _srvcProduto.ObterNomeProduto(itemBD.ID_PRODUTO),
                        quantidadeFinal = itemBD.QN_FINAL,
                        cortesias = itemBD.QN_CORTESIAS,
                        quebras = itemBD.QN_QUEBRAS,
                        divergencia = itemBD.IN_DIVERGENCIA,
                        quantidadeDivergencia = itemBD.QN_DIVERGENCIA
                    });

                return fechamentoDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
