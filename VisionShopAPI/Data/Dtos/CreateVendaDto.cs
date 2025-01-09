using System.ComponentModel.DataAnnotations;

namespace VisionShopAPI.Data.Dtos
{
    public class CreateVendaDto
    {
        [Required]
        public int ClienteId { get; set; }

        [Required]
        public DateTime DataVenda { get; set; }

        [Required]
        public List<CreateItemVendaDto> Itens { get; set; }
    }
}
