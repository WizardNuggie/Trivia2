using Trivia2._0.Models;
using Trivia2._0.Services;
using System.Windows.Input;

namespace Trivia2._0.ViewModels;

public class LogInViewModel : ViewModel
{
	private Service service;
	private string name;
	private string pass;
	private string log;
	private Color logColor;
	public string Name {  get { return name; } set { name = value; OnPropertyChanged(); ((Command)LogInCommand).ChangeCanExecute(); ((Command)CancelCommand).ChangeCanExecute(); } }
	public string Pass {  get { return pass; } set { pass = value; OnPropertyChanged(); ((Command)LogInCommand).ChangeCanExecute(); ((Command)CancelCommand).ChangeCanExecute(); } }
	public string Log { get { return log; } set { log = value; OnPropertyChanged(); } }
	public Color LogColor { get { return logColor; } set { logColor = value; OnPropertyChanged(); } }
	public ICommand LogInCommand {  get; private set; }
	public ICommand CancelCommand {  get; private set; }
	private User user;
	public LogInViewModel(Service s)
	{
		service = s;
		user = new User();
		LogInCommand = new Command(async () => await LogIn(), () => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Pass));
		CancelCommand = new Command(Cancel, () => !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Pass));
	}
	private async Task LogIn()
	{
		user.Username = name;
		user.Pswrd = pass;
		if (service.LogUser(user.Username, user.Pswrd))
		{
			await AppShell.Current.GoToAsync("///Home");
		}
		else
		{
			Log = "Log in failed";
			LogColor = Colors.Red;
		}
	}

    private void Cancel()
	{
		Log = string.Empty;
		Name = string.Empty;
		Pass = string.Empty;
		user = new User();
	}
}