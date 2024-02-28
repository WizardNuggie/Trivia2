using Trivia2._0.Models;
using Trivia2._0.Services;
using System.Windows.Input;

namespace Trivia2._0.ViewModels;

public class LogInViewModel : ViewModel
{
	private string name;
	private string pass;
	private string log;
	private bool isNotLogged;
	private Color logColor;
	public string Name {  get { return name; } set { name = value; OnPropertyChanged(); ((Command)LogInCommand).ChangeCanExecute(); ((Command)CancelCommand).ChangeCanExecute(); } }
	public string Pass {  get { return pass; } set { pass = value; OnPropertyChanged(); ((Command)LogInCommand).ChangeCanExecute(); ((Command)CancelCommand).ChangeCanExecute(); } }
	public string Log { get { return log; } set { log = value; OnPropertyChanged(); } }
	public bool IsNotLogged { get { return isNotLogged; } set { isNotLogged = value; OnPropertyChanged(); } }
	public Color LogColor { get { return logColor; } set { logColor = value; OnPropertyChanged(); } }
	public ICommand LogInCommand {  get; private set; }
	public ICommand CancelCommand {  get; private set; }
	private User user;
	public LogInViewModel()
	{
		user = new User();
		IsNotLogged = true;
		LogInCommand = new Command(LogIn, () => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Pass));
		CancelCommand = new Command(Cancel, () => !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Pass));
	}
	private void LogIn()
	{
		Service service = new Service();
		user.Username = name;
		user.Pswrd = pass;
		if (service.LogUser(user.Username, user.Pswrd))
		{
			Log = "Log in successfull";
			LogColor = Colors.Green;
			IsNotLogged = false;
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