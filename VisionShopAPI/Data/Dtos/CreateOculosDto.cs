using System.ComponentModel.DataAnnotations;

namespace VisionShopAPI.Data.Dtos
{
    public class CreateOculosDto
    {
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal Preco { get; set; }

        [Required]
        public string Fabricante { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Estoque { get; set; }
    }
}
