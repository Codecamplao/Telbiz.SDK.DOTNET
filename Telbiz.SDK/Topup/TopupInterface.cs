using HttpClientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbiz.SDK.Common;
using Telbiz.SDK.Configurations;
using Telbiz.SDK.Token;
using Telbiz.Shared.Common;
using Telbiz.Shared.DTO.APIGateway;

namespace Telbiz.SDK.Topup
{
    public class TopupInterface : ITopupInterface
    {
        private readonly ITokenInterface tokenInterface;
        private readonly TelbizCredential credential;
        private readonly IHttpService httpService;
        public TopupInterface(ITokenInterface tokenInterface, TelbizCredential credential, IHttpService httpService)
        {
            this.tokenInterface = tokenInterface;
            this.credential = credential;
            this.httpService = httpService;
        }

        public async Task<CommonResponse> SendAsync(string phone, decimal amount, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {

                // Get access token
                var access_token = await tokenInterface.GetAccessToken(new TokenEndPointRequest
                {
                    ClientID = credential.ClientID,
                    GrantType = "client_credentials",
                    Scope = "Telbiz_API_SCOPE profile openid",
                    Secret = credential.Secret,
                }, cancellationToken);

                if (!access_token.Success)
                {
                    return new CommonResponse
                    {
                        Code = "INVALID_CLIENT",
                        Success = false,
                        Message = "INVALID_CLIENT",
                        Detail = access_token.Response.Detail
                    };
                }

                // Try to Topup
                var result = await httpService.Post<NewTopupRequest, NewTopupResponse, CommonResponse>($"{UrlDefault.UrlEndpoint}{UrlDefault.Topup}", new NewTopupRequest
                {
                    Amount = amount,
                    Phone = phone
                }, new AuthorizeHeader("bearer", access_token.Response.AccessToken), cancellationToken);

                if (result.Success)
                {
                    return result.Response.Response;
                }
                return new CommonResponse
                {
                    Success = false,
                    Code = result.Error != null ? result.Error.Code : "SEND_SMS_FAIL",
                    Detail = result.Error != null ? result.Error.Detail : "SEND_SMS_FAIL",
                    Message = result.Error != null ? result.Error.Message : "SEND_SMS_FAIL"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
