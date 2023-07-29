using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Sockets;

namespace MAV.Cms.Common.Utilities
{
    public class NetworkUtils
    {
        public static string GetLocalIp()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return string.Empty;
        }
        public static string GetRemoteIp(IHttpContextAccessor httpContext)
        {
            return httpContext.HttpContext.Connection.RemoteIpAddress?.ToString();
        }
    }
}
