using Trivia2._0.ViewModels;
using Trivia2._0.Models;

namespace Trivia2._0.Views;

public partial class BestScoresPage : ContentPage
{
	public BestScoresPage(BestScoresPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}