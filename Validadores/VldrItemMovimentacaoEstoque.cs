using FluentValidation;
using RoofStockBackend.Database.Dados.Objetos;
using System.Net;

namespace RoofStockBackend.Validadores
{
    public class VldrItemMovimentacaoEstoque : AbstractValidator<ItemMovimentacaoEstoque>
    {
        private string errorCode = HttpStatusCode.InternalServerError.ToString();
        public VldrItemMovimentacaoEstoque()
        {
            RuleFor(item => item.ID_PRODUTO)
                .GreaterThan(0)
                .WithErrorCode(errorCode)
                .WithMessage("Produto inválido");

            RuleFor(item => item.QN_MOVIMENTACAO)
                .Equal(0)
                .WithErrorCode(errorCode)
                .WithMessage("O item da movimentação não pode ter quantidade zerada.");

            RuleFor(item => item.CORTESIAS)
                .GreaterThan(item => item.QN_MOVIMENTACAO)
                .WithErrorCode(errorCode)
                .WithMessage("A quantiade de itens de cortesia não pode ser maior que a quantidade total na movimentação.");

            RuleFor(item => item.QUEBRAS)
                .GreaterThan(item => item.QN_MOVIMENTACAO)
                .WithErrorCode(errorCode)
                .WithMessage("A quantiade de itens quebradis não pode ser maior que a quantidade total na movimentação.");

            RuleFor(item => item.CORTESIAS + item.QUEBRAS)
                .GreaterThan(item => item.QN_MOVIMENTACAO)
                .WithErrorCode(errorCode)
                .WithMessage("A quantidade de itens de cortesias e itens quebrados somada não pode ser maior que a quantidade total de itens na movimentação.");
        }
    }
}
