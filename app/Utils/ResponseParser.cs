
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ZabbixSenderCore
{
    public class ResponseParser
    {
        public ResponseInfo Parse(string response)
        {
            var rows = getRows(response);
            var values = GetValues(rows);

            return FillResponseInfo(values);
        }

        private string[] getRows(string response)
        {
            return response.Split(new[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
        }

        private IDictionary<string, string> GetValues(string[] rows)
        {
            IDictionary<string, string> values = new Dictionary<string, string>(5);

            for (var i = 0; i < rows.Length; i++)
            {
                var row = rows[i];
                if (string.IsNullOrWhiteSpace(row))
                    continue;

                var rowData = row.Split(new char[] { ':' }, System.StringSplitOptions.RemoveEmptyEntries);
                var name = rowData[0].Trim();
                var value = rowData[1].Trim();

                values.Add(name, value);
            }

            return values;
        }

        private ResponseInfo FillResponseInfo(IDictionary<string, string> values)
        {
            var result = new ResponseInfo();

            foreach (var kv in values)
            {
                switch (kv.Key)
                {
                    case "failed":
                        result.Failed = int.Parse(kv.Value);
                        break;
                    case "processed":
                        result.Processed = int.Parse(kv.Value);
                        break;
                    case "total":
                        result.Total = int.Parse(kv.Value);
                        break;
                    case "seconds spent":
                        result.SecondsSpent = decimal.Parse(kv.Value, NumberStyles.Currency, CultureInfo.InvariantCulture);
                        break;
                    default:
                        throw new NotImplementedException($"Not implemented parsing for \"{kv.Key}\" zabbix key.");
                }
            }

            return result;
        }
    }

    public class ResponseInfo
    {
        public int Processed { get; set; }
        public int Failed { get; set; }
        public int Total { get; set; }
        public decimal SecondsSpent { get; set; }

    }
}