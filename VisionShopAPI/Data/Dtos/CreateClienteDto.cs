using System.ComponentModel.DataAnnotations;

namespace VisionShopAPI.Data.Dtos
{
    public class CreateClienteDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
        public string Nome { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public string Email { get; set; }

        [Required]
        [Phone(ErrorMessage = "Número de telefone inválido.")]
        public string Telefone { get; set; }

        [Required]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O CPF deve estar no formato 000.000.000-00.")]
        public string CPF { get; set; }
    }
}
