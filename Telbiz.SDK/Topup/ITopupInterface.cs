using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telbiz.Shared.Common;

namespace Telbiz.SDK.Topup
{
    public interface ITopupInterface
    {
        /// <summary>
        /// This method used for bulk topup balance 
        /// </summary>
        /// <param name="phones"></param>
        /// <param name="amount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CommonResponse> BulkAsync(List<string> phones, decimal amount, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method used for topup balance mobile phone number
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="amount"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CommonResponse> SendAsync(string phone, decimal amount, CancellationToken cancellationToken = default);
    }
}
