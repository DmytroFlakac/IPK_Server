using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Server;

public class TcpUser : User
{
    private readonly TcpClient _tcpClient;
    private NetworkStream _stream;
    private StreamReader _reader;

    public TcpUser(TcpClient client)
    {
        _tcpClient = client;
        _stream = _tcpClient.GetStream();
        _reader = new StreamReader(_stream, Encoding.UTF8, leaveOpen: true);
        Port = ((IPEndPoint)client.Client.RemoteEndPoint!).Port;
        Host = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
    }

    public override async Task<string?> ReadAsyncTcp(CancellationToken cts)
    {
        StringBuilder messageBuilder = new StringBuilder();
        try
        {
            byte[] buffer = new byte[1];  // Single byte buffer for reading byte by byte
            while (!cts.IsCancellationRequested)
            {
                int bytesRead = await _stream.ReadAsync(buffer, 0, 1, cts);
                if (bytesRead == 0) // End of stream
                    break;

                char readChar = (char)buffer[0];
                if (readChar == '\n') // End of message
                {
                    // Check if the last character is '\r' and remove it
                    if (messageBuilder.Length > 0 && messageBuilder[^1] == '\r')
                    {
                        messageBuilder.Length -= 1;
                    }
                    return messageBuilder.ToString();
                }
                else
                {
                    messageBuilder.Append(readChar);
                }
            }
        }
        catch (Exception ex)
        {
            // Ignore
        }
        return null; // Return null if the loop exits without a complete message or if no more data to read
    }


    public override async Task WriteAsync(string message)
    {
        try
        {
            Console.WriteLine($"SENT {Host}:{Port} | {GetMessageType(message)}");
            var buffer = Encoding.UTF8.GetBytes(message + "\r\n");
            await _stream.WriteAsync(buffer, 0, buffer.Length);
        }
        catch (Exception)
        {
            // Ignore
        }
    }

    public override bool IsConnected() => _tcpClient.Connected;

    public override MessageType GetMessageType(string message)
    {
        if (message.Contains("AUTH"))
            return MessageType.AUTH;
        else if (message.Contains("JOIN"))
            return MessageType.JOIN;
        else if (message.Contains("MSG FROM"))
            return MessageType.MSG;
        else if (message.Contains("ERR FROM"))
            return MessageType.ERR;
        else if (message == "BYE")
            return MessageType.BYE;
        else if (message.Contains("REPLY"))
            return MessageType.REPLY;
        else
            return MessageType.ERR;
    }
    
    public override async void WriteError(string message)
    {
        await WriteAsync($"ERR FROM Server {message}");
    }

    public override void Disconnect()
    {
        Active = false;
        _stream.Close();
        _tcpClient.Close();
    }
}