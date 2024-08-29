using System.ComponentModel.DataAnnotations;

namespace LoggingApi.DTOs
{
    public class Neo4jDto
    {
        [Required]
        public Uri Url { get; set; } = new Uri("http://default.url");

        [Required]
        public string Method { get; set; } = "GET";

        [Required]
        public string FromApp { get; set; } = "API";

        [Required]
        public string ToApp { get; set; } = "API";

        [Required]
        public double Duration { get; set; } = 0.0; // Duration in milliseconds
    }
}
