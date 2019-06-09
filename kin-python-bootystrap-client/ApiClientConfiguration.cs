namespace kin_python_bootystrap_client
{
    public class ApiClientConfiguration
    {
        public string ApiHostName { get; }
        public string NetworkId { get;  }
        public string AppId { get; }
        public int StartingBalance { get; }

        public ApiClientConfiguration(string apiHostName, string networkId, string appId, int startingBalance = 0)
        {
            ApiHostName = apiHostName;
            NetworkId = networkId;
            AppId = appId;
            StartingBalance = startingBalance;
        }
    }
}