namespace VisionShopAPI.Data.Dtos
{
    public class CreateMovimentacaoEstoqueDto
    {
        public int OculosId { get; set; }
        public int Quantidade { get; set; }
        public string TipoMovimentacao { get; set; } // "Entrada" ou "Saída"
        public string Observacao { get; set; }
    }
}
