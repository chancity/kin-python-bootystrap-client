using Newtonsoft.Json;

namespace kin_python_bootystrap_client.Models
{
    public class WhiteListResponse : ErrorResponse
    {
        [JsonProperty("tx_envelope")]
        public string TransactionEnvelope { get; set; }
    }
}