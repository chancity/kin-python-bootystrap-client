namespace kin_python_bootystrap_client.Models
{
    public class StatusResponse
    {
        public string service_version { get; set; }
        public string horizon { get; set; }
        public string app_id { get; set; }
        public string public_address { get; set; }
        public float balance { get; set; }
        public Channels channels { get; set; }
    }

    public class Channels
    {
        public int free_channels { get; set; }
        public int non_free_channels { get; set; }
        public int total_channels { get; set; }
    }
}