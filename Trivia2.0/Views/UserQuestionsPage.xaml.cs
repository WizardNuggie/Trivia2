using Trivia2._0.ViewModels;

namespace Trivia2._0.Views;

public partial class UserQuestionsPage : ContentPage
{
	public UserQuestionsPage(UserQuestionsPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}