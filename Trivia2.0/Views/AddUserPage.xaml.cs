using Trivia2._0.ViewModels;

namespace Trivia2._0.Views;

public partial class AddUserPage : ContentPage
{
	public AddUserPage(AddUserPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}