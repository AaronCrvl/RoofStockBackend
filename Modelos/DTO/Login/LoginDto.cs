using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace RoofStockBackend.Modelos.DTO.Login
{
    public class LoginDto
    {
        [Required]
        public string login { get; set; }

        [Required]
        [MinLength(5)]
        public string senha { get; set; }
    }
}