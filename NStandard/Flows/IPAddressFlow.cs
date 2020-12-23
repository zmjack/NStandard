using System;
using System.Linq;
using System.Net;

namespace NStandard.Flows
{
    public static class IPAddressFlow
    {
        public static long Int64(IPAddress ip) => BitConverter.ToUInt32(ip.GetAddressBytes().Reverse().ToArray(), 0);
        public static IPAddress IPAddress(long int64) => System.Net.IPAddress.Parse(int64.ToString());
    }

}
