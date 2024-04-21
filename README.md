
# IPK24-CHAT Server Documentation

## Table of Contents
- [Introduction](#introduction)
- [Theoretical Background](#theoretical-background)
- [Architecture and Design](#architecture-and-design)
    - [UML Diagrams](#uml-diagrams)
    - [Interesting Code Sections](#interesting-code-sections)
- [Testing](#testing)
- [Extra Features](#extra-features)
- [Bibliography](#bibliography)
- [Usage](#usage)
- [License](#license)
- [Server Documentation Summary](#server-documentation-summary)

## Introduction
The IPK24-CHAT server application supports handling multiple clients over TCP and UDP protocols, offering robust chat functionality. This documentation explores the server architecture, details on its implementation, outlines testing strategies, and discusses extra features enhancing the chat service.

## Theoretical Background
Understanding the server side of the IPK24-CHAT requires knowledge of:
- **Concurrency**: Managing multiple client connections simultaneously through threading or asynchronous programming.
- **Network Protocols**: Implementation of both TCP and UDP protocols to handle varying network conditions and requirements.
- **Server Architecture**: Design patterns and practices for scalable and reliable server implementation.

## Architecture and Design
### UML Diagrams
![Mermaid Diagram](https://www.mermaidchart.com/raw/dd168c5a-7e37-4c4f-837d-82c2a83497af?theme=dark&version=v0.1&format=svg)

### Interesting Code Sections
- **Connection Management**: `TcpServer.cs` and `UdpServer.cs` manage the lifecycle of network connections, demonstrating socket programming.
- **User Handling**: `AbstractUser.cs` and its subclasses like `TcpUser.cs` encapsulate user operations and state management.

## [Testing](tests.md)

## Usage
1. Deploy the server on a machine with a public IP.
2. Configure firewall and network settings to allow traffic on the chosen ports.

### Command-Line Arguments
- `-l`: Set the server IP address.
- `-p`: Set the server port.
- `-d`: Set the UDP confirmation timeout.
- `-r`: Set the UDP retry count.

### Example
```shell
./ipk24chat-server -l [server_ip] -p [port] -d [udp_confirmation_timeout] -r [udp_retry_count]
```

## License
This server is open-source, licensed under the MIT License.

## Server Documentation Summary
- **Protocol Support**: Implements TCP and UDP to offer options based on client needs.
- **Extensible Design**: Modular architecture makes it easy to add new features or protocols.

## Bibliography
- [Socket Programming in C#](https://docs.microsoft.com/en-us/dotnet/framework/network-programming/socket-code-examples)
- [C# Threading](https://docs.microsoft.com/en-us/dotnet/standard/threading/)
- [TCP vs. UDP](https://www.cloudflare.com/learning/network-layer/what-is-udp/)
- [Stack Overflow](https://stackoverflow.com/questions/46882815/c-sharp-sockets-tcp-udp)
- [delfstack](https://www.delftstack.com/howto/csharp/csharp-udp-server/)
```
