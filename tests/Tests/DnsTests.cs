using System;
using System.Linq;
using System.Net;
using Xunit;

namespace Tests {
    public class DnsTests {
        [Fact]
        public void TestDns() {
            var entry = Dns
                .GetHostEntry("i.hazzik.ru")
                .AddressList
                .First()
                .ToString();
          
        }
    }
}