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
	public ObservableCollection<User> Users { get; set; }
	public List<Rank> Ranks { get; set; }
	public List<string> RanksNames {  get; set; }
    public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
	public Rank SelectedRank { get => selectedRank; set { selectedRank = value; OnPropertyChanged(); } }
	public ICommand RefreshCommand { get; set; }
	public BestScoresPageViewModel(Service s)
	{
		service = s;
		Ranks = new List<Rank>();
		RanksNames = new List<string>();
		IsRefreshing = false;
		RefreshCommand = new Command(async () => await Refresh());
		foreach (Rank rank in service.Ranks)
		{
			Ranks.Add(rank);
			RanksNames.Add(rank.RankName);
		}
	}

    private async Task Refresh()
    {
		IsRefreshing = true;
		Users.Clear();
		foreach (User user in service.Players)
		{
			Users.Add(user);
		}
		Users = new ObservableCollection<User>(Users.OrderByDescending(u => u.Points));
		IsRefreshing = false;
    }
}