using System.Threading.Tasks;
using kin_python_bootystrap_client.Models;
using Refit;

namespace kin_python_bootystrap_client
{
    internal interface IApiClient
    {
        [Post("/create")]
        Task<ErrorResponse> Create([Body] CreateRequest createRequest);

        [Get("/payment/{tx_hash}")]
        Task<PaymentResponse> Payment([AliasAs("tx_hash")] string transactionHash);

        [Post("/whitelist")]
        Task<WhiteListResponse> Whitelist([Body] WhiteListRequest whiteListRequest);

        [Post("/pay")]
        Task<PayResponse> Pay([Body] PayRequest payRequest);

        [Get("/balance/{address}")]
        Task<ErrorResponse> Balance([AliasAs("address")] string address);


        [Get("/status/")]
        Task<StatusResponse> Status();
    }
}