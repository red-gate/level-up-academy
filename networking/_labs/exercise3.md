---
layout: lab
title: 3. Access web-nat
category: Network Troubleshooting
order: 3
---
From the host browse to http://web-nat.local. Does it work? Why? 

<details>
<summary markdown="span"> Hint </summary>

Remember that we have NAT

</details>

<details>
<summary markdown="span"> Answer </summary>

The `192.168.10.0/24` network is not routable from the host network, which means IP packets cannot travel to 192.168.10.10 (web-nat.local). 

How _could_ we solve this problem?

#### Port forwarding

Try browsing http://nat-vm.local:8080 from the Host machine. What happens? 

This uses port forwarding. We use iptables on Linux to configure this. Take a look at the rules by ssh-ing to `nat-vm.local` and run `sudo iptables -L -t nat -v -n`

This is the output you'll see.

```
root@nat-vm:/home/rgadmin# iptables -L -t nat -v -n
Chain PREROUTING (policy ACCEPT 62 packets, 6753 bytes)
 pkts bytes target     prot opt in     out     source               destination
    2   104 DNAT       tcp  --  *      *       0.0.0.0/0            0.0.0.0/0            tcp dpt:8080 to:192.168.10.10:80

Chain INPUT (policy ACCEPT 46 packets, 5630 bytes)
 pkts bytes target     prot opt in     out     source               destination

Chain OUTPUT (policy ACCEPT 32 packets, 2219 bytes)
 pkts bytes target     prot opt in     out     source               destination

Chain POSTROUTING (policy ACCEPT 2 packets, 104 bytes)
 pkts bytes target     prot opt in     out     source               destination
   48  3342 MASQUERADE  all  --  *      ens33   0.0.0.0/0            0.0.0.0/0
```

For this example, we care about the first `PREROUTING` stage. 

You can see that it's doing Destination NAT (DNAT), on the TCP protocol. We're allowing any source and any destination, and the exact translation we're going to apply is `tcp dpt:8080 to:192.168.10.10:80`. Loosely, that's 'take inbound connections to me on TCP port 8080, and send them to host 192.168.10.10 on port 80'. 

In the background, iptables then takes care of forwarding the request it received on port 8080 to port 80 of the _actual_ web server and then relaying the response back to the original client (the host machine, in this case).

</details>