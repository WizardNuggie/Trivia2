using Trivia2._0.ViewModels;

namespace Trivia2._0.Views;

public partial class Login : ContentPage
{
	public Login(LogInViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}