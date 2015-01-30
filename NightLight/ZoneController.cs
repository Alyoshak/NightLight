using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NightLight
{
    public  class ZoneController
    {
        private static int UDP_PORT = 8899;
        private static int MESSAGE_OFFSET = 85;
        private static int COLOR_CODE = 64;
        private static int BRIGHTNESS_CODE = 78;
        private static int DISCO_CODE = 77;
        private static int DISCO_INC_CODE = 68;
        private static int DISCO_DEC_CODE = 67;

        private int _Zone;
        private string _IpAddress;

        public void On()
        {
            int[] codes = { 66, 69, 71, 73, 75 };
            SendUDPMessage(codes[_Zone]);
            Thread.Sleep(100);
        }
        public void Off()
        {
            int[] codes = { 65, 70, 72, 74, 76 };
            Thread.Sleep(100);
            SendUDPMessage(codes[_Zone]);
        }

        public ZoneController(int zone, string ipAddress)
        {
            _Zone = zone;
            _IpAddress = ipAddress;
        }
        public void SetBrightness( int brightness)
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

        private void SendWhiteCommand()
        {
            int[] codes = { 194, 197, 199, 201, 203 };
            SendUDPMessage(codes[_Zone]);
        }

        private void SendUDPMessage(int code, int subcode = 0)
        {
            UdpClient udpClient = new UdpClient(_IpAddress, UDP_PORT);
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

        public void SetColor(int color)
        {
            SendUDPMessage(COLOR_CODE, color);
        }

        private void Disco()
        {
            SendUDPMessage(DISCO_CODE);
        }
        private void DiscoInc()
        {
            SendUDPMessage(DISCO_INC_CODE);
        }
        private void DiscoDec()
        {
            SendUDPMessage(DISCO_DEC_CODE);
        }


    }
}
