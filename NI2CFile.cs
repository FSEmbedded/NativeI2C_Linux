/*****************************************************************************/
/***                                                                       ***/
/***      NN    NN             tt     DDDDDD       CCCCCC   UU     UU      ***/
/***      NNN   NN             tt     DD    DD    CC    CC  UU     UU      ***/
/***      NNNN  NN    eeee   tttttt   DD     DD  CC         UU     UU      ***/
/***      NN NN NN  ee    ee   tt     DD     DD  CC         UU     UU      ***/
/***      NN  NNNN  eeeeeeee   tt     DD     DD  CC         UU     UU      ***/
/***      NN   NNN  ee         tt tt  DD    DD    CC    CC  UU     UU      ***/
/***      NN    NN    eeeee     ttt   DDDDDD       CCCCCC     UUUUU        ***/
/***                                                                       ***/
/*****************************************************************************/
/***                                                                       ***/
/***                                                                       ***/
/***         . N E T   A c c e s s   f o r   N a t i v e I 2 C             ***/
/***                                                                       ***/
/***                                                                       ***/
/*****************************************************************************/
/*** File:     NI2CFile.cs                                                 ***/
/*** Author:   Hartmut Keller                                              ***/
/*** Created:  23.08.2007                                                  ***/
/*** Modified: 24.08.2007 17:37:06 (HK)                                    ***/
/*** Modified: 11.05.2023 14:11:58 (DD)                                    ***/
/*** Modified: 13.02.2025 11:06:28 (BS)                                    ***/
/***                                                                       ***/
/*** Description:                                                          ***/
/*** Wrapper class for using the NI2C bus interface of the NetDCUx in .NET ***/
/*** environment (C#, VB, using the Compact Framework).                    ***/
/***                                                                       ***/
/*** Modification History:                                                 ***/
/*** 11.05.2023 DD: Fixed Error in handleError()                           ***/
/***                Changed GetLastWin32Error() with GetLastPInvokeError() ***/
/*** 13.02.2025 BS: Changed GetLastPInvokeError() with GetLastWin32Error() ***/
/***                GetLastPInvokeError() could not be called from Mono	   ***/
/*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace FS.NetDCU
{
    //------------------- NI2CException Class ----------------------------------

    /*************************************************************************
     *** Class:      NI2CException : Exception                             ***
     ***                                                                   ***
     *** Description:                                                      ***
     *** ------------                                                      ***
     *** Exception including some error code. Provide every parameter com- ***
     *** bination that is possible.                                        ***
     *************************************************************************/
    public class NI2CException : ApplicationException
    {
        #region NI2CException: Member variables
        /* Error value, usually from Marshal.GetLastPInvokeError() */
        private int reason;

        public int Reason
        {
            get
            {
                return reason;
            }
        }
        #endregion

        #region NI2CException: Construction and Destruction
        /*********************************************************************
         *** Constructor: NI2CException(string text, int reason,           ***
         ***                                             Exception inner)  ***
         ***                                                               ***
         *** Parameters:  text:   Error text                               ***
         ***              reason: Error value                              ***
         ***              inner:  Inner exception                          ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Use given error value and error text.                         ***
         *********************************************************************/
        public NI2CException(string text, int reason, Exception inner)
            : base(text + ": Error code " + reason.ToString(), inner)
        {
            this.reason = reason;
        }


        /*********************************************************************
         *** Constructor: NI2CException(string text, int reason)           ***
         ***                                                               ***
         *** Parameters:  text:   Error text                               ***
         ***              reason: Error value                              ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Use given error value and error text.                         ***
         *********************************************************************/
        public NI2CException(string text, int reason)
            : this(text, reason, null)
        { }


        /*********************************************************************
         *** Constructor: NI2CException(string text)                       ***
         ***                                                               ***
         *** Parameters:  text:  Error text                                ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Use Marshal.GetLastPInvokeError() and given error text.       ***
         *********************************************************************/
        public NI2CException(string text)
            : this(text, Marshal.GetLastWin32Error())
        { }


        /*********************************************************************
         *** Constructor: NI2CException(string text, Exception inner)      ***
         ***                                                               ***
         *** Parameters:  text:  Error text                                ***
         ***              inner: Inner exception                           ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Use Marshal.GetLastPInvokeError();() and given error text.    ***
         *********************************************************************/
        public NI2CException(string text, Exception inner)
            : this(text, Marshal.GetLastWin32Error())
        { }


        /*********************************************************************
         *** Constructor: NI2CException(int reason)                        ***
         ***                                                               ***
         *** Parameters:  reason: Error value                              ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Use given error value and "System error".                     ***
         *********************************************************************/
        public NI2CException(int reason)
            : this("System error", reason)
        { }


        /*********************************************************************
         *** Constructor: NI2CException(int reason, Exception inner)       ***
         ***                                                               ***
         *** Parameters:  reason: Error value                              ***
         ***              inner:  Inner exception                          ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Use given error value and "System error".                     ***
         *********************************************************************/
        public NI2CException(int reason, Exception inner)
            : this("System error", reason, inner)
        { }


        /*********************************************************************
         *** Constructor: NI2CException()                                  ***
         ***                                                               ***
         *** Parameters:  -                                                ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Use Marshal.GetLastPInvokeError() and "System error".         ***
         *********************************************************************/
        public NI2CException()
            : this(Marshal.GetLastWin32Error())
        { }


        /*********************************************************************
         *** Constructor: NI2CException(Exception inner)                   ***
         ***                                                               ***
         *** Parameters:  inner: Inner exception                           ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Use Marshal.GetLastPInvokeError() and "System error".         ***
         *********************************************************************/
        public NI2CException(Exception inner)
            : this(Marshal.GetLastWin32Error(), inner)
        { }
        #endregion
    } // class NI2CException


    //------------------- CanPort Class -------------------------------------------

    /*************************************************************************
     *** Class:      NI2CFile                                              ***
     ***                                                                   ***
     *** Description:                                                      ***
     *** ------------                                                      ***
     *** Wrapper class to use the Win32 file interface to the NI2C devices ***
     *** I2C1: etc.                                                        ***
     *************************************************************************/
    public class NI2CFile
    {
        #region NI2CFile: IOCTL values
        /* IOCTL definitions */
        internal const UInt32 METHOD_BUFFERED = 0;
        internal const UInt32 METHOD_IN_DIRECT = 1;
        internal const UInt32 METHOD_OUT_DIRECT = 2;
        internal const UInt32 METHOD_NEITHER = 3;

        internal const UInt32 FILE_ANY_ACCESS = (0 << 14);
        internal const UInt32 FILE_READ_ACCESS = (1 << 14);    // file & pipe
        internal const UInt32 FILE_WRITE_ACCESS = (2 << 14);   // file & pipe

        internal const UInt32 DEVICE_NI2C = (0x00008037U << 16);

        internal const UInt32 IOCTL_NI2C_SCHEDULE =
            DEVICE_NI2C | FILE_ANY_ACCESS | (0x800 << 2) | METHOD_BUFFERED;
        internal const UInt32 IOCTL_NI2C_GET_RESULT =
            DEVICE_NI2C | FILE_ANY_ACCESS | (0x801 << 2) | METHOD_BUFFERED;
        internal const UInt32 IOCTL_NI2C_SKIP_RESULT =
            DEVICE_NI2C | FILE_ANY_ACCESS | (0x802 << 2) | METHOD_BUFFERED;
        internal const UInt32 IOCTL_NI2C_CHECK_RESULT =
            DEVICE_NI2C | FILE_ANY_ACCESS | (0x803 << 2) | METHOD_BUFFERED;
        internal const UInt32 IOCTL_NI2C_GET_CLKFREQ =
            DEVICE_NI2C | FILE_ANY_ACCESS | (0x804 << 2) | METHOD_BUFFERED;
        #endregion

        #region NI2CFile: Constants and enumerations
        /* File access */
        internal const Int32 INVALID_HANDLE_VALUE = -1;
        internal const UInt32 OPEN_EXISTING = 3;

        /* Error values from API file calls. These can be used when checking
         * the return value. */
        public enum APIError : int
        {
            ERROR_FILE_NOT_FOUND = 2,     // Port not found
            ERROR_ACCESS_DENIED = 5,      // Access denied
            ERROR_INVALID_HANDLE = 6,     // Invalid handle
            ERROR_NOT_READY = 21,         // Device not ready
            ERROR_WRITE_FAULT = 27,       // Write fault
            ERROR_DEV_NOT_EXIST = 55,     // Device does not exist
            ERROR_INVALID_PARAMETER = 87, // Bad parameters
            ERROR_INVALID_NAME = 123,     // Invalid port name
        }

        /* Access to the device used in constructor */
        [Flags]
        public enum NI2CAccess : uint
        {
            QUERY = 0x00000000,
            WRITE = 0x40000000,
            READ = 0x80000000,
            READ_WRITE = WRITE | READ
        }

        /* Flag values for chFlags */
        [Flags]
        public enum NI2C_FLAGS : uint
        {
            LASTBYTE_ACK = 0x01,          /* Receive: Send ACK on last byte;
                                             Transmit: Got ACK on last byte */
            DATA_NAK = 0x02,              /* No ACK when sending data */
            DEVICE_NAK = 0x04,            /* No ACK when talking to device */
            ARBITRATION_LOST = 0x08,      /* Lost arbitration */
            TIMEOUT = 0x80,               /* Timeout on I2C bus */
        }
        #endregion

        #region NI2CFile: Structures
        /* A transmission request defines a group of messages to be sent
           and/or received on the I2C bus. Each message can individually send
           or receive to/from any device address with any length. The driver
           will use RESTART between the messages and therefore handles all
           messages of the transmission request in one go, without letting
           other tasks interrupt the transfer. This allows time critical
           transfers on one hand, but on the other hand can also block the bus
           for quite some time. So try to be fair and split transfers in
           different requests whenever possible.

           A transmission request consists of two parts:

           1. An array of message headers, defining the parameters of the
              messages. Each message header describes the 7-bit address of the
              device to communicate with, the transfer direction (send/receive
              as the eighth bit of the address), and the message length. On
              receiving messages you can determine by setting a flag whether
              the last received byte should be acknowledged or not.

           2. A byte array containing the concatenated bytes of all messages.
              For receiving messages you have to provide as many dummy bytes
              with arbitrary content.

           Example:
           --------
           msg1: Send three bytes 0x01, 0x02, 0x03 to device 0x40
           msg2: Receive two bytes from device 0x40, don't send ACK on last byte
           msg3: Send two bytes 0x04, 0x05 to device 0x78
           msg4: Receive three bytes from device 0x78, send ACK on last byte


           Message Array[]:  chDevAddr chFlags wLen
           -----------------------------------------------------------
           0            0x40      0x00    0x0003 (msg1, send)
           1            0x41      0x00    0x0002 (msg2, receive, no ACK)
           2            0x78      0x00    0x0002 (msg3, send)
           3            0x79      0x01    0x0003 (msg4, receive, ACK)

           Byte Array[]:     Content
           -----------------------------------------------------------
           0            0x01 (msg1, 1st byte, send)
           1            0x02 (msg1, 2nd byte, send)
           2            0x03 (msg1, 3rd byte, send)
           3            0x00 (msg2, 1st dummy byte, receive)
           4            0x00 (msg2, 2nd dummy byte, receive)
           5            0x04 (msg3, 1st byte, send)
           6            0x05 (msg3, 2nd byte, send)
           7            0x00 (msg4, 1st dummy byte, receive)
           8            0x00 (msg4, 2nd dummy byte, receive)
           9            0x00 (msg4, 3rd dummy byte, receive)

           A transmission request can be scheduled by Schedule().
           This means the request is stored in the driver and transferred
           asynchronously. The call returns immediately. By using
           GetResult() and the same parameters, the result can be
           obtained later when the transfer is finished. After return, the
           special flags entry in the message headers is valid and reports the
           transfer status of each message individually. And also the byte
           array now contains the received bytes.

           You can schedule several requests in a row with Schedule() before
           obtaining the results with GetResult(). But please note that you
           will get the results in the same sequence as you had scheduled the
           requests before and it is important to provide the same parameter
           array sizes with the result call as with the corresponding schedule
           call, or GetResult() will fail. */
        [StructLayout(LayoutKind.Sequential)]
        public struct NI2C_MSG_HEADER
        {
            public byte chDevAddr;
            public byte chFlags;
            public ushort wLen;
            public NI2C_MSG_HEADER(byte chDevAddr, byte chFlags, ushort wLen)
            {
                this.chDevAddr = chDevAddr;
                this.chFlags = chFlags;
                this.wLen = wLen;
            }
        }
        #endregion

        #region NI2CFile: Member variables
        /* Device file handle; required in each of our API calls. */
        protected IntPtr hPort = (IntPtr)INVALID_HANDLE_VALUE;

        /* Error handling via return value (C style) or by exceptions */
        protected bool bCStyle = false;
        #endregion

        #region NI2CFile: Construction, Destruction and Error Handling
        /*********************************************************************
         *** Constructor: NI2CFile(string FileName, NI2CAccess access)     ***
         ***                                                               ***
         *** Parameters:  FileName: Name of the device ("I2C1:")           ***
         ***              access:   Any combination of GENERIC_WRITE and   ***
         ***                        GENERIC_READ                           ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Open the device file. Throw an exception if it fails.         ***
         *********************************************************************/
        public NI2CFile(string FileName, NI2CAccess access)
        {
            hPort = CECreateFileW(FileName, (UInt32)access, 0, IntPtr.Zero,
                                  OPEN_EXISTING, 0, IntPtr.Zero);
            if (hPort == (IntPtr)INVALID_HANDLE_VALUE)
            {
                throw new NI2CException("NI2CFile construction failed");
            }
        }


        /*********************************************************************
         *** Destructor: ~NI2CFile()                                       ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Close the device file (if open).                              ***
         *********************************************************************/
        ~NI2CFile()
        {
            if (hPort != (IntPtr)INVALID_HANDLE_VALUE)
                CECloseHandle(hPort);
        }


        /*********************************************************************
         *** Function: void HandleErrorsViaReturn(bool bCStyle)            ***
         ***                                                               ***
         *** Parameters: bCStyle: TRUE: Return error as return value       ***
         ***                      FALSE: Throw exception on error (default)***
         ***                                                               ***
         *** Return:    -                                                  ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Set the error reporting method.                               ***
         *********************************************************************/
        public void HandleErrorsViaReturn(bool bCStyle)
        {
            this.bCStyle = bCStyle;
        }


        /*********************************************************************
         *** Function: int HandleError(int success, string errorfunction)  ***
         ***                                                               ***
         *** Parameters: success:       Return value of API function:      ***
         ***                            0: Success, !=0: Failure           ***
         ***             errorfunction: Name of function that failed       ***
         ***                                                               ***
         *** Return:     0: Success                                        ***
         ***             !=0: Error from GetLastWin32Error()               ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Depending on the error reporting method,  return the error of ***
         *** GetLastWin32Error() directly or throw an exception.           ***
         *** This function is meant to be called directly with the result  ***
         *** of the API function. Example:                                 ***
         ***                                                               ***
         ***    return HandleError(CESetCommMask(hPort, mask),             ***
         ***                       "SetCommMask() failed");                ***
         *********************************************************************/
        protected int HandleError(int success, string errorfunction)
        {
            if (success != 0)
            {
                if (bCStyle)
                    return Marshal.GetLastWin32Error();
                throw new NI2CException(errorfunction + " failed");
            }
            return 0;
        }


        /*********************************************************************
         *** Function: int HandleError(string errorfunction, int reason)   ***
         ***                                                               ***
         *** Parameters: errorfunction: Name of function that failed       ***
         ***             reason:        Error value                        ***
         ***                                                               ***
         *** Return:     reason (as given)                                 ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Depending on the error reporting method, return the value gi- ***
         *** ven in reason or throw an exception.                          ***
         *********************************************************************/
        protected int HandleError(string errorfunction, int reason)
        {
            if (bCStyle)
                return reason;
            throw new NI2CException(errorfunction + " failed", reason);
        }
        #endregion

        #region NI2CFile: Member functions using DeviceIoControl()

        /*********************************************************************
         *** Function:   int Schedule(NI2C_MSG_HEADER[] msg, byte[] data)  ***
         ***                                                               ***
         *** Parameters: msg:  Array of message headers                    ***
         ***             data: Array with data bytes of all messages       ***
         ***                                                               ***
         *** Return:     0: Success                                        ***
         ***             !=0: Error from Marshal.GetLastPInvokeError()     ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Schedule a transmission request. This may consist of several  ***
         *** seperate messages.                                            ***
         *********************************************************************/
        public int Schedule(NI2C_MSG_HEADER[] msg, byte[] data)
        {
            Int32 bytesreturned;

            bytesreturned = data.Length;

            return HandleError(
                CEDeviceIoControl(hPort, IOCTL_NI2C_SCHEDULE,
                                  msg, msg.Length * Marshal.SizeOf(typeof(NI2C_MSG_HEADER)),
                                  data, data.Length,
                                  out bytesreturned, IntPtr.Zero),
                "Schedule()");
        }

        /*********************************************************************
         *** Function:   int GetResult(NI2C_MSG_HEADER[] msg, byte[] data) ***
         ***                                                               ***
         *** Parameters: msg:  Array of message headers                    ***
         ***             data: Array with data bytes of all messages       ***
         ***                                                               ***
         *** Return:     0: Success                                        ***
         ***             !=0: Error from Marshal.GetLastPInvokeError()     ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Fill the result data into the given arrays.  The arrays must  ***
         *** have the same size as when this transmission was scheduled or ***
         *** this function will fail.                                      ***
         *********************************************************************/
        public int GetResult(NI2C_MSG_HEADER[] msg, byte[] data)
        {
            Int32 bytesreturned;

            return HandleError(
                CEDeviceIoControl(hPort, IOCTL_NI2C_GET_RESULT,
                                  msg, msg.Length * Marshal.SizeOf(typeof(NI2C_MSG_HEADER)),
                                  data, data.Length,
                                  out bytesreturned, IntPtr.Zero),
                "GetResult()");
        }

        /*********************************************************************
         *** Function:   int SkipResult()                                  ***
         ***                                                               ***
         *** Parameters: -                                                 ***
         ***                                                               ***
         *** Return:     0: Success                                        ***
         ***             !=0: Error from Marshal.GetLastPInvokeError()     ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Skip the next available result. This function may block if no ***
         *** transmission is already finished (still in progress).         ***
         *********************************************************************/
        public int SkipResult()
        {
            return HandleError(
                CEDeviceIoControl(hPort, IOCTL_NI2C_SKIP_RESULT,
                                  IntPtr.Zero, 0,
                                  IntPtr.Zero, 0,
                                  IntPtr.Zero, IntPtr.Zero),
                "SkipResult()");
        }

        /*********************************************************************
         *** Function:   int CheckResult()                                 ***
         ***                                                               ***
         *** Parameters: -                                                 ***
         ***                                                               ***
         *** Return:     0: No result available                            ***
         ***             1: At least one result available                  ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Check if at least one transmission is complete and the result ***
         *** available.                                                    ***
         *********************************************************************/
        public int CheckResult()
        {
            return CEDeviceIoControl(hPort, IOCTL_NI2C_CHECK_RESULT,
                                     IntPtr.Zero, 0,
                                     IntPtr.Zero, 0,
                                     IntPtr.Zero, IntPtr.Zero);
        }

        /*********************************************************************
         *** Function:   int GetClockFreq()                                ***
         ***                                                               ***
         *** Parameters: -                                                 ***
         ***                                                               ***
         *** Return:     Speed of I2C bus (in Hz)                          ***
         ***                                                               ***
         *** Description:                                                  ***
         *** ------------                                                  ***
         *** Get the current clock speed of the native I2C bus.            ***
         *********************************************************************/
        public int GetClockFreq()
        {
            return CEDeviceIoControl(hPort, IOCTL_NI2C_GET_CLKFREQ,
                                     IntPtr.Zero, 0,
                                     IntPtr.Zero, 0,
                                     IntPtr.Zero, IntPtr.Zero);
        }

        #endregion

        #region NI2CFile: Windows CE API imports
        // CreateFileW()
        [DllImport("coredll.dll", EntryPoint = "CreateFileW",
                   SetLastError = true)]
        private static extern
        IntPtr CECreateFileW(String lpFileName, UInt32 dwDesiredAccess,
                             UInt32 dwShareMode, IntPtr lpSecurityAttributes,
                             UInt32 dwCreationDisposition,
                             UInt32 dwFlagsAndAttributes,
                             IntPtr hTemplateFile);

        // CloseHandle()
        [DllImport("coredll.dll", EntryPoint = "CloseHandle",
                   SetLastError = true)]
        private static extern
        int CECloseHandle(IntPtr hObject);

        // Several overloaded versions of DeviceIoControl()
        // Schedule(), GetResult()
        [DllImport("coredll.dll", EntryPoint = "DeviceIoControl",
                   SetLastError = true)]
        private static extern
        int CEDeviceIoControl(IntPtr hFile, UInt32 dwIoControlCode,
                              NI2C_MSG_HEADER[] lpInBuffer,
                              Int32 nInBufferSize,
                              byte[] lpOutBuffer, Int32 nOutBufferSize,
                              out Int32 lpBytesReturned, IntPtr lpOverlapped);

        // SkipResult(), CheckResult(), GetClockFreq()
        [DllImport("coredll.dll", EntryPoint = "DeviceIoControl",
                   SetLastError = true)]
        private static extern
        int CEDeviceIoControl(IntPtr hFile, UInt32 dwIoControlCode,
                              IntPtr lpInBuffer, Int32 nInBufferSize,
                              IntPtr lpOutBuffer, Int32 nOutBufferSize,
                              IntPtr lpBytesReturned, IntPtr lpOverlapped);
        #endregion


    } // class NI2CFile
} // namespace FS.NetDCU
