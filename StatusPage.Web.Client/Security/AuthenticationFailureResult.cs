using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace StatusPage.Web.Security
{
    public class AuthenticationFailureResult : IHttpActionResult
    {
        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
        {
            ReasonPhrase = reasonPhrase;
            Request = request;
        }

        public string ReasonPhrase { get; private set; }

        public HttpRequestMessage Request { get; private set; }       

        private HttpResponseMessage Execute()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = Request;
            response.ReasonPhrase = ReasonPhrase;
            return response;
        }

        Task<HttpResponseMessage> IHttpActionResult.ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute());
        }
    }
}