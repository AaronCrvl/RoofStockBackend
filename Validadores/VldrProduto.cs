using RoofStockBackend.Database.Dados.Objetos;
using FluentValidation;
using System.Net;

namespace RoofStockBackend.Validadores
{
    public class VldrProduto : AbstractValidator<Produto>
    {
        private string errorCode = HttpStatusCode.InternalServerError.ToString();
        public VldrProduto()
        {
            RuleFor(produto => produto.TX_NOME)
                .NotNull()
                .WithErrorCode(errorCode)
                .WithMessage("O nome do produto não pode ser nulo.")
                .MinimumLength(3)
                .WithErrorCode(errorCode)
                .WithMessage("O nome do produto não pode ter menos que 3 caracteres.")
                .MaximumLength(15)
                .WithErrorCode(errorCode)
                .WithMessage("O nome do produto não pode ter mais que 15 caracteres.");                          

            RuleFor(produto => produto.VALOR)
                .Equal(0)
                .When(produto => !produto.IN_PROMOCAO)
                .WithErrorCode(errorCode)
                .WithMessage("O produto não poder ter valor zerado se não está em promoção.");

            RuleFor(produto => produto.ID_MARCA)
                .Equal(0)
                .WithErrorCode(errorCode)
                .WithMessage("Marca inválida para o produto.");
        }
    }
}
