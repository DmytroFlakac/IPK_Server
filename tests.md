#### [Back](README.md)
 # Server Manual Testing Documentation

This document provides a detailed overview of manual testing scenarios for validating the authentication and messaging functionality of the server using both TCP and UDP protocols.

## Test Case 1: Successful TCP Authentication

### Client Input

User executes the command to authenticate via TCP:

```bash
xtrifo00@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t tcp -s 127.0.0.1 
/auth xtrifo00 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 DT
```

### Expected Client Output

```plaintext
Success: Authenticated successfully
Server: DT has joined default
```

### Server Output

The server processes the authentication and responds as follows:

```bash
xtrifo00@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:59566 | AUTH
SENT 127.0.0.1:59566 | REPLY
SENT 127.0.0.1:59566 | MSG
```

### Conclusion

The TCP authentication test confirms the successful processing of user credentials and proper handling of session messages. The user joins the default communication channel as expected.

## Test Case 2: Successful UDP Authentication

### Client Input

User executes the command to authenticate via UDP:

```bash
xtrifo00@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t udp -s 127.0.0.1 
/auth xtrifo00 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 DT
```

### Expected Client Output

```plaintext
Success: Authenticated successfully
Server: DT has joined default
```

### Server Output

Server interaction during UDP authentication:

```bash
xtrifo00@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:55118 | AUTH
SENT 127.0.0.1:55118 | CONFIRM
SENT 127.0.0.1:55118 | REPLY
```

### Conclusion

The UDP authentication test verifies that the server correctly receives and responds to authentication requests, confirming user access and facilitating subsequent communications in the default channel.

## Test Case 3: Simple Messaging with a TCP User

### Client Input

User initiates TCP connection and sends messages:

```bash
xtrifo00@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t tcp -s 127.0.0.1 
/auth xtrifo00 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 DT
Success: Authenticated successfully
Server: DT has joined default
Hello world
I am Dmytro
```

### Server Output

The server logs the communication as follows:

```bash
xtrifo00@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:59566 | AUTH
SENT 127.0.0.1:59566 | REPLY
SENT 127.0.0.1:59566 | MSG
RECV 127.0.0.1:59566 | MSG
RECV 127.0.0.1:59566 | MSG
```

### Conclusion

The test confirms that a TCP user can successfully authenticate and then engage in simple messaging with the server.

## Test Case 4: Simple Messaging with a UDP User

### Client Input

User initiates UDP connection and sends messages:

```bash
xtrifo00@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t tcp -s 127.0.0.1 
/auth xtrifo00 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 DT
Success: Authenticated successfully
Server: DT has joined default
Hello world
I am Dmytro
```

### Server Output

The server logs the communication as follows:

```bash
xtrifo00@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:55118 | AUTH
SENT 127.0.0.1:55118 | CONFIRM
SENT 127.0.0.1:55118 | REPLY
RECV 127.0.0.1:55118 | CONFIRM
SENT 127.0.0.1:55118 | MSG
RECV 127.0.0.1:55118 | CONFIRM
RECV 127.0.0.1:55118 | MSG
SENT 127.0.0.1:55118 | CONFIRM
RECV 127.0.0.1:55118 | MSG
SENT 127.0.0.1:55118 | CONFIRM
```

### Conclusion

The test verifies that a UDP user can successfully authenticate and conduct simple messaging with the server, with proper acknowledgments and message confirmations.

## Test Case 5: Handling TCP Client Disconnection

### Client Input

```bash
xtrifo00@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t tcp -s 127.0.0.1 
/auth xtrifo00 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 DT
Success: Authenticated successfully
Server: DT has joined default
xtrifo00@DIMA:~/IPK2/IPK$
```

### Server Output

```bash
xtrifo00@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:49656 | AUTH
SENT 127.0.0.1:49656 | REPLY
SENT 127.0.0.1:49656 | MSG
RECV 127.0.0.1:49656 | BYE
```

### Conclusion

This test verifies that the server correctly handles a TCP client disconnection initiated by the client using Ctrl+C. The server receives a 'BYE' message, which signifies that the client is disconnecting gracefully.

## Test Case 6: Handling UDP Client Disconnection

### Client Input

```bash
xtrifo00@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t udp -s 127.0.0.1 
/auth xtrifo00 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 DT
Success: Authenticated successfully
Server: DT has joined default
xtrifo00@DIMA:~/IPK2/IPK$
```

### Server Output

```bash
xtrifo00@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:34836 | AUTH
SENT 127.0.0.1:34836 | CONFIRM
SENT 127.0.0.1:34836 | REPLY
RECV 127.0.0.1:34836 | CONFIRM
SENT 127.0.0.1:34836 | MSG
RECV 127.0.0.1:34836 | CONFIRM
RECV 127.0.0.1:34836 | BYE
SENT 127.0.0.1:34836 | CONFIRM
```

### Conclusion

This test ensures that the server correctly handles a UDP client disconnection, similarly initiated by the client using Ctrl+C. The server processes a 'BYE' message appropriately, confirming the message to acknowledge the client's disconnection.


## Test Case 7: Simple Broadcasting Between Two TCP Users

### Preconditions
- Server is running and ready to accept TCP connections.
- Two clients are prepared to connect via TCP.

### Test Steps
1. `ClientA` connects and authenticates via TCP.
2. `ClientB` connects and authenticates via TCP.
3. `ClientA` sends a broadcast message.
4. Verify that `ClientB` receives the broadcasted message.

### ClientA 

```
ClientA@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t tcp -s 127.0.0.1
/auth ClientA 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 user1
Success: Authenticated successfully
Server: user1 has joined default
Server: user2 has joined default
Broadcasting a message to all users
```

### ClientB 

```
ClientA@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t tcp -s 127.0.0.1
/auth ClientB Password123 user2
Success: Authenticated successfully
Server: user2 has joined default
user1: Broadcasting a message to all users
```

### Server Output

```
Server@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:55322 | AUTH
SENT 127.0.0.1:55322 | REPLY
SENT 127.0.0.1:55322 | MSG
RECV 127.0.0.1:54182 | AUTH
SENT 127.0.0.1:54182 | REPLY
SENT 127.0.0.1:54182 | MSG
SENT 127.0.0.1:55322 | MSG
RECV 127.0.0.1:55322 | MSG
SENT 127.0.0.1:54182 | MSG 
```

### Conclusion

This test confirms that when `ClientA` sends a message, it is successfully broadcasted by the server and received by `ClientB`. The server's broadcasting functionality is working as intended for TCP users.

## Test Case 8: Simple Broadcasting Between Two UDP Users

### Preconditions
- Server is running and ready to accept UDP connections.
- Two clients are prepared to connect via UDP.

### Test Steps
1. `ClientA` connects and authenticates via UDP.
2. `ClientB` connects and authenticates via UDP.
3. `ClientA` sends a broadcast message.
4. Verify that `ClientB` receives the broadcasted message.

### ClientA

```
ClientA@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t udp -s 127.0.0.1
/auth ClientA 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 user1
Success: Authenticated successfully
Server: user1 has joined default
Server: user2 has joined default
Broadcasting a message to all users
```

### ClientB 

```
ClientA@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t udp -s 127.0.0.1
/auth ClientB Password123 user2
Success: Authenticated successfully
Server: user2 has joined default
user1: Broadcasting a message to all users
```

### Server Output

```
xtrifo00@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:36559 | AUTH
SENT 127.0.0.1:36559 | CONFIRM
SENT 127.0.0.1:36559 | REPLY
RECV 127.0.0.1:36559 | CONFIRM
SENT 127.0.0.1:36559 | MSG
RECV 127.0.0.1:36559 | CONFIRM
RECV 127.0.0.1:36313 | AUTH
SENT 127.0.0.1:36313 | CONFIRM
SENT 127.0.0.1:36313 | REPLY
RECV 127.0.0.1:36313 | CONFIRM
SENT 127.0.0.1:36313 | MSG
RECV 127.0.0.1:36313 | CONFIRM
SENT 127.0.0.1:36559 | MSG
RECV 127.0.0.1:36559 | CONFIRM
RECV 127.0.0.1:36559 | MSG
SENT 127.0.0.1:36559 | CONFIRM
SENT 127.0.0.1:36313 | MSG
RECV 127.0.0.1:36313 | CONFIRM
```

### Conclusion

This test confirms that when `ClientA` sends a message over UDP, it is successfully broadcasted by the server and received by `ClientB`. The server's broadcasting functionality is working as intended for UDP users.

## Test Case 9: Broadcasting Message Between TCP and UDP Users

### Preconditions
- Server is running and ready to accept both TCP and UDP connections.
- One client is prepared to connect via TCP and another via UDP.

### Test Steps
1. `ClientA` (TCP) connects and authenticates.
2. `ClientB` (UDP) connects and authenticates.
3. `ClientA` (TCP) sends a broadcast message.
4. Verify that `ClientB` (UDP) receives the broadcasted message.

### ClientA(TCP)

```
ClientA@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t tcp -s 127.0.0.1
/auth ClientA 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 user1
Success: Authenticated successfully
Server: user1 has joined default
Server: user2 has joined default
Broadcasting a message to all users
```

### ClientB(UDP)

```
ClientA@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t udp -s 127.0.0.1
/auth ClientB Password123 user2
Success: Authenticated successfully
Server: user2 has joined default
user1: Broadcasting a message to all users
```

### Server Output

```
xtrifo00@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:57034 | AUTH
SENT 127.0.0.1:57034 | REPLY
SENT 127.0.0.1:57034 | MSG
RECV 127.0.0.1:38060 | AUTH
SENT 127.0.0.1:38060 | CONFIRM
SENT 127.0.0.1:38060 | REPLY
RECV 127.0.0.1:38060 | CONFIRM
SENT 127.0.0.1:38060 | MSG
RECV 127.0.0.1:38060 | CONFIRM
SENT 127.0.0.1:57034 | MSG
RECV 127.0.0.1:57034 | MSG
SENT 127.0.0.1:38060 | MSG
RECV 127.0.0.1:38060 | CONFIRM
```

### Conclusion

This test confirms that when `ClientA` sends a message over TCP, it is successfully broadcasted by the server to `ClientB` who is connected via UDP. The server's broadcasting functionality supports hybrid communication between TCP and UDP clients.

## Test Case 10: Broadcasting Message from UDP to TCP User

### Preconditions
- Server is running and ready to accept both TCP and UDP connections.
- One client is prepared to connect via UDP and another via TCP.

### Test Steps
1. `ClientA` (UDP) connects and authenticates.
2. `ClientB` (TCP) connects and authenticates.
3. `ClientA` (UDP) sends a broadcast message.
4. Verify that `ClientB` (TCP) receives the broadcasted message.

### ClientA(UDP)

```
ClientA@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t udp -s 127.0.0.1
/auth ClientA 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 user1
Success: Authenticated successfully
Server: user1 has joined default
Server: user2 has joined default
Broadcasting a message to all users
```

### ClientB(TCP)

```
ClientA@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t tcp -s 127.0.0.1
/auth ClientB Password123 user2
Success: Authenticated successfully
Server: user2 has joined default
user1: Broadcasting a message to all users
```

### Server Output

```
xtrifo00@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:59819 | AUTH
SENT 127.0.0.1:59819 | CONFIRM
SENT 127.0.0.1:59819 | REPLY
RECV 127.0.0.1:59819 | CONFIRM
SENT 127.0.0.1:59819 | MSG
RECV 127.0.0.1:59819 | CONFIRM
RECV 127.0.0.1:58820 | AUTH
SENT 127.0.0.1:58820 | REPLY
SENT 127.0.0.1:58820 | MSG
SENT 127.0.0.1:59819 | MSG
RECV 127.0.0.1:59819 | CONFIRM
RECV 127.0.0.1:59819 | MSG
SENT 127.0.0.1:59819 | CONFIRM
SENT 127.0.0.1:58820 | MSG
```

### Conclusion

This test confirms that when `ClientA` sends a message over UDP, it is successfully broadcasted by the server and received by `ClientB` who is connected via TCP. The server's broadcasting functionality supports communication from UDP to TCP clients effectively.


## Test Case 11: Joining channel

### Preconditions
- Server is running and ready to accept both TCP and UDP connections.
- One client is prepared to connect via UDP and another via TCP.

### Test Steps
1. `ClientA` (UDP) connects and authenticates.
2. `ClientB` (TCP) connects and authenticates.
3. `ClientA` (UDP) joins a channel.
4.  `ClientB` (TCP) joins a channel.
5. Verify that `ClientA` and `ClientB` are in the same channel.
6. Verify that `ClientA` and `ClientB` receive the broadcasted message.
### ClientA(UDP)

```
ClientA@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t udp -s 127.0.0.1
/auth ClientA 6e28ee1e-5c6f-4ef4-80e3-a368724ccb39 user1
Success: Authenticated successfully
Server: user1 has joined default
/join channel1
Server: user1 has joined channel1
Success: Joined channel1
Server: user2 has joined channel1
Broadcasting a message from user1 to all users in channel1
user2: Broadcasting a message from user2 to all users in channel1
```

### ClientB(TCP)

```
ClientA@DIMA:~/IPK2/IPK$ ./ipk24chat-client -t tcp -s 127.0.0.1
/auth ClientB Password123 user2
Success: Authenticated successfully
Server: user2 has joined default
/join channel1
Server: user2 has joined channel1
Success: Joined channel1
user1: Broadcasting a message from user1 to all users in channel1
Broadcasting a message from user2 to all users in channel1
```

### Server Output

```
xtrifo00@DIMA:~/IPK_Server$ ./ipk24chat-server
RECV 127.0.0.1:45715 | AUTH
SENT 127.0.0.1:45715 | CONFIRM
SENT 127.0.0.1:45715 | REPLY
RECV 127.0.0.1:45715 | CONFIRM
SENT 127.0.0.1:45715 | MSG
RECV 127.0.0.1:45715 | CONFIRM
RECV 127.0.0.1:45715 | JOIN
SENT 127.0.0.1:45715 | CONFIRM
SENT 127.0.0.1:45715 | MSG
RECV 127.0.0.1:45715 | CONFIRM
SENT 127.0.0.1:45715 | REPLY
RECV 127.0.0.1:45715 | CONFIRM
RECV 127.0.0.1:36156 | AUTH
SENT 127.0.0.1:36156 | REPLY
SENT 127.0.0.1:36156 | MSG
RECV 127.0.0.1:36156 | JOIN
SENT 127.0.0.1:36156 | MSG
SENT 127.0.0.1:36156 | REPLY
SENT 127.0.0.1:45715 | MSG
RECV 127.0.0.1:45715 | CONFIRM
RECV 127.0.0.1:45715 | MSG
SENT 127.0.0.1:45715 | CONFIRM
SENT 127.0.0.1:36156 | MSG
RECV 127.0.0.1:36156 | MSG
SENT 127.0.0.1:45715 | MSG
RECV 127.0.0.1:45715 | CONFIRM

```

### Conclusion

This test confirms that when `ClientA` and `ClientB` join the same channel, they can communicate with each other. The server's channel management functionality is working as intended for both TCP and UDP users.
