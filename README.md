# NativeI2C for Linux
Wrapper class for using the NI2C bus interface of the F&S boards in .NET environment on Linux.

This project is intended to give the customers of F&S a unified software interface for I²C in .NET projects, regardless of using Windows or Linux as operating system.
The code shown here is the Linux equivalent to the software available at F&S: [Software .NET Drivers](https://www.fs-net.de/en/embedded-modules/accessories/software-net-drivers/)

# How to use this code
You can either implement NI2CFile.cs as a class into your existing project, or compile a DLL which can be placed on your device alongside your project binaries.

If you have an existing WinForms application that is using NativeI2C.dll for Windows, you can run this application under Linux using [Mono](https://www.mono-project.com/). Keep in mind, that Mono only supports .NET up to Framework 4.7! Replace the windows specific NativeI2C.dll with the file provided here.
You can also build it yourself:

```
csc.exe /target:library /out:NativeI2C.dll /reference:System.dll .\NI2CFile.cs
```

In your application code, you need to change the device address, for example ***I2C2:*** could be called with ***/dev/i2c-2*** in Linux, depending on your hardware and pin connections.

When using Mono, you also need need a [special library](https://github.com/FSEmbedded/dotnet_linux_IO_API) provided by F&S.
Also have a look at [NativeSPI-V1 for Linux](https://github.com/FSEmbedded/NativeSPI-V1_Linux) and this [demo application](https://github.com/FSEmbedded/WinForms_On_Linux_InterfaceDemo).

# DISCLAIMER
We tested this software on our boards with some test applications. However, this code may contain errors and is not production ready yet!
SEE IT AS A PROOF OF CONCEPT AND USE AT YOUR OWN RISK.