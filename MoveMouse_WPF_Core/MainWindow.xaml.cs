using System;
using System.Windows;
using System.Windows.Threading;

namespace MoveMouse_WPF_Core
{
    public partial class MainWindow : Window
    {

        // emulates mouse movements and clicks. 
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);

        // get mouses position
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);
        public struct Point {
            public int x;
            public int y;
        }
        

        // timer to run on. 
        DispatcherTimer timer = new DispatcherTimer();


        private int minTime = 5; 

        public MainWindow()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(TickEvent);
            timer.Interval = new TimeSpan(0, minTime, 0); // hours, min, sec
        }

        private void StartAction(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void StopAction(object sender, EventArgs e)
        {
            timer.Stop();
        }

        private void TickEvent(object sender, EventArgs e)
        {
            Point p = new Point();
            int x = 0, y = 0;

            // get mouse position
            if (GetCursorPos(out p))
            {
                x = p.x;
                y = p.y;
            }

            // move the mouse passed on windows position.  
            //var xL = (int)App.Current.MainWindow.Left; // left boundry
            //var xR = (int)App.Current.MainWindow.Width; // right boundry
            //var yT = (int)App.Current.MainWindow.Top; // top boundry
            //var yB = (int)App.Current.MainWindow.Height; // bottom boundry

            
            // move position. 
            SetCursorPos(x + 20, y + 20);
            
            SetCursorPos(x - 20, y - 20);

            // set random time
            Random gen = new Random();
            minTime = gen.Next(4, 8); //between 4 and 7 minutes. 
            timeLabel.Content = DateTime.Now.AddMinutes(minTime);
        }
    }
}
