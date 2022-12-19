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

namespace Telbiz.SDK.Sms
{
    public class SMSInterface : ISMSInterface
    {
        private readonly ITokenInterface tokenInterface;
        private readonly TelbizCredential credential;
        private readonly IHttpService httpService;

        public SMSInterface(ITokenInterface tokenInterface, TelbizCredential credential, IHttpService httpService)
        {
            this.tokenInterface = tokenInterface;
            this.credential = credential;
            this.httpService = httpService;
        }

        public async Task<CommonResponse> SendAsync(SMSHeader header, string phone, string message, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                string src = "Telbiz";
                switch (header)
                {
                    case SMSHeader.News:
                        src = "News";
                        break;
                    case SMSHeader.Promotion:
                        src = "Promotion";
                        break;
                    case SMSHeader.OTP:
                        src = "OTP";
                        break;
                    case SMSHeader.Info:
                        src = "Info";
                        break;
                    default:
                        break;
                }


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

                // Try to send SMS
                var result = await httpService.Post<NewSMSRequest, NewSMSResponse, CommonResponse>($"{UrlDefault.UrlEndpoint}{UrlDefault.SendSMS}",new NewSMSRequest
                {
                    Title = src,
                    Message = message,
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


        public async Task<CommonResponse> BulkAsync(SMSHeader header, List<string> phones, string message, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                string src = "Telbiz";
                switch (header)
                {
                    case SMSHeader.News:
                        src = "News";
                        break;
                    case SMSHeader.Promotion:
                        src = "Promotion";
                        break;
                    case SMSHeader.OTP:
                        src = "OTP";
                        break;
                    case SMSHeader.Info:
                        src = "Info";
                        break;
                    default:
                        break;
                }


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

                // Try to send SMS
                var result = await httpService.Post<BulkSMSRequests, NewSMSResponse, CommonResponse>($"{UrlDefault.UrlEndpoint}{UrlDefault.BulkSMS}", new BulkSMSRequests
                {
                    Title = src,
                    Message = message,
                    Phones = phones
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
