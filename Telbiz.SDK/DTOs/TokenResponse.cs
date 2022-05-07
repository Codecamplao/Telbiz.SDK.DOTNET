using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbiz.Shared.DTO.APIGateway;

namespace Telbiz.SDK.DTOs
{
    public class TokenResponse
    {
        public bool Success { get; set; }
        public TokenEndPointResponse? Response { get; set; } = new();
    }
}
