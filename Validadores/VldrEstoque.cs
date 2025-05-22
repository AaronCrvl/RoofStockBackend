using FluentValidation;
using RoofStockBackend.Database.Dados.Objetos;
using System.Net;

namespace RoofStockBackend.Validadores
{
    public class VldrEstoque : AbstractValidator<Estoque>
    {
        private string errorCode = HttpStatusCode.InternalServerError.ToString();
        public VldrEstoque()
        {
            RuleFor(est => est.TX_NOME)
                .MinimumLength(3)
                .WithErrorCode(errorCode)
                .WithMessage("O nome do estoque deve ter no mínimo 3 caracteres");

            RuleFor(est => est.ID_RESPONSAVEL)
                .Equal(0)
                .WithErrorCode(errorCode)
                .WithMessage("O responsável pelo estoque deve ser definido.");
        }
    }
}
