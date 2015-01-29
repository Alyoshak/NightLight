using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NightLight
{
    class Program
    {
        private static string UDP_IP = "192.168.0.7";
        private static int UDP_PORT = 8899;
        private static int MESSAGE_OFFSET = 85;
        private static int COLOR_CODE = 64;
        private static int BRIGHTNESS_CODE = 78;
        private static int DISCO_CODE = 77;
        private static int DISCO_INC_CODE = 68;
        private static int DISCO_DEC_CODE = 67;

        static void Main(string[] args)
        {
            SendOnCommand(1);

            SendColorCommand(1, 255);
            Thread.Sleep(2000);
            SendWhiteCommand(1);

            Thread.Sleep(2000);
            //SendDiscoCommand(1);
            //Thread.Sleep(2000);
            //SendDiscoIncCommand(1);
            //Thread.Sleep(2000);
            //SendDiscoDecCommand(1);
            Thread.Sleep(2000);
            for (int i = 0; i < 100; i++ )
            {
                SendBrightnessCommand(1, i);
                Thread.Sleep(1000);
            }
                SendOffCommand(1);
        }

        private static void SendWhiteCommand(int zone)
        {
            int[] codes = { 194, 197, 199, 201, 203 };
            SendUDPMessage(codes[zone]);
        }

        private static void SendUDPMessage(int code, int subcode = 0)
        {
            UdpClient udpClient = new UdpClient(UDP_IP, UDP_PORT);
            Byte[] sendBytes = new byte[] { (byte)code, (byte)subcode, (byte)MESSAGE_OFFSET };
            try
            {
                udpClient.Send(sendBytes, sendBytes.Length);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        private static void SendColorCommand(int zone, int color)
        {
            SendUDPMessage(COLOR_CODE, color);
        }

        private static void SendOnCommand(int zone)
        {
            int[] codes = { 66, 69, 71, 73, 75 };
            SendUDPMessage(codes[zone]);
            Thread.Sleep(500);
        }
        private static void SendOffCommand(int zone)
        {
            int[] codes = { 65, 70, 72, 74, 76 };
            Thread.Sleep(500);
            SendUDPMessage(codes[zone]);
        }
        private static void SendBrightnessCommand(int zone, int brightness)
        {
            if (brightness > 25)
            {
                Console.WriteLine("Brightness needs to be 0-24");
            }
            else
            {
                SendUDPMessage(BRIGHTNESS_CODE, brightness);
            }
        }

        private static void SendDiscoCommand(int zone)
        {
            SendUDPMessage(DISCO_CODE);
        }
        private static void SendDiscoIncCommand(int zone)
        {
            SendUDPMessage(DISCO_INC_CODE);
        }
        private static void SendDiscoDecCommand(int zone)
        {
            SendUDPMessage(DISCO_DEC_CODE);
        }


    }
}
