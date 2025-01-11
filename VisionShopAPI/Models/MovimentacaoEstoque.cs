using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VisionShopAPI.Models
{
    public class MovimentacaoEstoque
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Quantidade { get; set; } // Quantidade adicionada ou removida

        [Required]
        public DateTime DataMovimentacao { get; set; } = DateTime.Now;

        [Required]
        [MaxLength(10)]
        public string TipoMovimentacao { get; set; } // "Entrada" ou "Saída"

        [MaxLength(255)]
        public string Observacao { get; set; }

        [Required]
        public int OculosId { get; set; } // Chave estrangeira para a tabela de óculos

        [ForeignKey("OculosId")]
        public Oculos Oculos { get; set; } // Relacionamento com a tabela de óculos
    }
}
