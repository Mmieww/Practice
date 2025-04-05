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
using Newtonsoft.Json;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.IO;    
using System.Windows.Threading;

namespace zadanije1._12
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsOrganizer { get; set; }
    }

    public class UserService
    {
        private const string UserFilePath = "users.json";

        public ObservableCollection<User> GetUsers()
        {
            if (!File.Exists(UserFilePath))
                return new ObservableCollection<User>();

            var json = File.ReadAllText(UserFilePath);
            return JsonConvert.DeserializeObject<ObservableCollection<User>>(json);
        }

        public void SaveUsers(ObservableCollection<User> users)
        {
            var json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(UserFilePath, json);
        }

        public User Authenticate(string username, string password)
        {
            var users = GetUsers();
            return users.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }

        public void Register(string username, string password, bool IsOrganizer)
        {
            var users = GetUsers();
            users.Add(new User { UserName = username, Password = password, IsOrganizer = IsOrganizer });
            SaveUsers(users);
        }
    }

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
        private const string EventFilePath = "events.jons";
        public ObservableCollection<Event> GetEvents()
        {
            if (!File.Exists(EventFilePath))
                return new ObservableCollection<Event>();

            var json = File.ReadAllText(EventFilePath);
            return JsonConvert.DeserializeObject<ObservableCollection<Event>>(json);
        }

        public void SaveEvents(ObservableCollection<Event> events)
        {
            var json = JsonConvert.SerializeObject(events, Formatting.Indented);
            File.WriteAllText(EventFilePath, json);
            {
                new Event { Name = "Конференция по технологиям", Date = DateTime.Now.AddDays(30) };
                new Event { Name = "Семинар по программированию", Date = DateTime.Now.AddDays(15) };
                new Event { Name = "Семинар по веб-разработке", Date = DateTime.Now.AddDays(45) };
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

        private UserService _userService;
        private EventService _eventService;
        private NotificationService _notificationService;
        public ObservableCollection<User> Users { get; set; }

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

        private User _loggedInUser;

        public ICommand CreateEventCommand { get; }
        public ICommand EditEventCommand { get; }
        public ICommand DeleteEventCommand { get; }
        public ICommand AddParticipantCommand { get; }
        public RelayCommand RegisterCommand { get; }
        public RelayCommand LoginCommand { get; }

        private User _loggedUser;

        public MainViewModel()
        {
            _userService = new UserService();
            Users = _userService.GetUsers();

            _eventService = new EventService();
            Events = _eventService.GetEvents();

            _notificationService = new NotificationService();
            _notificationService.MonitorNotifications();

            CreateEventCommand = new RelayCommand(o => CreateEvent());
            AddParticipantCommand = new RelayCommand(async o => await AddParticipant(), o => SelectedEvent != null);
        }

        //public ICommand RegisterCommand => new RelayCommand(ExecuteRegister);
        //public ICommand LoginCommand => new RelayCommand(ExecuteLogin);

        private void ExecuteRegister()
        {
            string username = "newuser";
            string password = "password";

            _userService.Register(username, password, false);
            MessageBox.Show("Пользователь зарегестрирован");
        }

        private void ExecuteLogin()
        {
            string username = "newuser";
            string password = "password";
            _loggedInUser = _userService.Authenticate(username, password);
            if (_loggedInUser != null)
            {
                MessageBox.Show("Добро пожаловать, " + _loggedUser.UserName);
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.");  
            }
        }

        private void CreateEvent()
        {
            var newEvent = new Event { Name = "Новое мероприятие", Date = DateTime.Now.AddDays(7) };
            Events.Add(newEvent);
            _eventService.SaveEvents(Events);
            _notificationService.SendNotification($"Создано новое мероприятие: {newEvent.Name}");
            MessageBox.Show("Мероприятие создано.");
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
                _eventService.SaveEvents(Events);
                MessageBox.Show($"Участник '{participantName}' успешно зарегестрирован на мероприятие '{SelectedEvent.Name}'!");
            }
        }

        public class NotificationService
        {
            private const string MemoryMappedFileName = "EventNotification";

            public void SendNotification(string message)
            {
                using (var mmf = MemoryMappedFile.CreateOrOpen(MemoryMappedFileName, 1024))
                {
                    using (var stream = mmf.CreateViewStream())
                    {
                        var bytes = System.Text.Encoding.UTF8.GetBytes(message);
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
            }

            public string ReceiveNotification()
            {
                try
                {
                    using (var mmf = MemoryMappedFile.OpenExisting(MemoryMappedFileName))
                    {
                        using (var stream = mmf.CreateViewStream())
                        {
                            var buffer = new byte[1024];
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);
                            return System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    return string.Empty;
                }
            }

            public void MonitorNotifications()
            {
                Thread notificationTread = new Thread(() =>
                {
                    var notificationService = new NotificationService();
                    while (true)
                    {
                        string notification = ReceiveNotification();
                        if (!string.IsNullOrEmpty(notification))
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                MessageBox.Show(notification, "Уведомление");
                            });
                        }
                        Thread.Sleep(1000);
                    }
                });
                notificationTread.IsBackground = true;
                notificationTread.Start();
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