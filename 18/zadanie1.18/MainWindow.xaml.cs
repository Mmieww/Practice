using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using zadanie1._18;
using static zadanie1._18.IEventRepository;

namespace zadanie1._18
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
    }

    public class Participant
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=events.db");
        }
    }

    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();
        Task AddAsync(Event event);
        Task DeleteAsync(Event event);
        }

        public class EventRepository : IEventRepository
        {
            private readonly ApplicationDbContext _context;

            public EventRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<List<Event>> GetAllAsync()
            {
                return await _context.Events.ToListAsync();
            }

            public async Task AddAsync(Event event)
        {
                _context.Events.Add(event);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Event event)
        {
            _context.Events.Remove(event);
            await _context.SaveChangesAsync();
        }
}

public interface IParticipantRepository
{
    Task<List<Participant>> GetAllAsync();
    Task AddAsync(Participant participant);
    Task DeleteAsync(Participant participant);
}

public class ParticipantRepository : IParticipantRepository
{
    private readonly ApplicationDbContext _context;

    public ParticipantRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Participant>> GetAllAsync()
    {
        return await _context.Participants.Include(p => p.Event).ToListAsync();
    }

    public async Task AddAsync(Participant participant)
    {
        _context.Participants.Add(participant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Participant participant)
    {
        _context.Participants.Remove(participant);
        await _context.SaveChangesAsync();
    }
}

public class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

public class ProjectViewModel<T> : BaseViewModel
{
    private readonly IRepository<T> _repo;
    public ObservableCollection<T> Items { get; } = new();
    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand DeleteCommand { get; }

    public ProjectViewModel(IRepository<T> repo)
    {
        _repo = repo;

        LoadCommand = new AsyncRelayCommand(async () =>
        {
            var list = await _repo.GetAllAsync();
            Items.Clear();
            foreach (var i in list)
                Items.Add(i);
        });

        AddCommand = new AsyncRelayCommand(async () =>
        {
            var newItem = Activator.CreateInstance<T>();
            if (newItem is Event newEvent)
            {
                newEvent.Name = "New Event";
                newEvent.Location = "New Location";
                newEvent.Date = DateTime.Now;
                await _repo.AddAsync(newEvent);
            }
            else if (newItem is Participant newParticipant)
            {
                newParticipant.FullName = "New Participant";
                await _repo.AddAsync(newParticipant);
            }
            await ((AsyncRelayCommand)LoadCommand).ExecuteAsync(null);
        });

        DeleteCommand = new AsyncRelayCommand(async () =>
        {
        if (SelectedItem != null)
        {
            await _repo.DeleteAsync(SelectedItem);
            await ((AsyncRelayCommand)LoadCommand).ExecuteAsync(null);
            }
        });
    }

    public T SelectedItem { get; set; }
}

public class EventViewModel : ProjectViewModel<Event>
{
    public EventViewModel(IEventRepository repo) : base(repo) { }
}

public class ParticipantViewModel : ProjectViewModel<Participant>
{
    public ParticipantViewModel(IParticipantRepository repo) : base(repo) { }
}

public partial class MainWindow : Window
{
    private readonly EventViewModel _eventViewModel;
    private readonly ParticipantViewModel _participantViewModel;

    public MainWindow()
    {
        InitializeComponent();
        var context = new ApplicationDbContext();
        _eventViewModel = new EventViewModel(new EventRepository(context));
        _participantViewModel = new ParticipantViewModel(new ParticipantRepository(context));
        this.DataContext = new { Events = _eventViewModel, Participants = _participantViewModel };
    }
}

public partial class App : Application
{
    public ApplicationDbContext DbContext { get; private set; }
    public IEventRepository EventRepository { get; private set; }
    public IParticipantRepository ParticipantRepository { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        DbContext = new ApplicationDbContext();

        EventRepository = new EventRepository(DbContext);
        ParticipantRepository = new ParticipantRepository(DbContext);

        var mainWindow = new MainWindow
        {
            DataContext = new
            {
                Events = new EventViewModel(EventRepository),
                Participants = new ParticipantViewModel(ParticipantRepository)
            }
        };

        mainWindow.Show();
    }

    public class AsyncRelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public async void Execute(object parameter) => await _execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task AddAsync(T item);
        Task DeleteAsync(T item);
    }
}