using System;
using System.Collections.Generic;
using Xunit;
using ZabbixSenderCore;

namespace tests
{
    public class ResponseParserTest
    {
        public static List<object[]> ResponseInfoParserParameters = new List<object[]>()
        {
            new object[] { "processed: 1; failed: 0; total: 1; seconds spent: 0.123", 1, 0, 1, 0.123 },
            new object[] { "processed: 11; failed: 1; total: 12; seconds spent: 1.22", 11, 1, 12, 1.22 },
            new object[] { " seconds spent:  1  ; total  :  12  ; failed: 2; processed: 10; ", 10, 2, 12, 1 }
        };

        [Theory]
        [MemberData("ResponseInfoParserParameters")]
        public void Test1(string response, int processed, int failed, int total, decimal secondsSpent)
        {
            var parser = new ResponseParser();
            var parsed = parser.Parse(response);

            Assert.Equal(processed, parsed.Processed);
            Assert.Equal(failed, parsed.Failed);
            Assert.Equal(secondsSpent, parsed.SecondsSpent);
            Assert.Equal(total, parsed.Total);
        }
    }
}
