using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightLight
{
    public class ZoneState
    {
        private DayOfWeek _DayOfWeek;

        public DayOfWeek DayOfWeek
        {
            get { return _DayOfWeek; }
            set { _DayOfWeek = value; }
        }

        private TimeSpan _Time;

        public TimeSpan Time
        {
            get { return _Time; }
            set { _Time = value; }
        }
        private int? _Brightness;

        public int? Brightness
        {
            get { return _Brightness; }
            set { _Brightness = value; }
        }
        private int? _Color;

        public int? Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        private bool _Off = false;

        public bool Off
        {
            get { return _Off; }
            set { _Off = value; }
        }

        private bool _On = false;

        public bool On
        {
            get { return _On; }
            set { _On = value; }
        }


        public DateTime GetCurrentTime(ZoneDirection direction, DateTime now)
        {
            DateTime currentTime;
            var daysToChange = (now.DayOfWeek - _DayOfWeek) % 7 * -1;
            if (direction == ZoneDirection.After)
            {
                daysToChange = (_DayOfWeek - now.DayOfWeek + 7) % 7;
            }
            else
            {
                daysToChange = -(now.DayOfWeek - _DayOfWeek) % 7;
            }
            currentTime = now.AddDays(daysToChange).Date.Add(Time);
            if(direction == ZoneDirection.After && currentTime<now)
            {
                currentTime = currentTime.AddDays(7);
            }
            return currentTime;
        }

    }

    public enum ZoneDirection
    {
        Before,
        After
    }
}