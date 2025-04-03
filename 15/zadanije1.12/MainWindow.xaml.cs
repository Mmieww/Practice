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
using System.ComponentModel;

namespace zadanije1._12
{

    public class Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public ObservableCollection<Participant> Participants { get; set; } = new ObservableCollection<Participant>();

        public int ParticipantCount => Participants.Count;
    }

    public class Participant
    {
        public string Name { get; set; }
        public string Section { get; set; }
    }

    public class EventService
    {
        public ObservableCollection<Event> GetEvents()
        {
            return new ObservableCollection<Event>
            {
                new Event {Name = "Конференция по технологиям", Date = DateTime.Now.AddDays(30)},
                new Event {Name = "Семинар по программированию", Date = DateTime.Now.AddDays(15)},
                new Event {Name = "Семинар по веб-разработке", Date = DateTime.Now.AddDays(45)}
            };
        }
    }


    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Event _selectedEvent;
        public Event SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
            }
        }
        public ObservableCollection<Event> Events { get; set; }

        public ICommand CreateEventCommand { get; }
        public ICommand EditEventCommand { get; }
        public ICommand DeleteEventCommand { get; }
        public ICommand AddParticipantCommand { get; }

        public MainViewModel()
        {
            var EventService = new EventService();
            Events = EventService.GetEvents();

            CreateEventCommand = new RelayCommand(o => CreateEvent());
            AddParticipantCommand = new RelayCommand(async o => await AddParticipant(), o => SelectedEvent != null);
        }

        private void CreateEvent()
        {
            MessageBox.Show("Создание мероприятия");
        }

        private void EditEvent()
        {
            MessageBox.Show("Редактирование мероприятия");
        }

        private void DeleteEvent()
        {
            MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить мероприятие?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MessageBox.Show("Мероприятие удалено");
            }
        }

        private async Task AddParticipant()
        {
            if (SelectedEvent != null)
            {
                await Task.Delay(3000);
                var participantName = "Новый участник";
                var newParticipant = new Participant { Name = participantName, Section = "Секция 1" };

                SelectedEvent.Participants.Add(newParticipant);
                MessageBox.Show($"Участник '{participantName}' успешно зарегестрирован на мероприятие '{SelectedEvent.Name}'!");
            }
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
            DataContext = _viewModel;

            InputBindings.Add(new InputBinding(_viewModel.CreateEventCommand, new KeyGesture(Key.N, ModifierKeys.Control)));
        }

        private void EventDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EventDataGrid.SelectedItem is Event selectedEvent)
            {
                _viewModel.SelectedEvent = selectedEvent;
                ParticipantListBox.ItemsSource = selectedEvent.Participants;
            }
            else
            {
                ParticipantListBox.ItemsSource = null;
                _viewModel.SelectedEvent = null;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
 }
