# TcpClientSendReceive
TcpClient comunication client that sends data on one thread and listening on another thread.  

This is basically a copy of [Matt Davis implementation on StackOverflow](https://stackoverflow.com/a/20698153/1187583) with lots of added stuff.


# How to run?
There are two ways you can run this.

1. Run up Console and TcpServerProgram and you will see data flow between each other.
2. Run up WinForms and TcpServerProgram and you'll see the same.

# Known problems at the moment
1. All the data I'm sending to the service gets concatinated into one big (1024 byte) string. This is something I was going to research with the WinForms project.

2. If I run the WinForms client I can´t get the UI to update with data. That is probably because of some threading issues that should be possible to fix with some async stuff.

## Want to help me?
I would appriciate all the help/pointers you can offer!
