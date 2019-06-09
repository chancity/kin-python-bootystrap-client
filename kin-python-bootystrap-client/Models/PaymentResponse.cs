using Newtonsoft.Json;

namespace kin_python_bootystrap_client.Models
{
    public class PaymentResponse : ErrorResponse
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }
    }
}