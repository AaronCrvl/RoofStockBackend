using FluentValidation;
using RoofStockBackend.Database.Dados.Objetos;
using System.Net;

namespace RoofStockBackend.Validadores
{
    public class VldrFechamentoEstoque : AbstractValidator<FechamentoEstoque>
    {
        private string errorCode = HttpStatusCode.InternalServerError.ToString();
        public VldrFechamentoEstoque()
        {
            RuleFor(fechamento => fechamento.DT_FECHAMENTO)
                .NotEmpty()
                .WithErrorCode(errorCode)
                .WithMessage("A data do fechamento não pode ser nula.");
            
            RuleFor(fechamento => fechamento.DT_INICIO_PERIODO)
                .NotEmpty()
                .WithErrorCode(errorCode)
                .WithMessage("A data inicial do período de fechamento não pode ser nula.");

            RuleFor(fechamento => fechamento.DT_FINAL_PERIODO)
                .NotEmpty()
                .WithErrorCode(errorCode)
                .WithMessage("A data final do período de fechamento não pode ser nula.");
        }
    }
}
