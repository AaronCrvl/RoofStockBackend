using FluentValidation;
using RoofStockBackend.Database.Dados.Objetos;
using System.Net;

namespace RoofStockBackend.Validadores
{
    public class VldrItemFechamentoEstoque : AbstractValidator<ItemFechamentoEstoque>
    {
        private string errorCode = HttpStatusCode.InternalServerError.ToString();
        public VldrItemFechamentoEstoque()
        {
            RuleFor(item => item.ID_FECHAMENTO)
                .NotEmpty()
                .GreaterThan(0)
                .WithErrorCode(errorCode)
                .WithMessage("O fechamento original não pode ser nulo no item.");

            RuleFor(item => item.ID_PRODUTO)
                .NotEmpty()
                .GreaterThan(0)               
                .WithErrorCode(errorCode)
                .WithMessage("O item do fechamento não pode ser nulo.");            

            RuleFor(item => item.QN_CORTESIAS)
                .LessThan(item => item.QN_FINAL)                
                .WithErrorCode(errorCode)
                .WithMessage("A quantidade de cortesias não pode ser maior que a quantidade final.");

            RuleFor(item => item.QN_QUEBRAS)
                .LessThan(item => item.QN_FINAL)
                .WithErrorCode(errorCode)
                .WithMessage("A quantidade de quebras não pode ser maior que a quantidade final.");

            RuleFor(item => item.QN_DIVERGENCIA)
                .GreaterThan(0)
                .Unless(item => item.IN_DIVERGENCIA)                
                .WithErrorCode(errorCode)
                .WithMessage("A quantidade de divergência não pode ser maior que zero se o item não foi marcado como divergente.");
        }
    }
}
