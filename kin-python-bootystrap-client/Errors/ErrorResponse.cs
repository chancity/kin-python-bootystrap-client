using Newtonsoft.Json;

namespace kin_python_bootystrap_client.Models
{
    public class ErrorResponse
    {
        [JsonProperty("code")]
        public ErrorCodeTypes Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}