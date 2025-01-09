using System.ComponentModel.DataAnnotations;

namespace VisionShopAPI.Data.Dtos
{
    public class CreateItemVendaDto
    {
        [Required]
        public int OculosId { get; set; }

        [Required]
        public int Quantidade { get; set; }

        [Required]
        public decimal PrecoUnitario { get; set; }
    }
}
