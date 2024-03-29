using System.Collections.ObjectModel;
using System.Windows.Input;
using Trivia2._0.Models;
using Trivia2._0.Services;

namespace Trivia2._0.ViewModels;

public class UserQuestionsPageViewModel : ViewModel
{
	private Service service;
	private List<Question> userQuestions;
	private bool isRefreshing;
	private Question selectedQuestion;
	private Status selectedStatus;
	private Color editColor;
	public ObservableCollection<Question> Questions { get; private set; }
	public List<Status> Statuses { get; private set; }
	public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
	public Question SelectedQuestion { get => selectedQuestion; set { selectedQuestion = value; OnPropertyChanged(); ((Command)NavToEditCommand).ChangeCanExecute(); if (selectedQuestion != null) EditColor = Colors.Black; else EditColor = Colors.DarkGray; } }
	public Status SelectedStatus { get => selectedStatus; set {  selectedStatus = value; OnPropertyChanged(); Filter(); ((Command)ClearFilterCommand).ChangeCanExecute(); } }
	public Color EditColor { get => editColor; set { editColor = value; OnPropertyChanged(); } }
	public ICommand RefreshCommand { get; set; }
	public ICommand ClearFilterCommand { get; set; }
    public ICommand NavToEditCommand { get; set; }

    public UserQuestionsPageViewModel(Service s)
	{
		EditColor = Colors.DarkGray;
		service = s;
        Statuses = new List<Status>();
		userQuestions = service.Questions.Where(q => q.UserId == service.LoggedPlayer.Id).ToList();
		Questions = new ObservableCollection<Question>();
		IsRefreshing = false;
        RefreshCommand = new Command(async () => await Refresh());
        ClearFilterCommand = new Command(async () => await Refresh(), () => SelectedStatus != null);
		NavToEditCommand = new Command(async () => await NavToEdit(), () => SelectedQuestion != null);
		foreach (Status status in service.QuestionStatuses)
		{
			Statuses.Add(status);
		}
		Refresh();
    }
	private async Task NavToEdit()
	{
		Dictionary<string, object> data = new Dictionary<string, object>();
		data.Add("Question", SelectedQuestion);
		await AppShell.Current.GoToAsync("Edit", data);
		SelectedQuestion = null;
	}
	private async Task Filter()
	{
        Questions.Clear();
		SelectedQuestion = null;
		userQuestions = service.Questions;
        foreach (Question question in userQuestions)
        {
            if (question.StatusId == SelectedStatus.Id)
                Questions.Add(question);
        }
    }
    private async Task Refresh()
    {
		IsRefreshing = true;
		SelectedStatus = null;
		Questions.Clear();
        userQuestions = service.Questions;
        foreach (Question question in userQuestions)
		{
			Questions.Add(question);
		}
		IsRefreshing = false;
    }
}