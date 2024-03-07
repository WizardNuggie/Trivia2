using Trivia2._0.Services;
using Trivia2._0.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Trivia2._0.ViewModels;

public class ApproveQuestionsPageViewModel : ViewModel
{
	private Service service;
    private bool isRefreshing;
    private Subject selectedSubject;
    private List<Question> qlist;
    public List<Subject> Subjects { get; private set; }
    public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
    public Subject SelectedSubject { get => selectedSubject; set { selectedSubject = value; OnPropertyChanged(); Filter(); ((Command)ClearFilterCommand).ChangeCanExecute(); } }
    public ObservableCollection<Question> PenQs { get; set; }
    public ICommand RefreshCommand { get; set; }
    public ICommand ClearFilterCommand { get; set; }
    public ICommand ApproveCommand { get; set; }
    public ICommand DeclineCommand { get; set; }

    public ApproveQuestionsPageViewModel(Service s)
	{
		service = s;
        qlist = service.Questions.Where(x => x.StatusId == 2).ToList();
        Subjects = new List<Subject>();
        PenQs = new ObservableCollection<Question>();
        IsRefreshing = false;
        RefreshCommand = new Command(async () => await Refresh());
        ClearFilterCommand = new Command(async () => await Refresh(), () => SelectedSubject != null);
        ApproveCommand = new Command((Object obj) => Approve(obj));
        DeclineCommand = new Command((Object obj) => Decline(obj));
        foreach (Subject subject in service.Subjects)
        {
            Subjects.Add(subject);
        }
        foreach (Question q in service.Questions.Where(x => x.StatusId == 2))
        {
            PenQs.Add(q);
        }
    }
    private void Approve(Object obj)
    {
        service.ChangeStatus((Question)obj, true);
        PenQs.Remove((Question)obj);
    }
    private void Decline(Object obj)
    {
        service.ChangeStatus((Question)obj, false);
        PenQs.Remove((Question)obj);
    }
    private async Task Filter()
    {
        PenQs.Clear();
        qlist = service.Questions.Where(x => x.StatusId == 2).ToList();
        foreach (Question question in qlist)
        {
            if (question.SubjectId == SelectedSubject.Id || SelectedSubject == null)
                PenQs.Add(question);
        }
    }
    private async Task Refresh()
    {
        IsRefreshing = true;
        SelectedSubject = null;
        qlist = service.Questions.Where(x => x.StatusId == 2).ToList();
        PenQs.Clear();
        foreach (Question question in qlist)
        {
            PenQs.Add(question);
        }
        IsRefreshing = false;
    }
}