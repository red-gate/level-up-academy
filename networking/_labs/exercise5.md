---
layout: lab
title: 5. Access web-route from the host
category: Network Troubleshooting
order: 5
---
Access http://web-routed.local via the web browser on the host. Does it work? Why?

<details>
<summary markdown="span">Hint</summary>

Use traceroute (`tracert -d <destinationIP>`). Is it correct? What _should_ it be? 

</details>

<details>
<summary markdown="span">Answer</summary>

After two hops, you begin getting `Request timed out` responses. Eventually, after 30 hops, tracert exits.

This shows the packet effectively getting lost. It has been passed through a few routers and we're no longer getting responses suggesting that the packet is just getting dropped. In some cases you may also get a `No route to host` response.

#### Fixing it
This is happening because there is no valid route to the destination network. You can add a new route to the Windows routing table manually using the command line. Open a command prompt (or PowerShell) as Administrator and use the `route` command to examine the current routing table:

`route print`

Look at the available routes. What does each entry mean?

Consider the destination network we're trying to reach? What entry needs to be added?

Add an entry for the `192.168.20.0` network. Use `route add /?` to get an explanation for how to add a new route.

The basic syntax is: `route add <detination network id> mask <subnet mask> <gateway>`

`route add 192.168.20.0 mask 255.255.255.0 192.168.30.3`

Try to access `web-routed.local` again. It _should_ work now.

</details>