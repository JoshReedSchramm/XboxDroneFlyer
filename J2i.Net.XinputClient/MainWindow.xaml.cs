using AR.Drone.Client;
using AR.Drone.Client.Command;
using AR.Drone.Data;
using AR.Drone.Data.Navigation;
using AR.Drone.Media;
using J2i.Net.XInputWrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace J2i.Net.XinputClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        const float MAX_VALUE = 32768.0f;

        private static NavigationData _navigationData;
        private static NavigationPacket _navigationPacket;
        private static PacketRecorder _packetRecorderWorker;
        private static DroneClient _droneClient;
        
        private System.Timers.Timer _timer;


        private float _yaw = 0f;
        private float _gaz = 0f;
        private float _roll = 0f;
        private float _pitch = 0f;

        private bool _isTakingOff = false;
        private bool _isLanding = false;
        private bool _isEmergency = false;
        private bool _isFlying = false;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _selectedController = XboxController.RetrieveController(0);
            _selectedController.StateChanged += _selectedController_StateChanged;

            _droneClient = new DroneClient("192.168.1.1");

            _droneClient.Start();

            _droneClient.NavigationPacketAcquired += OnNavigationPacketAcquired;
            _droneClient.NavigationDataAcquired += ProcessNavigationData;

            _droneClient.FlatTrim();

            _timer = new System.Timers.Timer(50);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();

            XboxController.StartPolling();
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            DoMovement();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            XboxController.StopPolling();
            base.OnClosing(e);
        }

        void _selectedController_StateChanged(object sender, XboxControllerStateChangedEventArgs e)
        {
            OnPropertyChanged("SelectedController");   
        }

        
        public XboxController SelectedController
        {

            get { return _selectedController; }
        }


        volatile bool _keepRunning;
        XboxController _selectedController;


        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                Action a = ()=>{PropertyChanged(this, new PropertyChangedEventArgs(name));};
                Dispatcher.BeginInvoke(a, null);
                ProcessDroneCommands();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void CheckboxBButton_Checked_1(object sender, RoutedEventArgs e)
        {

        }

        private void SelectedControllerChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedController = XboxController.RetrieveController(((ComboBox)sender).SelectedIndex);
            OnPropertyChanged("SelectedController");


        }

        private void ProcessDroneCommands()
        {
            if (SelectedController.IsAPressed) 
            {
                if (!_isFlying) {
                    _isTakingOff = true; 
                }
                else
                {
                    _isLanding = true;

                }
                return;
            }

            if (SelectedController.IsBackPressed)
            {
                _isEmergency = true;
                return;
            }
            
            HandleStickMovement();
        }

        private void HandleStickMovement()
        {
            float yawScaleFactor = 0.75F;
            float gazScaleFactor = 0.75F;
            float rollScaleFactor = 0.5F;
            float pitchScaleFactor = -0.5F;
            
            _yaw = (float)SelectedController.RightThumbStick.X / MAX_VALUE * yawScaleFactor;
            _gaz = (float)SelectedController.RightThumbStick.Y / MAX_VALUE * gazScaleFactor;
            _roll = (float)SelectedController.LeftThumbStick.X / MAX_VALUE * rollScaleFactor;
            _pitch = (float)SelectedController.LeftThumbStick.Y / MAX_VALUE * pitchScaleFactor;
        }

        private void DoMovement()
        {
            if (_isTakingOff)
            {
                Console.WriteLine("Taking Off");
                _droneClient.Takeoff();
                _isFlying = true;
                Reset();
                return;
            }

            if (_isLanding)
            {
                Console.WriteLine("Landing");
                _droneClient.Land();
                _isFlying = false;
                Reset();
                return;
            }

            if (_isEmergency)
            {
                Console.WriteLine("Emergency");
                _droneClient.Emergency();
                _isFlying = false;
                Reset();
                return;
            }

            if (Math.Abs(_yaw) < .1) 
            {
                _yaw = 0;
            }

            if (Math.Abs(_gaz) < .1) {
                _gaz = 0;
            }

            if (Math.Abs(_roll) < .1) {
                _roll = 0;
            }

            if (Math.Abs(_pitch) < .1)
            {
                _pitch = 0;
            }

            Console.WriteLine("yaw: {0}, gaz: {1}, roll: {2}, pitch: {3}", _yaw, _gaz, _roll, _pitch);
            _droneClient.Progress(FlightMode.Progressive, yaw: _yaw, roll: _roll, pitch: _pitch, gaz: _gaz);
        }

        private void Reset()
        {
            _isTakingOff = false;
            _isLanding = false;
            _isEmergency = false;
        }

        private void SendVibration_Click(object sender, RoutedEventArgs e)
        {
            double leftMotorSpeed = LeftMotorSpeed.Value;
            double rightMotorSpeed = RightMotorSpeed.Value;
            _selectedController.Vibrate(leftMotorSpeed, rightMotorSpeed, TimeSpan.FromSeconds(2));
        }

        private void ProcessNavigationData(NavigationData data)
        {
            _navigationData = data;
        }

        // This happens when the drone sends a packet of data.
        private void OnNavigationPacketAcquired(NavigationPacket packet)
        {
           if (_packetRecorderWorker != null && _packetRecorderWorker.IsAlive)
                _packetRecorderWorker.EnqueuePacket(packet);

            _navigationPacket = packet;
        }
    }
}
