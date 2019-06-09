using System;
using Newtonsoft.Json;

namespace kin_python_bootystrap_client.Models
{
    public class CreateRequest
    {
        [JsonProperty("destination")]
        public string Destination { get; set; }

        [JsonProperty("starting_balance")]
        public int StartingBalance { get; set; }

        [JsonProperty("memo")]
        public string Memo { get; set; }

        public CreateRequest(string destination, int startingBalance, string memo)
        {
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            StartingBalance = startingBalance;
            Memo = memo ?? throw new ArgumentNullException(nameof(memo));
        }
    }
}