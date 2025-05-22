using FluentValidation;
using RoofStockBackend.Database.Dados.Enums;
using RoofStockBackend.Database.Dados.Objetos;
using System.Net;

namespace RoofStockBackend.Validadores
{
    public class VldrMovimentacaoEstoque : AbstractValidator<MovimentacaoEstoque>
    {
        private string errorCode = HttpStatusCode.InternalServerError.ToString();
        public VldrMovimentacaoEstoque()
        {
            RuleFor(mov => mov.ID_ESTOQUE)
                .NotNull()
                .WithErrorCode(errorCode)
                .WithMessage("O estoque da movimentação não pode ser nulo.");

            RuleFor(mov => mov.ID_USUARIO)
                .NotNull()
                .WithErrorCode(errorCode)
                .WithMessage("O usuário da movimentação não pode ser nulo.");

            RuleFor(mov => mov.TIPO_MOVIMENTACAO)
                .InclusiveBetween((long)eTipoMovimentacao.Saida, (long)eTipoMovimentacao.Entrada)
                .WithErrorCode(errorCode)
                .WithMessage("Valor inválido para tipo de movimentação.");
        }
    }
}
