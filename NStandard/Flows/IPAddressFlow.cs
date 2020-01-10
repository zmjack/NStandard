using System;
using System.Linq;
using System.Net;

namespace NStandard.Flows
{
    public static class IPAddressFlow
    {
        public static IFlow<IPAddress, long> IPLong = new Flow<IPAddress, long>(x => BitConverter.ToUInt32(x.GetAddressBytes().Reverse().ToArray(), 0));

        public static IFlow<long, IPAddress> FromIPLong = new Flow<long, IPAddress>(x => IPAddress.Parse(x.ToString()));
    }

}
