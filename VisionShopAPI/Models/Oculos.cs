using System.ComponentModel.DataAnnotations;

namespace VisionShopAPI.Models
{
    public class Oculos
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public decimal Preco { get; set; }
    }
}
