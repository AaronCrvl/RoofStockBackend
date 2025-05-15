using RoofStockBackend.Database.Dados.Objetos;
using FluentValidation;

namespace RoofStockBackend.Validadores
{
    public class VldrProduto : AbstractValidator<Produto>
    {
        public VldrProduto()
        {
            RuleFor(produto => produto.TX_NOME)
                .NotNull()
                .WithMessage("O nome do produto não pode ser nulo.");

            RuleFor(produto => produto.TX_NOME)
                .MinimumLength(3)
                .WithMessage("O nome do produto não pode ter meonos que 3 caracteres.");

            RuleFor(produto => produto.TX_NOME)
                .MaximumLength(15)
                .WithMessage("O nome do produto não pode ter mais que 15 caracteres."); ;

            RuleFor(produto => produto.VALOR)
                .Equal(0)
                .When(produto => !produto.IN_PROMOCAO)
                .WithMessage("O produto não poder ter valor zerado se não está em promoção.");

            RuleFor(produto => produto.ID_MARCA)
                .Equal(0)
                .WithMessage("Marca inválida para o produto.");
        }
    }
}
