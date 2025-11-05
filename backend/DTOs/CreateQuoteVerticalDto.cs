namespace Parason_Api.DTOs
{
    public class CreateQuoteVerticalDto
    {
        public int QuoteID { get; set; }
        public byte QuoteRevision { get; set; }
        public string Layer { get; set; } = string.Empty;
        public int VerticalID { get; set; }
        public int ProcessID { get; set; }
    }
}
