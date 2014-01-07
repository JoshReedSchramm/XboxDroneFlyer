using AR.Drone.Client;
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
            if (_navigationData == null)
                return;

            if (SelectedController.IsAPressed) 
            {
                if (_navigationData.State.HasFlag(NavigationState.Landed) && _navigationData.State.HasFlag(NavigationState.Command) && !_navigationData.State.HasFlag(NavigationState.Watchdog))
                {
                    _droneClient.Takeoff();
                    _droneClient.Hover();
                }
                else
                {
                    _droneClient.Land();
                }
            }

            if (SelectedController.IsBackPressed)
            {
                if (_navigationData.State.HasFlag(NavigationState.Flying)) 
                {
                    _droneClient.Emergency();
                }
            }

            bool isDead = HandleLeftStickMovement();
            isDead == isDead && HandleRightStickMovement();

            if (isDead)
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

        private bool HandleLeftStickMovement()
        {
            bool isDeadZone = Math.Abs((SelectedController.LeftThumbStick.X / Int32.MaxValue)) < .1 &&
                Math.Abs((SelectedController.LeftThumbStick.Y / Int32.MaxValue)) < .1;

            if (isDeadZone)
            {
                Console.WriteLine("Left is Dead");
                return false;
            }
        }

        private bool HandleRightStickMovement()
        {
            bool isDeadZone = Math.Abs((SelectedController.RightThumbStick.X / Int32.MaxValue)) < .1 &&
                Math.Abs((SelectedController.RightThumbStick.Y / Int32.MaxValue)) < .1;

            if (isDeadZone)
            {
                Console.WriteLine("Right is Dead");
                return false;
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
