using HttpClientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbiz.SDK.Common;
using Telbiz.SDK.DTOs;
using Telbiz.Shared.DTO.APIGateway;

namespace Telbiz.SDK.Token
{
    public class TokenInterface : ITokenInterface
    {
        private readonly IHttpService httpService;

        public TokenInterface(IHttpService httpService)
        {
            this.httpService = httpService;
        }
        public async Task<TokenResponse> GetAccessToken(TokenEndPointRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                
                httpService.MediaType = MediaType.JSON;
                var result = await httpService.Post<TokenEndPointRequest, TokenEndPointResponse>($"{UrlDefault.UrlEndpoint}{UrlDefault.TokenUrl}", request, cancellationToken);

                return new TokenResponse
                {
                    Success = result.Success,
                    Response = result.Response
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<TokenResponse> GetRefreshToken(RefreshTokenEndPointRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {

                httpService.MediaType = MediaType.JSON;
                var result = await httpService.Post<RefreshTokenEndPointRequest, TokenEndPointResponse>($"{UrlDefault.UrlEndpoint}{UrlDefault.RefreshTokenUrl}", request, cancellationToken);

                return new TokenResponse
                {
                    Success = result.Success,
                    Response = result.Response
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
