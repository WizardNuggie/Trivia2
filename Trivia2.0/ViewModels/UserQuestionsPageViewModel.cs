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
	private Subject selectedSubject;
	public ObservableCollection<Question> Questions { get; private set; }
	public List<Subject> Subjects { get; private set; }
	public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
	public Question SelectedQuestion { get => selectedQuestion; set { selectedQuestion = value; OnPropertyChanged(); } }
	public Subject SelectedSubject { get => selectedSubject; set {  selectedSubject = value; OnPropertyChanged(); ((Command)FilterCommand).ChangeCanExecute(); ((Command)ClearFilterCommand).ChangeCanExecute(); } }
	public ICommand RefreshCommand { get; set; }
	public ICommand FilterCommand { get; set; }
	public ICommand ClearFilterCommand { get; set; }
	public UserQuestionsPageViewModel(Service s)
	{
		service = s;
		Subjects = new List<Subject>();
		userQuestions = service.Questions.Where(q => q.UserId == service.LoggedPlayer.Id).ToList();
		Questions = new ObservableCollection<Question>();
		IsRefreshing = false;
        RefreshCommand = new Command(async () => await Refresh());
        FilterCommand = new Command(async () => await Filter(), () => SelectedSubject != null);
        ClearFilterCommand = new Command(async () => await ClearFilter(), () => SelectedSubject != null);
		foreach (Subject subject in service.Subjects)
		{
			Subjects.Add(subject);
		}
		Refresh();
    }

    private async Task ClearFilter()
    {
		SelectedSubject = null;
		Refresh();
    }

	private async Task Filter()
	{
        Questions.Clear();
        foreach (Question question in userQuestions)
        {
            if (question.SubjectId == SelectedSubject.Id)
                Questions.Add(question);
        }
    }
    private async Task Refresh()
    {
		IsRefreshing = true;
		Questions.Clear();
		foreach (Question question in userQuestions)
		{
			Questions.Add(question);
		}
		IsRefreshing = false;
    }
}