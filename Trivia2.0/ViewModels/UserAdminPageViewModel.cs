using System.Collections.ObjectModel;
using System.Windows.Input;
using Trivia2._0.Models;
using Trivia2._0.Services;

namespace Trivia2._0.ViewModels;

public class UserAdminPageViewModel : ViewModel
{
	private Service service;
	private bool isRefreshing;
    private Rank selectedRank;
    public List<Rank> Ranks { get; private set; }
    public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
    public Rank SelectedRank { get => selectedRank; set { selectedRank = value; OnPropertyChanged(); Filter(); ((Command)ClearFilterCommand).ChangeCanExecute(); } }
    public ObservableCollection<User> Users { get; private set; }
    public ICommand RefreshCommand { get; set; }
    public ICommand ClearFilterCommand { get; set; }
    public ICommand ResetPointsCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand AddUserCommand { get; set; }
    public ICommand ShowDetailsCommand { get; set; }
    public UserAdminPageViewModel(Service s)
	{
        service = s;
        IsRefreshing = false;
        Ranks = new();
        Users = new ObservableCollection<User>();
        RefreshCommand = new Command(async () => await Refresh());
        ClearFilterCommand = new Command(async () => await Refresh(), () => selectedRank != null);
        ResetPointsCommand = new Command((Object obj) => ResetPoints(obj));
        DeleteCommand = new Command((Object obj) => Delete(obj));
        AddUserCommand = new Command(async () => await AddUser());
        ShowDetailsCommand = new Command((Object obj) => ShowDetails((User)obj));
        foreach (Rank rank in service.Ranks)
        {
            Ranks.Add(rank);
        }
        foreach(User user in service.Players)
        {
            Users.Add(user);
        }
    }
    private void ShowDetails(User u)
    {
        string details = $"Id: \"{u.Id}\"\nUsername: \"{u.Username}\"\nPassword: \"{u.Pswrd}\"\nPoints: \"{u.Points}\"\nQuestions Aded: \"{u.Questionsadded}\"\nRank: \"{u.Rank.RankName}\"\nEmail: \"{u.Email}\"";
        AppShell.Current.DisplayAlert($"{u.Username}'s Details", details, "Ok");
    }
    private async Task AddUser()
    {
        await AppShell.Current.GoToAsync("AddUser");
    }
    private void ResetPoints(Object obj)
    {
        service.ResetPoints((User)obj);
        Users.Where(x => x.Id == ((User)obj).Id).FirstOrDefault().Points = 0;
        Refresh();
    }
    private void Delete(Object obj)
    {
        User usertodel = new User();
        foreach (User user in service.Players.Where(x => x.Id == ((User)obj).Id))
        {
            Users.Remove(user);
            usertodel = user;
        }
        service.Players.Remove(usertodel);
        Refresh();
    }
    private async Task Refresh()
    {
        IsRefreshing = true;
        Users.Clear();
        SelectedRank = null;
        foreach (User user in service.Players)
        {
            Users.Add(user);
        }
        IsRefreshing = false;
    }
    private async Task Filter()
    {
        Users.Clear();
        foreach (User user in service.Players)
        {
            if (user.Rankid == SelectedRank.Rankid || SelectedRank == null)
                Users.Add(user);
        }
    }
}