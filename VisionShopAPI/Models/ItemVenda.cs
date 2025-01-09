using System.ComponentModel.DataAnnotations;

namespace VisionShopAPI.Models
{
    public class ItemVenda
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VendaId { get; set; }
        public Venda Venda { get; set; } // Relacionamento com Venda

        [Required]
        public int OculosId { get; set; }
        public Oculos Oculos { get; set; } // Relacionamento com Óculos

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public decimal PrecoUnitario { get; set; }

        public decimal Total => Quantidade * PrecoUnitario; // Propriedade calculada
    }

}
