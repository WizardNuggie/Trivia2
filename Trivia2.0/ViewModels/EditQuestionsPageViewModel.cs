using System.Windows.Input;
using Trivia2._0.Services;
using Trivia2._0.Models;
using Trivia2._0.Views;

namespace Trivia2._0.ViewModels;
[QueryProperty(nameof(Question), "Question")]
public class EditQuestionsPageViewModel : ViewModel
{
	private Service service;
	private bool isRefreshing;
	private Question question;
	private Question newQuestion;
	//private string notif;
	public List<string> subjectNames;
	public bool IsRefreshing { get => isRefreshing; set {  isRefreshing = value; OnPropertyChanged(); } }
	public Question Question { get => question; set { question = value; OnPropertyChanged(); } }
	public Question NewQuestion { get => newQuestion; set {  newQuestion = value; OnPropertyChanged(); } }
    //public string Notif { get => notif; set {  notif = value; OnPropertyChanged(); } }
	public ICommand RefreshCommand { get; set; }
	public ICommand SaveChangesCommand {  get; set; }
	public EditQuestionsPageViewModel(Service s)
	{
		IsRefreshing = false;
		service = s;
		NewQuestion = new Question() {UserId = service.LoggedPlayer.Id, User = service.LoggedPlayer};
		NewQuestion.Subject = new Subject();
		subjectNames = new List<string>();
		foreach (Subject sub in service.Subjects)
		{
			subjectNames.Add(sub.SubjectName);
		}
        RefreshCommand = new Command(async () => await Refresh());
        SaveChangesCommand = new Command(async () => await SaveChanges());
	}
	private async Task Refresh()
	{
		IsRefreshing = true;
		foreach (Question q in service.Questions.Where(x => x.Id == Question.Id))
		{
			Question = q;
		}
		NewQuestion = new Question() { UserId = service.LoggedPlayer.Id, User = service.LoggedPlayer };
		NewQuestion.Subject = new Subject();
		IsRefreshing = false;
	}
	private async Task SaveChanges()
	{
		bool needSave = false;
		if (NewQuestion.Subject.SubjectName != null)
			NewQuestion.Subject.Id = service.Subjects.Where(x => x.SubjectName == NewQuestion.Subject.SubjectName).FirstOrDefault().Id;
		foreach (Question q in service.Questions.Where(x => x.Id == Question.Id))
		{
            if (NewQuestion.Text == null || NewQuestion.Text == "")
			{
				NewQuestion.Text = Question.Text;
            }
            if (NewQuestion.RightAnswer == null || NewQuestion.RightAnswer == "")
			{
                NewQuestion.RightAnswer = Question.RightAnswer;
            }
            if (NewQuestion.WrongAnswer1 == null || NewQuestion.WrongAnswer1 == "")
			{
                NewQuestion.WrongAnswer1 = Question.WrongAnswer1;
            }
            if (NewQuestion.WrongAnswer2 == null || NewQuestion.WrongAnswer2 == "")
			{
                NewQuestion.WrongAnswer2 = Question.WrongAnswer2;
            }
            if (NewQuestion.WrongAnswer3 == null || NewQuestion.WrongAnswer3 == "")
			{
                NewQuestion.WrongAnswer3 = Question.WrongAnswer3;
            }
            if (NewQuestion.Subject == null || !subjectNames.Contains(NewQuestion.Subject.SubjectName))
			{
				NewQuestion.Subject = Question.Subject;
				NewQuestion.Subject.Id = Question.Subject.Id;
				NewQuestion.SubjectId = Question.SubjectId;
            }
			if (NewQuestion.Text != Question.Text || NewQuestion.RightAnswer != Question.RightAnswer || NewQuestion.WrongAnswer1 != Question.WrongAnswer1 || NewQuestion.WrongAnswer2 != Question.WrongAnswer2 || NewQuestion.WrongAnswer3 != Question.WrongAnswer3 || NewQuestion.SubjectId != Question.SubjectId)
			{
				needSave = true;
            }
        }
		if (needSave)
		{
			service.SaveEditedChanges(Question, NewQuestion);
		}
		await AppShell.Current.GoToAsync("///Questions");
	}
}