using System;
using System.Collections.Generic;
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

namespace CustomSliderControl
{
    /// <summary>
    /// Interaktionslogik für PositionSlider.xaml
    /// </summary>
    public partial class PositionSlider : UserControl
    {
        public PositionSlider()
        {
            InitializeComponent();

            this.Slider.TouchDown += new EventHandler<TouchEventArgs>(TouchableThing_TouchDown);

            this.TouchMove += new EventHandler<TouchEventArgs>(TouchableThing_TouchMove);

            this.TouchLeave += new EventHandler<TouchEventArgs>(TouchableThing_TouchLeave);

            this.Slider.MouseDown += SliderMouseDown;
            this.MouseMove += SliderMouseMove;

            this.MouseLeave += SliderMouseLeave;
            this.MouseUp += SliderMouseLeave;

        }


        bool _isDragging=false;
        public bool IsDragging
        {
            get => _isDragging;
            set
            {
                _isDragging = value;

                //TargetPositonPopup.IsOpen = value;
                
            }
        }

        #region Event handling

        void TouchableThing_TouchDown(object sender, TouchEventArgs e)
        {
            IsDragging = true;
        }

        void TouchableThing_TouchMove(object sender, TouchEventArgs e)
        {
            
        }

        void TouchableThing_TouchLeave(object sender, TouchEventArgs e)
        {
            IsDragging = false;
        }


        private void SliderMouseDown(object sender, MouseEventArgs e)
        {
            IsDragging = true;
        }

        private void SliderMouseMove(object sender, MouseEventArgs e)
        {
            if (!IsDragging)
                return;

            var myPoint = (e as MouseEventArgs).GetPosition(this.BasicRect);

            var ps =  ScreenPositonToTargetPosition(myPoint.X);

            Actuator.TargetPositon = ps;


        }

        private void SliderMouseLeave(object sender, MouseEventArgs e)
        {
            IsDragging = false;
        }
        #endregion




        #region Dependency Property

        public static readonly DependencyProperty ActuatorProperty =
        DependencyProperty.Register(nameof(Actuator),
        typeof(ActuatorViewModel),
        typeof(PositionSlider),
        new FrameworkPropertyMetadata(null, new PropertyChangedCallback(ActuatorChanged)));


        public ActuatorViewModel Actuator
        {
            get => (ActuatorViewModel)GetValue(ActuatorProperty);
            set
            {
                SetValue(ActuatorProperty, value);
            }
        }

        public static void ActuatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var obj = e.NewValue as ActuatorViewModel;

            var posSlid = d as PositionSlider;

            // buggy
            posSlid.DataContext = obj;
            // do some testing
            // do nothing for now

            var targetBinding = new Binding(nameof(ActuatorViewModel.TargetPositon));

            targetBinding.Source = obj;
            targetBinding.Mode = BindingMode.TwoWay;
            // Bind the new data source to the myText TextBlock control's Text dependency property.
            posSlid.SetBinding(PositionSlider.TargetPositionProperty, targetBinding);


        }



        //private Binding targetBinding;



        public static readonly DependencyProperty TargetPositionProperty =
        DependencyProperty.Register(nameof(TargetPosition),
        typeof(double),
        typeof(PositionSlider),
        new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(TargetPositionChanged)));

        
        public double TargetPosition
        {
            get => (double)GetValue(TargetPositionProperty);
            set
            {
                SetValue(TargetPositionProperty, value);
            }
        }

        public static void TargetPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var posSlid = d as PositionSlider;


            if (posSlid.IsDragging)
            {
                posSlid.BeginAnimation(PositionSlider.SliderPositionProperty, null);
                posSlid.SliderPosition = posSlid.TargetPositionToScreenPosition();
            }
            else
                posSlid.StartSlideAnimation(posSlid.TargetPositionToScreenPosition());

        }


        void StartSlideAnimation(double target)
        {

            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.To = target;

           
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(100));

            
            this.BeginAnimation(PositionSlider.SliderPositionProperty, myDoubleAnimation);

           

        }



        //private DoubleAnimation SlideAnimation { get; set; }


        public static readonly DependencyProperty SliderPositionProperty =
        DependencyProperty.Register(nameof(SliderPosition),
        typeof(double),
        typeof(PositionSlider),
        new FrameworkPropertyMetadata(0.0, new PropertyChangedCallback(SliderPositionPropertyChanged)));


        public double SliderPosition
        {
            get => (double)GetValue(SliderPositionProperty);
            set
            {
                SetValue(SliderPositionProperty, value);
            }
        }

        public static void SliderPositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            var posSlid = d as PositionSlider;

            posSlid.SetSliderPosition((double)e.NewValue); // ka ob das guat is
        }






        #endregion



        void SetSliderPosition(double pos)
        {
            TranslateTransform transform = new TranslateTransform(pos, 0);

            this.Slider.RenderTransform = transform;
        }




        double ScreenPositonToTargetPosition(double pos)
        {
            var rulW = this.Slider.ActualWidth;
            var maxmove = this.BasicRect.ActualWidth - rulW;

            var targetPos = ((pos - rulW /2) / maxmove) 
                * (Actuator.MaximumPosition - Actuator.MinimumPosition) 
                + Actuator.MinimumPosition ;

            // clamp
            if (targetPos < Actuator.MinimumPosition)
                return Actuator.MinimumPosition;

            if (targetPos > Actuator.MaximumPosition)
                return Actuator.MaximumPosition;

            return targetPos;
        }
        


        double TargetPositionToScreenPosition()
        {
            var rulW = this.Slider.ActualWidth;
            var maxmove = (this.BasicRect.ActualWidth - rulW)-4;

            var maxdelta = (Actuator.MaximumPosition - Actuator.MinimumPosition);

            return ((Actuator.TargetPositon-Actuator.MinimumPosition)/maxdelta - 0.5)*maxmove;
        }


    }
}
