using System.Windows.Input;
using Trivia2._0.Services;
using Trivia2._0.Models;

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
		NewQuestion = new Question();
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
		NewQuestion = new Question();
		NewQuestion.Subject = new Subject();
		IsRefreshing = false;
	}
	private async Task SaveChanges()
	{
		foreach (Question q in service.Questions.Where(x => x.Id == Question.Id))
		{
			if (NewQuestion.Text != null && NewQuestion.Text != "")
			{
				q.Text = NewQuestion.Text;
				q.Status = service.QuestionStatuses.Where(x => x.Id == 2).FirstOrDefault();
				q.StatusId = 2;
            }
            if (NewQuestion.RightAnswer != null && NewQuestion.RightAnswer != "")
			{
                q.RightAnswer = NewQuestion.RightAnswer;
                q.Status = service.QuestionStatuses.Where(x => x.Id == 2).FirstOrDefault();
                q.StatusId = 2;
            }
            if (NewQuestion.WrongAnswer1 != null && NewQuestion.WrongAnswer1 != "")
			{
                q.WrongAnswer1 = NewQuestion.WrongAnswer1;
                q.Status = service.QuestionStatuses.Where(x => x.Id == 2).FirstOrDefault();
                q.StatusId = 2;
            }
            if (NewQuestion.WrongAnswer2 != null && NewQuestion.WrongAnswer2 != "")
			{
                q.WrongAnswer2 = NewQuestion.WrongAnswer2;
                q.Status = service.QuestionStatuses.Where(x => x.Id == 2).FirstOrDefault();
                q.StatusId = 2;
            }
            if (NewQuestion.WrongAnswer3 != null && NewQuestion.WrongAnswer3 != "")
			{
                q.WrongAnswer3 = NewQuestion.WrongAnswer3;
                q.Status = service.QuestionStatuses.Where(x => x.Id == 2).FirstOrDefault();
                q.StatusId = 2;
            }
            if (NewQuestion.Subject != null && subjectNames.Contains(NewQuestion.Subject.SubjectName))
			{
                q.Subject = service.Subjects.Where(x => x.SubjectName ==  NewQuestion.Subject.SubjectName).First();
				q.SubjectId = q.Subject.Id;
                q.Status = service.QuestionStatuses.Where(x => x.Id == 2).FirstOrDefault();
                q.StatusId = 2;
            }
        }
		await AppShell.Current.GoToAsync("///Questions");
	}
}