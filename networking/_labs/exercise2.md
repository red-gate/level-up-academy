---
layout: lab
title: 2. Access web-host
category: Network Troubleshooting
order: 2
---
From the host, open Edge and go to http://web-host.local. Does it work? Why? 

<details>
<summary markdown="span"> Hint </summary>

Try browsing using the IP address instead of the hostname. Does that make a difference?

</details>

<details>
<summary markdown="span"> Hint </summary>

What can you learn by running `nslookup web-host.local`?

</details>

<details>


<summary markdown="span"> Answer </summary>

Performing `nslookup web-host.local` reveals that the hostname cannot be found:

```
PS C:\Users\rgadmin> nslookup web-host.local
Server:  dns-vm.local
Address:  192.168.30.20

*** dns-vm.local can't find web-host.local: Non-existent domain
```

This message tells you the DNS server that was used to make the request and the result of the query.

The host can't resolve the hostname to an IP address, because there's a DNS entry missing. 

In this example, we're using [BIND](https://www.isc.org/bind/) to act as our DNS server. There are many others out there, for example, Windows Server has a DNS role which is typically used in Active Directory deployments. 

To fix our example, SSH to `dns-vm.local` and navigate to the BIND config directory containing the zone definitions.

`cd /etc/bind/zones`

We're going to edit the `.local` zone file to add the missing DNS entry.

`sudo nano db.local`

Then add the following line to the bottom of the file

`web-host.local.    IN      A       192.168.30.30`

Save and exit (`ctrl+x`, `y`, `[Enter]`)

This adds a DNS A record for `web-host.local.` and points it to `192.168.30.30`. The trailing `.` after `local` here is important because that signifies that this is a fully qualified domain name.

Now restart `named` for the changes to take effect

`sudo service named restart`

Try `nslookup web-host.local` from the host machine again -- does it work now? What about `http://web-host.local` in the browser?

</details>

