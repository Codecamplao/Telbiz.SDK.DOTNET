using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telbiz.SDK.Common
{
    public static class UrlDefault
    {
        public static string UrlEndpoint = "https://api.telbiz.la";
        public static string TokenUrl = "/api/v1/connect/token";
        public static string RefreshTokenUrl = "/api/v1/connect/refreshToken";
        public static string SendSMS = "/api/v1/smsservice/newtransaction";
        public static string BulkSMS = "/api/v1/smsservice/bulktransactions";
        public static string Topup = "/api/v1/topupservice/newtransaction";
        public static string BulkTopup = "/api/v1/topupservice/bulktransactions";
    }
}
