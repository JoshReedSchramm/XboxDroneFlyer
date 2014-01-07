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

            XboxController.StartPolling();
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
            //if (_navigationData == null)
            //    return;

            if (SelectedController.IsAPressed) 
            {
                if (_navigationData.State.HasFlag(NavigationState.Landed) && _navigationData.State.HasFlag(NavigationState.Command) && !_navigationData.State.HasFlag(NavigationState.Watchdog))
                {
                    _droneClient.Takeoff();
                    _droneClient.Hover();
                    Console.WriteLine("Taking Off");
                }
                else
                {
                    _droneClient.Land();
                    Console.WriteLine("Landing");
                }
            }

            if (SelectedController.IsBackPressed)
            {
                if (_navigationData.State.HasFlag(NavigationState.Flying)) 
                {
                    _droneClient.Emergency();
                    Console.WriteLine("Emergency!");
                }
            }

            bool leftStickMoved = HandleLeftStickMovement();
            bool rightStickMoved = HandleRightStickMovement();

            if (!leftStickMoved && !rightStickMoved)
            {
                Console.WriteLine("Drone is Hovering");
                _droneClient.Hover();
            }

            if (SelectedController.IsBackPressed)
            {
                if (_navigationData.State.HasFlag(NavigationState.Flying))
                {
                    _droneClient.Emergency();
                }
            }
        }

        private bool HandleRightStickMovement()
        {
            float yaw = (float)SelectedController.RightThumbStick.X / MAX_VALUE;
            float gaz = (float)SelectedController.RightThumbStick.Y / MAX_VALUE;
             
            if ((Math.Abs(yaw) < .1) && (Math.Abs(gaz) < .1))
            {
                Console.WriteLine("Left is Dead");
                return false;
            }
            else
            {
                Console.WriteLine("Yaw: {0} = {1}, Gaz: {1} = {2}", SelectedController.RightThumbStick.Y, yaw, SelectedController.RightThumbStick.Y, gaz);
                _droneClient.Progress(FlightMode.AbsoluteControl, yaw: yaw);
                _droneClient.Progress(FlightMode.AbsoluteControl, gaz: gaz);
                
                return true;
            }
            return true;
        }

        private bool HandleLeftStickMovement()
        {
            float roll = (float)SelectedController.LeftThumbStick.X / MAX_VALUE;
            float pitch = (float)SelectedController.LeftThumbStick.Y / MAX_VALUE;
                
            if ((Math.Abs(roll) < .1) && (Math.Abs(pitch) < .1))
            {
                Console.WriteLine("Right is Dead");
                return false;
            }
            else
            {
                Console.WriteLine("Roll: {0} = {1}, Pitch: {2} = {3}", SelectedController.LeftThumbStick.X, roll, SelectedController.LeftThumbStick.Y, pitch);
                _droneClient.Progress(FlightMode.AbsoluteControl, roll: roll);
                _droneClient.Progress(FlightMode.AbsoluteControl, pitch: pitch);
                
                return true;
            }
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
