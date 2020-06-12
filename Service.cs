using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace ZabbixSenderCore
{
    public class Service
    {
        private const int AWAITING_DATA_DELAY = 10;
        private const int RESPONSE_HEADER_LENGTH = 13;
        private const int READ_BUFFER_LENGTH = 1024;

        private byte[] _headerBuffer = Encoding.ASCII.GetBytes("ZBXD\x01");
        private JsonSerializer _serializer;

        public string ServerAddress { get; set; }
        public int ServerPort { get; set; }
        public int ConnectionTimeout { get; set; }

        public Service(string serverAddress, int port = 10051, int timeout = 150)
        {
            if (port <= 0 || port > 65_535)
                throw new ArgumentException("Port number must be between 1 and 65 535");

            if (timeout <= 0)
                throw new ArgumentException("Timeout must be greater than 0");

            if (serverAddress == null)
                throw new ArgumentException("Server address must be defined");

            this.ServerAddress = serverAddress;
            this.ServerPort = port;
            this.ConnectionTimeout = timeout;
            this._serializer = new JsonSerializer();
        }

        public async Task<Response> Send(string host, string key, string value, DateTime? dateTime = null, CancellationToken cancellationToken = default)
        {
            return await this.Send(new Data[] { new Data(host, key, value, dateTime) }, cancellationToken);
        }

        public async Task<Response> Send(Data data, CancellationToken cancellationToken = default)
        {
            return await this.Send(new Data[] { data }, cancellationToken);
        }

        public async Task<Response> Send(Data[] data, CancellationToken cancellationToken = default)
        {
            var message = this.GetMessageBuffer(data);
            using(var client = new TcpClient())
            {
                await client.ConnectAsync(this.ServerAddress, this.ServerPort);
                using (var stream = client.GetStream())
                {
                    await stream.WriteAsync(message, 0, message.Length, cancellationToken);
                    await stream.FlushAsync(cancellationToken);

                    while (!stream.DataAvailable)
                        await Task.Delay(AWAITING_DATA_DELAY, cancellationToken);

                    byte[] readBuffer = new byte[READ_BUFFER_LENGTH];
                    var responseMessage = new StringBuilder(256);
                    int bytesReaded = 0;
                    do
                    {
                        bytesReaded = await stream.ReadAsync(readBuffer, 0, readBuffer.Length, cancellationToken);
                        responseMessage.Append(Encoding.ASCII.GetString(readBuffer, 0, bytesReaded));
                    } while (stream.CanRead && bytesReaded == READ_BUFFER_LENGTH);
                    
                    var responseString = responseMessage.ToString(RESPONSE_HEADER_LENGTH, responseMessage.Length - RESPONSE_HEADER_LENGTH);
                    var result = JsonConvert.DeserializeObject<Response>(responseString);
                    return result;
                }
            }
        }

        private byte[] GetMessageBuffer(Data[] data)
        {
            var request = new Request(data);
            var dataString = JsonConvert.SerializeObject(request);
            var dataBuffer = Encoding.ASCII.GetBytes(dataString);
            var datalenBuffer = BitConverter.GetBytes((long)dataBuffer.Length);

            var message = new byte[_headerBuffer.Length + datalenBuffer.Length + dataBuffer.Length];
            Array.Copy(_headerBuffer, 0, message, 0, _headerBuffer.Length);
            Array.Copy(datalenBuffer, 0, message, _headerBuffer.Length, datalenBuffer.Length);
            Array.Copy(dataBuffer, 0, message, _headerBuffer.Length + datalenBuffer.Length, dataBuffer.Length);

            return message;
        }
    }
}
