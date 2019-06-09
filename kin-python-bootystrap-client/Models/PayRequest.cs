using System;
using Newtonsoft.Json;

namespace kin_python_bootystrap_client.Models
{
    public class PayRequest
    {
        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }

        public PayRequest(string destination, int amount, string memo)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            Amount = amount;
            Memo = memo ?? throw new ArgumentNullException(nameof(memo));
        }
    }
}