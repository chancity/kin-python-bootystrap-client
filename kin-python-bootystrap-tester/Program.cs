using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Kin.Stellar.Sdk;
using kin_python_bootystrap_client;
using Newtonsoft.Json;

namespace kin_python_bootystrap_tester
{
    class Program
    {
        private static ApiClient _apiClient;
        private static Server _server;
        private static Asset _asset = new AssetTypeNative();
        static async Task Main(string[] args)
        {
            var httpMessageHandler = new HttpClientHandler();
            httpMessageHandler.Proxy = new WebProxy("http://192.168.1.3:9000");

            _server = new Server("https://horizon-testnet.kin.org", new HttpClient(httpMessageHandler));
            Network.Use(new Network("Kin Testnet ; December 2018"));
            

            bool exit = false;

            var apiConfiguration = new ApiClientConfiguration("https://dev-kin-python-bootstrap.kinny.io/", "Kin Testnet ; December 2018", "rced");
            _apiClient = new ApiClient(apiConfiguration, httpMessageHandler);

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            
           await RunTester(cancellationToken).ConfigureAwait(false);

            while (!exit)
            {
                var msg = Console.ReadLine();
                if (!string.IsNullOrEmpty(msg))
                {
                    exit = msg.Equals("exit");

                    if (exit)
                    {
                        cancellationTokenSource.Cancel();
                    }
                }
               Thread.Sleep(100);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }

        static async Task RunTester(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var statusResponse = await _apiClient.Status().ConfigureAwait(false);
                Console.WriteLine(JsonConvert.SerializeObject(statusResponse, Formatting.Indented));

                var keyPair = KeyPair.Random();
                var createResponse = await _apiClient.Create(keyPair.Address).ConfigureAwait(false);
                var payResponse = await _apiClient.Pay(keyPair.Address, 130).ConfigureAwait(false);
                var paymentResponse = await _apiClient.Payment(payResponse.TransactionId).ConfigureAwait(false);
                var balanceResponse = await _apiClient.Balance(keyPair.Address).ConfigureAwait(false);

                var unWhiteListedTransaction = await BuildTransaction(keyPair, 120).ConfigureAwait(false);

                var whiteListResponse = await _apiClient.Whitelist(unWhiteListedTransaction.ToEnvelopeXdrBase64()).ConfigureAwait(false);

                var txResponse = await _server.SubmitTransaction(whiteListResponse.TransactionEnvelope).ConfigureAwait(false);

              
            }
        }
        
        static async Task<Transaction> BuildTransaction(KeyPair keyPair, double amount, string appendedMemo = "")
        {
            amount = amount / 100;
            var destinationKeyPair = KeyPair.FromAccountId("GDL6CWJER7TOXIWMFTOLVUZU4GKT547OCTNPOTXISJGI4SSOPEQTC3HT");

            PaymentOperation.Builder paymentOperationBuilder = new PaymentOperation.Builder(destinationKeyPair,
                    _asset, amount.ToString(CultureInfo.InvariantCulture))
                .SetSourceAccount(keyPair);

            PaymentOperation paymentOperation = paymentOperationBuilder.Build();

            var accountResponse = await _server.Accounts.Account(keyPair);
            Transaction.Builder paymentTransaction = new Transaction.Builder(new Account(keyPair, accountResponse.SequenceNumber)).AddOperation(paymentOperation);
            paymentTransaction.AddMemo(new MemoText($"1-rced-{appendedMemo}"));
            var transaction = paymentTransaction.Build();

            transaction.Sign(keyPair);

            return transaction;
        }
    }
}
