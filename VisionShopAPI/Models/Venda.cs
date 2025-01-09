using System.ComponentModel.DataAnnotations;
using VisionShopAPI.Models;

public class Venda
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; } // Relacionamento com o Cliente

    [Required]
    public DateTime DataVenda { get; set; }

    public ICollection<ItemVenda> Itens { get; set; } // Relacionamento com ItemVenda
}
