using Trivia2._0.ViewModels;

namespace Trivia2._0.Views;

public partial class UserAdminPage : ContentPage
{
	public UserAdminPage(UserAdminPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}