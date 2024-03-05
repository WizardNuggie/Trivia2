using System.Collections.ObjectModel;
using System.Windows.Input;
using Trivia2._0.Models;
using Trivia2._0.Services;
using Xamarin.Google.Crypto.Tink.Signature;

namespace Trivia2._0.ViewModels;

public class BestScoresPageViewModel : ViewModel
{
	private Service service;
	private bool isRefreshing;
	private Rank selectedRank;
	private User selectedUser;
	public ObservableCollection<User> Users { get; set; }
	public List<Rank> Ranks { get; set; }
	public List<string> RanksNames {  get; set; }
    public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
	public Rank SelectedRank { get => selectedRank; set { selectedRank = value; OnPropertyChanged(); ((Command)FilterCommand).ChangeCanExecute(); ((Command)ClearFilterCommand).ChangeCanExecute(); } }
	public User SelectedUser { get => selectedUser; set { selectedUser = value; OnPropertyChanged(); } }
	public ICommand RefreshCommand { get; set; }
	public ICommand FilterCommand { get; set; }
	public ICommand ClearFilterCommand { get; set; }
	public BestScoresPageViewModel(Service s)
	{
		service = s;
		Ranks = new List<Rank>();
		RanksNames = new List<string>();
		Users = new ObservableCollection<User>();
		IsRefreshing = false;
		RefreshCommand = new Command(async () => await Refresh());
		FilterCommand = new Command(async () => await Filter(), () => SelectedRank != null);
		ClearFilterCommand = new Command(async () => await ClearFilter(), () => SelectedRank != null);
		foreach (Rank rank in service.Ranks)
		{
			Ranks.Add(rank);
			RanksNames.Add(rank.RankName);
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