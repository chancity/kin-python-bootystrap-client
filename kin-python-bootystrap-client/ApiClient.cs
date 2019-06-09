using System;
using System.Net.Http;
using System.Threading.Tasks;
using kin_python_bootystrap_client.Errors;
using kin_python_bootystrap_client.Models;
using Refit;

namespace kin_python_bootystrap_client
{
    public class ApiClient
    {
        private readonly IApiClient _apiClient;
        private readonly ApiClientConfiguration _configuration;

        public ApiClient(ApiClientConfiguration configuration, HttpMessageHandler httpMessageHandler = null)
        {
            _configuration = configuration ?? throw new ArgumentNullException($"{nameof(configuration)}");

            HttpExceptionHandler exceptionHandler =
                new HttpExceptionHandler(httpMessageHandler ?? new HttpClientHandler());

            HttpClient httpClient = new HttpClient(exceptionHandler)
            {
                BaseAddress = new Uri(_configuration.ApiHostName)
            };

            _apiClient = RestService.For<IApiClient>(httpClient);
        }

        public Task<PayResponse> Pay(string address, int amount, string memo = "")
        {
            return _apiClient.Pay(new PayRequest(address, amount, memo));
        }

        public Task<ErrorResponse> Create(string address, string memo = "")
        {
            return _apiClient.Create(new CreateRequest(address, _configuration.StartingBalance, memo));
        }

        public Task<ErrorResponse> Balance(string address)
        {
            return _apiClient.Balance(address);
        }

        public Task<PaymentResponse> Payment(string transactionHash)
        {
            return _apiClient.Payment(transactionHash);
        }

        public Task<WhiteListResponse> Whitelist(string envelope)
        {
            return _apiClient.Whitelist(new WhiteListRequest(envelope, _configuration.NetworkId));
        }

        public Task<StatusResponse> Status()
        {
            return _apiClient.Status();
        }
    }
}