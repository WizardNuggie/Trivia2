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
    private Color filterColor;
    private List<Question> qlist;
    public List<Subject> Subjects { get; private set; }
    public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }
    public Subject SelectedSubject { get => selectedSubject; set { selectedSubject = value; OnPropertyChanged(); ((Command)FilterCommand).ChangeCanExecute(); ((Command)ClearFilterCommand).ChangeCanExecute(); if (selectedSubject != null) FilterColor = Colors.Black; else FilterColor = Colors.DarkGray; } }
    public Color FilterColor { get => filterColor; set { filterColor = value; OnPropertyChanged(); } }
    public ObservableCollection<Question> PenQs { get; set; }
    public ICommand RefreshCommand { get; set; }
    public ICommand FilterCommand { get; set; }
    public ICommand ClearFilterCommand { get; set; }
    public ICommand ApproveCommand { get; set; }
    public ICommand DeclineCommand { get; set; }

    public ApproveQuestionsPageViewModel(Service s)
	{
		service = s;
        qlist = service.Questions.Where(x => x.StatusId == 2).ToList();
        FilterColor = Colors.DarkGray;
        Subjects = new List<Subject>();
        PenQs = new ObservableCollection<Question>();
        IsRefreshing = false;
        RefreshCommand = new Command(async () => await Refresh());
        FilterCommand = new Command(async () => await Filter(), () => SelectedSubject != null);
        ClearFilterCommand = new Command(async () => await ClearFilter(), () => SelectedSubject != null);
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
    private async Task ClearFilter()
    {
        Refresh();
    }

    private async Task Filter()
    {
        PenQs.Clear();
        qlist = service.Questions.Where(x => x.StatusId == 2).ToList();
        foreach (Question question in qlist)
        {
            if (question.SubjectId == SelectedSubject.Id)
                PenQs.Add(question);
        }
    }
    private async Task Refresh()
    {
        IsRefreshing = true;
        qlist = service.Questions.Where(x => x.StatusId == 2).ToList();
        PenQs.Clear();
        foreach (Question question in qlist)
        {
            PenQs.Add(question);
        }
        SelectedSubject = null;
        IsRefreshing = false;
    }
}