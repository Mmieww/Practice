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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media.Animation;

namespace zadanie1._17
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents()
        {
            var events = new List<Event>
            {
                new Event { EventName = "Event 1" },
                new Event { EventName = "Event 2" },
                new Event { EventName = "Event 3" }
            };
            ScheduleItemsControl.ItemsSource = events;
        }

        private void StartAnimation_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.Resources["BackgroundAnimation"] as Storyboard;
            if (sb != null)
            {
                sb.Begin();
            }
        }

        private void EventBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Event eventDetails)
            {
                MessageBox.Show($"Details about {eventDetails.EventName}", "Event Details");
            }
        }
    }

    public class Event
    {
        public string EventName { get; set; }
    }
}
