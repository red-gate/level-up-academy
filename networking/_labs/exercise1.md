---
layout: lab
title: 1. Check Connectivity
category: Network Troubleshooting
order: 1
---
Use `ping` to check connectivity to some of the lab VMs from the host.

- `192.168.30.30`
- `192.168.30.20`
- `192.168.30.4`
- `192.168.30.3`

What does the response tell you?

What about using `ping` to check connectivity to the other VMs?

- `192.168.10.10`
- `192.168.20.20`

<details>
<summary markdown="span"> Answer </summary>
First, You should see that the first 4 IPs all respond to ping with responses similar to this:

```
PS C:\Users\rgadmin> ping 192.168.30.30

Pinging 192.168.30.30 with 32 bytes of data:
Reply from 192.168.30.30: bytes=32 time<1ms TTL=64
Reply from 192.168.30.30: bytes=32 time<1ms TTL=64
Reply from 192.168.30.30: bytes=32 time<1ms TTL=64
Reply from 192.168.30.30: bytes=32 time<1ms TTL=64

Ping statistics for 192.168.30.30:
    Packets: Sent = 4, Received = 4, Lost = 0 (0% loss),
Approximate round trip times in milli-seconds:
    Minimum = 0ms, Maximum = 0ms, Average = 0ms
PS C:\Users\rgadmin>
```

On Windows, the `ping` command will send 4 requests out and wait for their replies. For each one, it will show you how long the reply took which is indicated by the `time<1ms` bit of the output above.

Lastly, there's some summary information showing how many packets were sent, received, lost and then a % lost figure. There's also some statistical information on minimum, maximum and average response times. 

Trying pinging something on the internet, such as `www.google.com` and see how that output differs?

Note that on Linux, the `ping` command will continue until you stop it with `ctrl+c`. You can achieve something similar on Windows with the `-t` flag, e.g. `ping -t 192.168.30.30`. This can be useful if you want to observe response times over a long period. 


Second, you should find that pings to the second set of IPs fail.

```
PS C:\Users\rgadmin> ping 192.168.10.10

Pinging 192.168.10.10 with 32 bytes of data:
Request timed out.
Request timed out.
Request timed out.
Request timed out.

Ping statistics for 192.168.10.10:
    Packets: Sent = 4, Received = 0, Lost = 4 (100% loss),
```

Here you can see the response is `Request timed out`, and there's 100% packet loss.

We'll dig into those a bit later in these labs.


</details>
