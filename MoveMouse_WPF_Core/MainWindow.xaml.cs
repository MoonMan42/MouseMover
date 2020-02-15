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

        private void StartBtn_Click(object sender, EventArgs e)
        {
            timer.Start();
            timeLabel.Content = DateTime.Now.AddMinutes(5).ToString("h:mm tt");
        }

        private void StopBtn_Click(object sender, EventArgs e)
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


            // move position. 
            SetCursorPos(x + GenPos(), y + GenPos());
            SetCursorPos(x + GenPos(), y + GenPos());

            // set random time
            GenTime();
        }

        // randomly generate time and display to board. 
        private void GenTime()
        {
            Random gen = new Random();
            minTime = gen.Next(4, 7); //between 4 and 7 minutes. 
            timeLabel.Content = DateTime.Now.AddMinutes(minTime).ToString("h:mm tt");
        }

        private int GenPos()
        {
            Random gen = new Random();
            return gen.Next(50, 200);
        }
    }
}
