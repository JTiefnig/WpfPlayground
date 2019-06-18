using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace CustomSliderControl
{

    enum ActuatorState
    {
        Stop,
        PositionMode
    };


    public class ActuatorViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;



        public double MaximumPosition { get; set; }

        public double TargetPositon { get; set; }

        public double MinimumPosition { get; set; }

        public double Position { get; set; }



        public void GoToTargetPosition()
        {

        }

        
        void SimulateGoToPosition()
        {
            // Schrittweite
            double step = (MaximumPosition - MinimumPosition) / 50;

            

        }



        public static ActuatorViewModel GetRandom()
        {

            var n = new Random();
            var ac = new ActuatorViewModel();

            var min = n.Next(1000);
            ac.MinimumPosition = min;

            var max = n.Next(min + 100, min + 1500);
            ac.MaximumPosition = max;

            ac.TargetPositon = n.Next(min, max);

            ac.Position = n.Next(min, max);


            return ac;
        }


    }
}
