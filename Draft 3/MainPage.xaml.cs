using System;
using System.Threading;
using System.Diagnostics;
using System.Timers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MUXC = Microsoft.UI.Xaml.Controls;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.ApplicationModel.Background;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Draft_3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();


        }

        
        DispatcherTimer dispatcherTimer;
        DateTimeOffset startTime;
        DateTimeOffset lastTime;
        DateTimeOffset stopTime;
        int timesTicked = 1;
        int timesToTick = 900;

        public void DispatcherTimerSetup()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //IsEnabled is False by Default
            startTime = DateTimeOffset.Now;
            lastTime = startTime;
            
        }

        public void microsoftNotifications()
        {
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationID", 9813)
                .AddText("Drink Water!")
                .AddText("Keep up the great work!")
                .Show();
        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            DateTimeOffset time = DateTimeOffset.Now;
            TimeSpan span = time - lastTime;
            lastTime = time;
            //Time since last tick should be very very close to Interval
            timesTicked++;

            if (timesTicked > timesToTick)
            {
                stopTime = time;
                dispatcherTimer.Stop();
                microsoftNotifications();
                timesTicked = 1;
                dispatcherTimer.Start();
                //IsEnabled should now be false after calling stop
          
                span = stopTime - startTime;
                
            }
        }




        private void ToggleSwitch_Toggled_1(object sender, RoutedEventArgs e)
        {
            if (toggle.IsOn) 
            {
                DispatcherTimerSetup();
                dispatcherTimer.Start();
            }
            else
            {
                dispatcherTimer.Stop();
                timesTicked = 1;
               
            }
        }

        private void timelable_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ToggleSwitch_Toggled_1();
        }

        private void ToggleSwitch_Toggled_1()
        {
            throw new NotImplementedException();
        }
    }
}
