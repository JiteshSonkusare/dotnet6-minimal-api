namespace KontofonMonitor.API.Models
{
    public class Tjeneste
    {
        public string? Navn { get; set; }
        public string? Status { get; set; }
        public List<funksjon>? funksjoner { get; set; }
    }
}
