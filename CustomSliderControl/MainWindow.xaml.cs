using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.Specialized;

namespace CustomSliderControl
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public Seatbuckviewmodel SIKI { get; set; }



        public MainWindow()
        {
            InitializeComponent();

            SIKI = new Seatbuckviewmodel();
            DataContext = SIKI;

            SIKI.PropertyChanged += myfunction;

            SIKI.Actuators.CollectionChanged += myfunction2;
           
        }

       


        void myfunction2(object sender, NotifyCollectionChangedEventArgs e)
        {
            var test = e.NewItems;
        }


        void myfunction(object sender, PropertyChangedEventArgs e)
        {
       



        }




        private void Testbutton_Click(object sender, RoutedEventArgs e)
        {
            //var sb = this.myrect.Resources["mystory"] as Storyboard;
            // sb.Begin();


            var rnd = new Random();

            foreach(var ac in SIKI.Actuators)
            {

                ac.TargetPositon = rnd.Next((int)ac.MinimumPosition, (int)ac.MaximumPosition);

            }



        }





        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            SIKI.Actuators.Add(ActuatorViewModel.GetRandom());
        }

        private void MultiSelectBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var delList = new List<ActuatorViewModel>();
            foreach (var ac in this.MultiSelectBox.SelectedItems)
                delList.Add(ac as ActuatorViewModel);

            foreach(var ac in delList)
            {
                try
                {
                    SIKI.Actuators.Remove(ac);
                }catch
                {
                    // just try the next^^
                }

            }


        }
    }



}
