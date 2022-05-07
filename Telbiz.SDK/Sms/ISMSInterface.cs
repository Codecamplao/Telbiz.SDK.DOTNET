using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbiz.SDK.Common;
using Telbiz.Shared.Common;

namespace Telbiz.SDK.Sms
{
    public interface ISMSInterface
    {
        /// <summary>
        /// This method used for send simple message
        /// </summary>
        /// <param name="header"></param>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CommonResponse> SendAsync(SMSHeader header, string phone, string message, CancellationToken cancellationToken = default);
    }
}
