using System.Collections.ObjectModel;
using System.Windows.Input;
using Trivia2._0.Models;
using Trivia2._0.Services;

namespace Trivia2._0.ViewModels;

public class BestScoresPageViewModel : ViewModel
{
    private Service service;
	private bool isRefreshing;
	private Rank selectedRank;
	private User selectedUser;
	private Color filterColor;
	public ObservableCollection<User> Users { get; private set; }
	public List<Rank> Ranks { get; private set; }
    public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
	public Rank SelectedRank { get => selectedRank; set { selectedRank = value; OnPropertyChanged(); ((Command)FilterCommand).ChangeCanExecute(); ((Command)ClearFilterCommand).ChangeCanExecute(); if (SelectedRank != null) FilterColor = Colors.Black; else FilterColor = Colors.DarkGray; } }
	public User SelectedUser { get => selectedUser; set { selectedUser = value; OnPropertyChanged(); } }
	public Color FilterColor { get => filterColor; set {  filterColor = value; OnPropertyChanged(); } }
	public ICommand RefreshCommand { get; set; }
	public ICommand FilterCommand { get; set; }
	public ICommand ClearFilterCommand { get; set; }
	public BestScoresPageViewModel(Service s)
	{
		service = s;
		Ranks = new List<Rank>();
		Users = new ObservableCollection<User>();
		FilterColor = Colors.DarkGray;
		IsRefreshing = false;
		RefreshCommand = new Command(async () => await Refresh());
		FilterCommand = new Command(async () => await Filter(), () => SelectedRank != null);
		ClearFilterCommand = new Command(async () => await ClearFilter(), () => SelectedRank != null);
		foreach (Rank rank in service.Ranks)
		{
			Ranks.Add(rank);
		}
		Refresh();
	}

    private async Task ClearFilter()
	{
		SelectedRank = null;
		Refresh();
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

    private async Task Refresh()
    {
		IsRefreshing = true;
		Users.Clear();
		foreach (User user in service.Players.OrderByDescending(p => p.Points))
		{
			Users.Add(user);
		}
		IsRefreshing = false;
    }
}