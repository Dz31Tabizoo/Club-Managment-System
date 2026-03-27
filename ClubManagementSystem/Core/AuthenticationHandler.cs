using ClubManagementSystem.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ClubManagementSystem.Core
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly IAuthenticationClientService _authService;

        public AuthenticationHandler(IAuthenticationClientService authService)
        {
            _authService = authService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var token = _authService.CurrentUser?.Token;

            if (!String.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            }            
                return await base.SendAsync(request, cancellationToken);
        }
    }
}
