using System.Windows;
using System;
using System.Threading;

namespace Third
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void StopTheBus()
        {
           while(true)
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    Bus.BusStop();
                }));
                Thread.Sleep(50);
            }
            
        }
        private Thread t;
        private Bus b = new Bus();
        public Bus Bus => b;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            t = new Thread(StopTheBus);
            t.Start();
        }
    }
}
