using System.Net;
using System.Net.Sockets;
using System.Net.Http;

public static class CustomHttpClientFactory
{
    public static HttpClient CreateHttpClientWithCustomDns(string dnsServer)
    {
        // Configure a SocketsHttpHandler with custom ConnectCallback
        var handler = new SocketsHttpHandler
        {
            ConnectCallback = async (context, token) =>
            {
                // Use custom DNS resolver to resolve the hostname
                var ipAddresses = await Dns.GetHostAddressesAsync(context.DnsEndPoint.Host);

                if (ipAddresses.Length == 0)
                {
                    throw new SocketException((int)SocketError.HostNotFound);
                }

                // Connect to the first resolved IP address
                var ipEndPoint = new IPEndPoint(ipAddresses[0], context.DnsEndPoint.Port);
                var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    socket.Connect(ipEndPoint);
                }
                catch
                {
                    socket.Dispose();
                    throw;
                }

                return new NetworkStream(socket, ownsSocket: true);
            }
        };

        // Create HttpClient with the custom handler
        return new HttpClient(handler);
    }
}
