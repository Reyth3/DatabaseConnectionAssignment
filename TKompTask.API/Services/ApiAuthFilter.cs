using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TKompTask.API.Attributes;
using TKompTask.API.Settings;
using TKompTask.DataAccess;
using TKompTask.DataAccess.Queries;

namespace TKompTask.API.Services
{
    public class ApiAuthFilter : IAsyncAuthorizationFilter
    {
        public const string AuthTypeName = "Basic ";
        private readonly string _authHeaderName;
        private readonly AuthSettings _authSettings;
        private readonly IMediator _mediator;

        public ApiAuthFilter(AuthSettings auth, IMediator mediator)
        {
            _mediator = mediator;
            _authSettings = auth;
            _authHeaderName = "Authorization";
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var attribute = context.HttpContext.GetEndpoint().Metadata.OfType<ApiAuthAttribute>().SingleOrDefault();
            if (attribute == null)
                return;
            try
            {
                var request = context?.HttpContext?.Request;
                var authHeader = request.Headers.Keys.Contains(_authHeaderName) ? request.Headers[_authHeaderName].First() : null;
                string encodedAuth = (authHeader != null && authHeader.StartsWith(AuthTypeName)) ? authHeader.Substring(AuthTypeName.Length).Trim() : null;

                var apiKeyHeader = request.Headers.ContainsKey(_authSettings.ApiKeyHeader) ? request.Headers[_authSettings.ApiKeyHeader].First() : null;
                // Na twardo zakodowany API Key Basic Auth check w celu uproszczenia przykladu
                if(apiKeyHeader == null || apiKeyHeader != _authSettings.DebugApiKey || string.IsNullOrEmpty(encodedAuth))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                var (username, password) = DecodeUserIdAndPassword(encodedAuth);

                var isAuthenticated = await _mediator.Send(new TestConnectionQuery(username, password));
                if (!isAuthenticated)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }

                // Populate user: adjust claims as needed
                var claims = new[] { new Claim(ClaimTypes.Name, username, ClaimValueTypes.String, AuthTypeName) };
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, AuthTypeName));
                context.HttpContext.User = principal;
            }
            catch
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
        }

        private static (string userid, string password) DecodeUserIdAndPassword(string encodedAuth)
        {
            var userpass = Encoding.UTF8.GetString(Convert.FromBase64String(encodedAuth));
            var separator = userpass.IndexOf(':');
            if (separator == -1)
                return (null, null);

            return (userpass.Substring(0, separator), userpass.Substring(separator + 1));
        }
    }
}