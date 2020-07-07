---
layout: lab
title: Intro to Labs
category: Network Troubleshooting
order: 0
---
This lab will take you through a few troubleshooting exercises that will demonstrate some of the concepts we've talked about in the training so far, and introduce you to a few troubleshooting tools and how to use them. 

{% assign mylabs = site.labs | where:"category","Network Troubleshooting" %}
<h3>Contents</h3>
  <ul>
    {% assign labs = mylabs | sort: 'order' %}
    {% for lab in labs  %}
      <li><a href="{{ lab.url }}">{{ lab.title }}</a></li>
    {% endfor %}
  </ul>

We're using a VM environment for this. On your lab machine you'll have several VMs arranged according to this diagram:

![Lab2 Diagram](/images/lab2-diagram.png)

You'll need to refer back to this image during these labs, so it might be a good idea to open it in a new tab, or similar.

The environment contains 3 virtual networks:

- **host-net**: This network is shared between the host machine and _some_ of the VMs. It's IP address range is 192.168.30.0/24, and it uses VMWare Workstations built-in NAT to allow internet access from the Lab VMs.
- **routed-net**: This network is VM-only and isolated from all other networks. It's IP address range is 192.168.20.0/24 and it's used to demonstrate IP routing concepts.
- **nat-net**: This network is VM-only and isolated from all other networks. It's IP address range is 192.168.10.0/24 and it's used to demonstrate the NAT concepts.


On each network, we have a **web** VM. This is a simple VM that has Nginx configured to serve an extremely simple web page. We'll use that to demonstrate we have connectivity _to the correct VM_. 

At the boundary of the **nat** and **routed** networks we have another VM which acts as a gateway. Each VM serves a different purpose -- one performs NAT, the other performs routing functions. In real life, you would likely have a hardware appliance to do this, but the functions they perform are the same. 

Finally, we have a **dns-vm** on the host network. This provides some DNS functions for this Lab.

### Lab Machines
So you don't all trip over each other, you've each got a lab machine. The details for which will be handed out during the training session.
