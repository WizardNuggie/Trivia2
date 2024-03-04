using Trivia2._0.ViewModels;

namespace Trivia2._0.Views;

public partial class BestScoresPage : ContentPage
{
	public BestScoresPage(BestScoresPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}