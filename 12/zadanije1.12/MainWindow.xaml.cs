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
using System.Collections.ObjectModel;

namespace zadanije1._12
{

    public class Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }

    public class Participant
    {
        public string Name { get; set; }
        public string Section { get; set; }
    }

    public class MainViewModel
    {
        public ObservableCollection<Event> Events { get; set; }
        public ObservableCollection<Participant> Participants { get; set; }

        public MainViewModel()
        {
            Events = new ObservableCollection<Event>();
            {
                new Event { Name = "Конференция по технологиям", Date = DateTime.Now.AddDays(30) };
                new Event { Name = "Семинар по программированию", Date = DateTime.Now.AddDays(15) };
                new Event { Name = "Семинар по веб-разработке", Date = DateTime.Now.AddDays(45) };
            };

            Participants = new ObservableCollection<Participant>();

        }

        public partial class MainWindow
        {
            private void InitializeComponent()
            {
                System.Windows.Window window = new System.Windows.Window();

                var grid = new System.Windows.Controls.Grid();
                window.Content = grid;

                var eventDataGrid = new System.Windows.Controls.DataGrid();

                grid.Children.Add(eventDataGrid);

                var stackPanel = new System.Windows.Controls.StackPanel();

                var participantListBox = new System.Windows.Controls.ListBox();

                stackPanel.Children.Add(participantListBox);

                grid.Children.Add(stackPanel);
            }
        }
    }
    public partial class MainWindow : Window
        {
            private MainViewModel _viewModel;

            public MainWindow()
            {
                InitializeComponent();
                _viewModel = new MainViewModel();
                EventDataGrid.ItemsSource = _viewModel.Events;
            }

            private void AddParticipant_Click(object sender, RoutedEventArgs e)
            {
                var perticipantName = "Новый участник";
                var newParticipant = new Participant { Name = perticipantName, Section = "Секция 1" };
                _viewModel.Participants.Add(newParticipant);
                ParticipantListBox.ItemsSource = _viewModel.Participants;
            }
        }
    }
