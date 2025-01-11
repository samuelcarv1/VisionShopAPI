using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VisionShopAPI.Models;

namespace VisionShopAPI.Data.Dtos
{
    public class ReadMovimentacaoEstoqueDto
    {
        public int Id { get; set; }

        public int Quantidade { get; set; } 

        public DateTime DataMovimentacao { get; set; } = DateTime.Now;

        public string TipoMovimentacao { get; set; }

        public string Observacao { get; set; }

        public Oculos Oculos { get; set; }

    }
}
