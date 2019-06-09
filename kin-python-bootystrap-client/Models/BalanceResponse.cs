using Newtonsoft.Json;

namespace kin_python_bootystrap_client.Models
{
    public class BalanceResponse
    {
        [JsonProperty("balance")]
        public float Balance { get; set; }
    }
}