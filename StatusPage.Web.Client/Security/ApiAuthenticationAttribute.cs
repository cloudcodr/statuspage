using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace StatusPage.Web.Security
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiAuthenticationAttribute : Attribute, System.Web.Http.Filters.IAuthenticationFilter
    {
        bool IFilter.AllowMultiple
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Simplified API authentication.
        /// - No it is not secure. Yes it can replay. Yes you can sniff the API token.
        /// Implement your own authentication logic here using nonce etc.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            // 1. Look for credentials in the request.
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            // 2. If there are no credentials, do nothing.
            if (authorization == null)
            {
                return;
            }

            // 3. If there are credentials but the filter does not recognize the 
            //    authentication scheme, do nothing.
            if (authorization.Scheme != "Basic")
            {
                return;
            }

            // 4. If there are credentials that the filter understands, try to validate them.
            // 5. If the credentials are bad, set the error result.
            if (String.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials", request);
                return;
            }

            string apiTokenCredentials = ExtractApiCredentials(authorization.Parameter);
            if (apiTokenCredentials == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials", request);
            }

            IPrincipal principal = await AuthenticateAsync(apiTokenCredentials, cancellationToken);
            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid api credentials", request);
            }

            // 6. If the credentials are valid, set principal.
            else
            {
                context.Principal = principal;
            }
        }

        private async Task<IPrincipal> AuthenticateAsync(string apiToken, CancellationToken cancellationToken)
        {
            var token = System.Configuration.ConfigurationManager.AppSettings["api:token"];
            if (token == null)
                throw new ApplicationException("API Token is missing in web.config");

            if (!token.Equals(apiToken))
            {
                return null;
            }

            // return any principal
            return new GenericPrincipal(new GenericIdentity("Api", "Basic"), new string[0]);            
        }

        private string ExtractApiCredentials(string parameter)
        {
            return parameter;
        }

        Task IAuthenticationFilter.ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Basic");
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);
            return Task.FromResult(0);
        }
    }
}