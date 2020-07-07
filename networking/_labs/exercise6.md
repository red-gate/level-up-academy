---
layout: lab
title: 6. Access the host from web-routed
category: Network Troubleshooting
order: 6
---
Check connectivity from `web-routed.local` to `host.local`. Use `ping` and `tracepath -n` (Performs the same function as traceroute). 

Does it work? What's happening to the packets?

<details>
<summary markdown="span">Answer</summary>

It does work.

When `web-routed` attempts to send a packet to `host.local`, the IP address is compared against its local routing table. `192.168.30.0/24` is not explicitly listed in the routing table, so the packet is sent to the default gateway which is `router-vm` (`192.168.20.1`). Note that it doesn't do this by changing the destination IP header. Instead, it puts the MAC address of the default gateway in the destination MAC field in the ethernet header.

When `router-vm` receives the packet, it inspects the destination IP field and similarly compares it to its local routing table. This time, `router-vm` has an entry in the routing table for `192.168.30.0/24`, and forwards the packet out of the correct interface and on to `host.local`. In addition, when the packet is sent on by `router-vm`, it will also perform an ARP lookup to find the MAC address of `host.local` and put that in the appropriate ethernet header.


Return traffic is routed in the same way. If we hadn't put the entry in the routing table of `host.local` in the previous exercise, then connectivity would still appear to fail because there was no valid return path.
</details>