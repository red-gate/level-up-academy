---
layout: lab
title: 4. Access web-host from web-nat
category: Network Troubleshooting
order: 4
---
Access web-nat using the VMWare Workstation console and login.

Use `curl` to access http://web-host.local. Does it work? Why?

`curl http://web-host.local`

<details>
<summary markdown="span"> Answer </summary>

It works because `nat-vm.local` acts as a gateway. Packets from `web-nat` travel to `nat-vm` first (because that's the default gateway). `nat-vm` then forwards the packet to `web-host`, _but_ it rewrites the source IP first as part of Network Address Translation. The returning packets from `web-host` are sent to `nat-vm` (because that's now the source IP), which in turn looks up the "real" destination and rewrites the destination IP accordingly. 

</details>
