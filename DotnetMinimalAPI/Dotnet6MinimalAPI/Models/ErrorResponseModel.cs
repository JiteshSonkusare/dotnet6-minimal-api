namespace Dotnet6MinimalAPI.Models
{
    public class ErrorResponseModel
    {
        public ErrorResponseModel()
        {
            Errors = new List<string>();
        }

        [JsonPropertyName("errors")]
        public List<string>? Errors { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("status_code")]
        public int? StatusCode { get; set; }
    }
}
