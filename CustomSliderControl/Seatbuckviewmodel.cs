using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace CustomSliderControl
{
    public class Seatbuckviewmodel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public Seatbuckviewmodel()
        {
            radomizelist();

        }



        public ObservableCollection<ActuatorViewModel> Actuators { get; set; }


        public void radomizelist()
        {
            var newcol = new ObservableCollection<ActuatorViewModel>();
            var n = new Random();

            for (int i = 0; i < 7; i++)
            {
                var ac = new ActuatorViewModel();

                var min =n.Next(1000);
                ac.MinimumPosition = min;

                var max= n.Next(min+100, min + 1500);
                ac.MaximumPosition = max;

                ac.TargetPositon = n.Next(min , max);

                ac.Position = n.Next(min , max);


                newcol.Add(ac);
            }

            Actuators = newcol;
        }


    }
}
