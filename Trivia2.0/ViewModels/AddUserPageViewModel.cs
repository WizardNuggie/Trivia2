using Trivia2._0.Services;
using Trivia2._0.Models;
using System.Windows.Input;

namespace Trivia2._0.ViewModels;

public class AddUserPageViewModel : ViewModel
{
	private Service service;
	private int id;
	private string userName;
	private string password;
	private string email;
	private Color saveColor;
	public string UserName { get =>  userName; set { userName = value; OnPropertyChanged(); ((Command)SaveChangesCommand).ChangeCanExecute(); if ((email != null && email != "") && (password != null && password != "") && (userName != null && userName != "")) SaveColor = Colors.Black; else SaveColor = Colors.DarkGray; } }
	public string Password { get =>  userName; set { userName = value; OnPropertyChanged(); ((Command)SaveChangesCommand).ChangeCanExecute(); if ((email != null && email != "") && (password != null && password != "") && (userName != null && userName != "")) SaveColor = Colors.Black; else SaveColor = Colors.DarkGray; } }
	public string Email { get =>  userName; set { userName = value; OnPropertyChanged(); ((Command)SaveChangesCommand).ChangeCanExecute(); if ((email != null && email != "") && (password != null && password != "") && (userName != null && userName != "")) SaveColor = Colors.Black; else SaveColor = Colors.DarkGray; } }
	public Color SaveColor { get => saveColor; set {  saveColor = value; OnPropertyChanged(); } }
	public ICommand SaveChangesCommand { get; set; }
	public AddUserPageViewModel(Service s)
	{
		service = s;
		id = 1;
		SaveChangesCommand = new Command(() => SaveChanges(), () => (Email != null && Email != "") && (UserName != null && UserName != "") && (Password != null && Password != ""));
		SaveColor = Colors.DarkGray;
	}
	private void SaveChanges()
	{
		foreach (User u in service.Players)
		{
			id++;
		}
		User userToAdd = new User();
		userToAdd.Username = this.UserName;
        userToAdd.Email = this.Email;
        userToAdd.Pswrd = this.Password;
        userToAdd.Id = this.id;
		userToAdd.Rankid = 3;
		userToAdd.Questionsadded = 0;
		userToAdd.Points = 0;
		userToAdd.Rank = service.Ranks.Where(x => x.Rankid == userToAdd.Rankid).FirstOrDefault();
        service.Players.Add(userToAdd);
		AppShell.Current.GoToAsync("///Admin");
	}
}