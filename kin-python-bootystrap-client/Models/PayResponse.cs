using Newtonsoft.Json;

namespace kin_python_bootystrap_client.Models
{
    public class PayResponse : ErrorResponse
    {
        [JsonProperty("tx_id")]
        public string TransactionId { get; set; }
    }
}