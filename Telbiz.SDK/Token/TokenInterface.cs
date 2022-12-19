using HttpClientService;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
        private readonly TokenResponse tokenResponse;

        public TokenInterface(IHttpService httpService, TokenResponse tokenResponse)
        {
            this.httpService = httpService;
            this.tokenResponse = tokenResponse;
        }
        private bool validateToken(string token)
        {
            if (token == null) return false;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = tokenHandler.ReadJwtToken(token);
                return (jwtSecurityToken.ValidTo > DateTime.UtcNow.AddMinutes(-5) ? true : false);
            }
            catch (Exception)
            {
                return false;
            }


        }
        public async Task<TokenResponse> GetAccessToken(TokenEndPointRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                
                httpService.MediaType = MediaType.JSON;
                if (tokenResponse == null)
                {

                    var result = await httpService.Post<TokenEndPointRequest, TokenEndPointResponse>($"{UrlDefault.UrlEndpoint}{UrlDefault.TokenUrl}", request, cancellationToken);

                    tokenResponse!.Success = result.Success;
                    tokenResponse.Response = result.Response;

                    return new TokenResponse
                    {
                        Success = result.Success,
                        Response = result.Response
                    };
                }
                else
                {
                    // Validate
                    if (validateToken(tokenResponse.Response!.AccessToken!))
                    {
                        return tokenResponse;
                    }
                    else
                    {
                        var result = await httpService.Post<TokenEndPointRequest, TokenEndPointResponse>($"{UrlDefault.UrlEndpoint}{UrlDefault.TokenUrl}", request, cancellationToken);

                        tokenResponse.Success = result.Success;
                        tokenResponse.Response = result.Response;

                        return new TokenResponse
                        {
                            Success = result.Success,
                            Response = result.Response
                        };
                    }
                }

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
