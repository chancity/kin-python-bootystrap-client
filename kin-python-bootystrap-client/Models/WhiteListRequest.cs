using System;
using Newtonsoft.Json;

namespace kin_python_bootystrap_client.Models
{
    public class WhiteListRequest
    {
        [JsonProperty("envelope")]
        public string Envelope { get; set; }

        [JsonProperty("network_id")]
        public string NetworkId { get; set; }

        public WhiteListRequest(string envelope, string networkId)
        {
            Envelope = envelope ?? throw new ArgumentNullException(nameof(envelope));
            NetworkId = networkId ?? throw new ArgumentNullException(nameof(networkId));
        }
    }
}