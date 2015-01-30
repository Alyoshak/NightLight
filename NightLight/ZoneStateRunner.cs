using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NightLight
{
    public class ZoneStateRunner
    {
        private List<ZoneState> _States;

        private ZoneController _Controller;

        public ZoneStateRunner(ZoneController controller,List<ZoneState> states)
        {
            _Controller = controller;
            _States = states;
        }
        public void Run()
        {

            if (_States.Count > 0)
            {
                if(_States.Count == 1)
                {
                    ApplyState(_States[0]);
                }

                

                var dateTimeNow = DateTime.Now;
                var dummyState = new ZoneState()
                {
                    DayOfWeek = dateTimeNow.DayOfWeek,
                    Time = dateTimeNow.TimeOfDay
                };
                //Add the current time as a state and then find where it fell in the sort.
                //That way we can determine which one is previous and which one is next. 
                var sortedStates = _States.Union(new ZoneState[] { dummyState }).OrderBy(x => x.DayOfWeek).ThenBy(x => x.Time).ToArray();
                var locationOfDummy = Array.FindIndex(sortedStates, x => x == dummyState);
                int nextIndex = locationOfDummy+1, 
                    previousIndex = locationOfDummy-1;
                //If the dummy state fell between the edges of the array
                if(locationOfDummy == 0)
                {
                    previousIndex = sortedStates.Length - 1;
                }
                else
                {
                    if(locationOfDummy == sortedStates.Length -1)
                    {
                        nextIndex = 0;
                    }
                }
                 var nextState = sortedStates[nextIndex];
                 var previousState = sortedStates[previousIndex];

                var newZoneState = GenerateTransitionState(previousState, nextState, dateTimeNow);
                ApplyState(newZoneState);
                

            }

        }

        public ZoneState GenerateTransitionState(ZoneState start, ZoneState end, DateTime currentTime)
        {
            var startCurrentTime =start.GetCurrentTime(ZoneDirection.Before,currentTime) ;
            var transitionSpan= end.GetCurrentTime(ZoneDirection.After,currentTime) - startCurrentTime;
            var transitionUsage = currentTime - startCurrentTime;
            var transitionPercentage =transitionUsage.TotalMinutes/transitionSpan.TotalMinutes;
            return new ZoneState()
            {
                Brightness = GetTransitionValue(start.Brightness, end.Brightness, transitionPercentage),
                Color = GetTransitionValue(start.Color, end.Color, transitionPercentage),
                On=start.On,
                Off=start.Off
                
            };
        }

        private int? GetTransitionValue(int? startValue, int? endValue, double percentage)
        {
            int? transitionValue = startValue;
            if(startValue.HasValue && endValue.HasValue)
            {
                transitionValue = Convert.ToInt32(startValue.Value + (endValue.Value - startValue.Value) * percentage);
            }
            return transitionValue;
        }
     

        private void ApplyState(ZoneState state)
        {
            if(!state.Off){
                if(state.On)
                {
                    _Controller.On();
                }
                if (state.Color.HasValue)
                {
                    _Controller.SetColor(state.Color.Value);
                }
                if(state.Brightness.HasValue)
                {
                    _Controller.SetBrightness(state.Brightness.Value);
                }
            }
            else{
                _Controller.Off();
            }
        }
    }
}
