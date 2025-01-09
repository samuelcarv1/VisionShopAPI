namespace VisionShopAPI.Data.Dtos
{
    public class ReadVendaDto
    {
        public int Id { get; set; }
        public string ClienteNome { get; set; }
        public DateTime DataVenda { get; set; }
        public List<ReadItemVendaDto> Itens { get; set; }
        public decimal TotalVenda { get; set; }
    }
}
