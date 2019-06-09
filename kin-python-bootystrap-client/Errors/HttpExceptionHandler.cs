using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using kin_python_bootystrap_client.Models;
using Newtonsoft.Json;
using NJsonSchema;
using NJsonSchema.Validation;

namespace kin_python_bootystrap_client.Errors
{
    internal class HttpExceptionHandler : DelegatingHandler
    {
        private readonly JsonSchema4 _schema;

        public HttpExceptionHandler(HttpMessageHandler innerHandler)
        {
            _schema = JsonSchema4.FromTypeAsync<ErrorResponse>().Result;
            InnerHandler = innerHandler ?? new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    string responseMessage = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (TryParseErrorResponse(responseMessage, out ErrorResponse error))
                    {
                        throw new BootyStrapException(error.Message, error.Code);
                    }
                }

                return response;
            }
            catch (WebException wex)
            {
                wex?.Response?.Dispose();
                throw;
            }
        }

        private bool TryParseErrorResponse(string jsonResponse, out ErrorResponse error)
        {
            if (!jsonResponse.Contains("code") ||
                !jsonResponse.Contains("message"))
            {
                error = null;
                return false;
            }

            ICollection<ValidationError> errors = _schema.Validate(jsonResponse);

            if (errors.Count > 0)
            {
                error = null;
                return false;
            }

            try
            {
                error = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);
                return true;
            }
            catch
            {
                error = null;
                return false;
            }
        }
    }
}