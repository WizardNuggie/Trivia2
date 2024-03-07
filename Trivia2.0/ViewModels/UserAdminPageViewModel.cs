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
    private Color filterColor;
    public List<Rank> Ranks { get; private set; }
    public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
    public Rank SelectedRank { get => selectedRank; set { selectedRank = value; OnPropertyChanged(); ((Command)FilterCommand).ChangeCanExecute(); ((Command)ClearFilterCommand).ChangeCanExecute(); if (selectedRank != null) FilterColor = Colors.Black; else FilterColor = Colors.DarkGray; } }
    public Color FilterColor { get => filterColor; set { filterColor = value; OnPropertyChanged(); } }
    public ObservableCollection<User> Users { get; private set; }
    public ICommand RefreshCommand { get; set; }
    public ICommand FilterCommand { get; set; }
    public ICommand ClearFilterCommand { get; set; }
    public ICommand ResetPointsCommand { get; set; }
    public ICommand DeleteCommand { get; set; }
    public ICommand AddUserCommand { get; set; }
    public UserAdminPageViewModel(Service s)
	{
        service = s;
        FilterColor = Colors.DarkGray;
        IsRefreshing = false;
        Ranks = new();
        Users = new ObservableCollection<User>();
        RefreshCommand = new Command(async () => await Refresh());
        FilterCommand = new Command(async () => await Filter(), () => selectedRank != null);
        ClearFilterCommand = new Command(async () => await ClearFilter(), () => selectedRank != null);
        ResetPointsCommand = new Command((Object obj) => ResetPoints(obj));
        DeleteCommand = new Command((Object obj) => Delete(obj));
        AddUserCommand = new Command(async () => await AddUser());
        foreach (Rank rank in service.Ranks)
        {
            Ranks.Add(rank);
        }
        foreach(User user in service.Players)
        {
            Users.Add(user);
        }
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
        foreach (User user in service.Players)
        {
            Users.Add(user);
        }
        SelectedRank = null;
        IsRefreshing = false;
    }
    private async Task Filter()
    {
        Users.Clear();
        foreach (User user in service.Players.OrderByDescending(p => p.Points))
        {
            if (user.Rankid == SelectedRank.Rankid)
                Users.Add(user);
        }
    }
    private async Task ClearFilter()
    {
        SelectedRank = null;
        Refresh();
    }
}