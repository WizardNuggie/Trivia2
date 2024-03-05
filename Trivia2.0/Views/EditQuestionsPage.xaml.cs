using Trivia2._0.ViewModels;

namespace Trivia2._0.Views;

public partial class EditQuestionsPage : ContentPage
{
	public EditQuestionsPage(EditQuestionsPageViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
	}
}