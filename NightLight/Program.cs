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
            Thread.Sleep(DateTime.Parse("1/30/2015 6:40") - DateTime.Now);
            ZoneStateRunner runner = new ZoneStateRunner(zone1,
                new List<ZoneState>{
                    new ZoneState(){
                        On=true,
                        Brightness=0,
                        Color=255,
                        DayOfWeek=DayOfWeek.Friday,
                        Time=new TimeSpan(6,40,0)
                    },
                    new ZoneState(){
                        Off=true,
                        Brightness=24,
                        Color=180,
                        DayOfWeek=DayOfWeek.Friday,
                        Time=new TimeSpan(7,30,0)
                    }
                });

            while (DateTime.Now < DateTime.Parse("1/30/2015 7:30"))
            {
                runner.Run();
                Thread.Sleep(6000);

            }
           
        }

       


       


    }
}
