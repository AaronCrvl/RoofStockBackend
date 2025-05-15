using FluentValidation;
using RoofStockBackend.Database.Dados.Objetos;

namespace RoofStockBackend.Validadores
{
    public class VldrEstoque : AbstractValidator<Estoque>
    {
        public VldrEstoque()
        {
            RuleFor(est => est.TX_NOME)
                .MinimumLength(3)
                .WithMessage("O nome do estoque deve ter no mínimo 3 caracteres");

            RuleFor(est => est.ID_RESPONSAVEL)
                .Equal(0)
                .WithMessage("O responsável pelo estoque deve ser definido.");
        }
    }
}
