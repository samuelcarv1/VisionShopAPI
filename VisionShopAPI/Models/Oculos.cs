using System.ComponentModel.DataAnnotations;

namespace VisionShopAPI.Models
{
    public class Oculos
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Telefone { get; set; }
    }
}
