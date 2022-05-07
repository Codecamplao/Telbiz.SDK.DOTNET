using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbiz.SDK.DTOs;
using Telbiz.Shared.DTO.APIGateway;

namespace Telbiz.SDK.Token
{
    public interface ITokenInterface
    {
        Task<TokenResponse> GetAccessToken(TokenEndPointRequest request, CancellationToken cancellationToken = default);
        Task<TokenResponse> GetRefreshToken(RefreshTokenEndPointRequest request, CancellationToken cancellationToken = default);
    }
}
