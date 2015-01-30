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
        static void Main(string[] args)
        {
            ZoneController zone1 = new ZoneController(1, "192.168.0.7");
            zone1.On();
            ZoneStateRunner runner = new ZoneStateRunner(zone1,
                new List<ZoneState>{
                    new ZoneState(){
                        Brightness=0,
                        Color=0,
                        DayOfWeek=DayOfWeek.Friday,
                        Time=DateTime.Now.AddMinutes(-1).TimeOfDay
                    },
                    new ZoneState(){

                        Brightness=24,
                        Color=255,
                        DayOfWeek=DayOfWeek.Friday,
                        Time=DateTime.Now.AddMinutes(2).TimeOfDay

                    }
                });

            for (int i = 0; i < 120; i++)
            {
                runner.Run();
                Thread.Sleep(1000);

            }
           
                zone1.Off();
        }

       


       


    }
}
