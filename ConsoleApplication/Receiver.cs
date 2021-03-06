﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Msg
    {
        public Int64 duration;                        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
        public byte[] note;	                   
    }

    class UtileFunction
    {
        static public string ByteToString(byte[] v_byte)
        {
            if (v_byte == null)
                return "";
            string v_BarNo = System.Text.Encoding.Default.GetString(v_byte);
            int len = v_BarNo.IndexOf('\0');
            if (len >= 0)
            {
                return v_BarNo.Substring(0, len);
            }
            return v_BarNo;
        }
    }


    class Receiver
    {
        public static int AlarmCallBack(IntPtr pData, int dataLen)
        {
            byte[] msgStream = new byte[dataLen];
            Marshal.Copy(pData, msgStream, 0, dataLen);
            Msg msg = new Msg();
            msg = (Msg)InteroperateFunction.BytesToStruct(msgStream,msg.GetType());
            Console.WriteLine("Note:{0}, duration:{1}", UtileFunction.ByteToString(msg.note),msg.duration);
            return 0;
        }
    }
}
