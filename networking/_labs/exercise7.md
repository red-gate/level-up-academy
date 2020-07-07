---
layout: lab
title: 7. Inspect nginx logs
category: Network Troubleshooting
order: 7
---
Access `web-host.local` via SSH or the VMWare console.

Use `tail` to view the last n entries in the nginx access log.

`tail -n 50 /var/log/nginx/access.log`

The nginx access log shows information about a web request. You'll see the Source IP, a timestamp, the HTTP Method, the HTTP response code and the User Agent string.

Access `http://web-host.local` using `curl` from `web-routed.local`. Now look at the access log on `web-host.local` to see how the request is logged.

Access `http://web-host.local` using `curl` from `web-nat.local`. Again, look at the access log on `web-host.local`. 

What do you notice about the request log entries?

<details>
<summary markdown="span"> Hint </summary>

Look at the Source IP of the request log, and compare against the IP address of the VM from where the request originated. Can you explain the difference?

</details>

<details>
<summary markdown="span"> Answer </summary>

For the request from `web-routed` you should see that the Source IP is `192.168.20.20`. That is the IP address of that host, so this makes sense.

For the request from `web-nat` you should see that the Source IP is `192.168.30.4`. This is _not_ the IP address of `web-nat`, but it is the IP of `nat-vm`. This is because `web-nat` is "hidden" behind NAT. When the packets from `web-nat` passed through `nat-vm` their source IP header was rewritten to be the IP of `nat-vm`. As a result, as far as `web-host` is concerned, the request came from `nat-vm` not `web-nat`.

</details>