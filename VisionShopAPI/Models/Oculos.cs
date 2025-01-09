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
        public string Fabricante { get; set; }
        public decimal Preco { get; set; }
    }
}
