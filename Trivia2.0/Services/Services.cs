using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Trivia2._0.Models;

namespace Trivia2._0.Services
{
    public class Service
    {
        public User LoggedPlayer { get; private set; }
        public List<User> Players { get { return playerService.Players; } private set { this.playerService.Players = value; } }
        private PlayerService playerService;
        public List<Question> Questions { get { return questionService.Questions; } private set { this.questionService.Questions = value; } }
        private QuestionService questionService;
        public List<Status> QuestionStatuses { get { return questionStatusService.QuestionStatuses; } private set { this.questionStatusService.QuestionStatuses = value; } }
        private QuestionStatusService questionStatusService;
        public List<Rank> Ranks { get { return rankService.Ranks; } private set { this.rankService.Ranks = value; } }
        private RankService rankService;
        public List<Subject> Subjects { get { return subjectService.Subjects; } private set { this.subjectService.Subjects = value; } }
        private SubjectService subjectService;
        public Service()
        {
            playerService = new PlayerService();
            questionService = new QuestionService();
            questionStatusService = new QuestionStatusService();
            rankService = new RankService();
            subjectService = new SubjectService();
            AddPlayerToQuestions();
            AddRanksToPlayers();
            AddStatusesToQuestions();
            AddSubjectsToQuestions();

        }
        public async void SaveEditedChanges(Question q, Question newQ)
        {
                int index = q.Id-1;
                Questions.RemoveAt(index);
                q = new Question()
                {
                    Id = index+1,
                    User = newQ.User,
                    UserId = q.User.Id,
                    Text = newQ.Text,
                    RightAnswer = newQ.RightAnswer,
                    WrongAnswer1 = newQ.WrongAnswer1,
                    WrongAnswer2 = newQ.WrongAnswer2,
                    WrongAnswer3 = newQ.WrongAnswer3,
                    Subject = Subjects.Where(x => x.SubjectName == newQ.Subject.SubjectName).FirstOrDefault(),
                    SubjectId = newQ.Subject.Id,
                    Status = QuestionStatuses.Where(x => x.Id == 2).FirstOrDefault(),
                    StatusId = 2,
                };
                Questions.Insert(index, q);
        }
        public void ChangeStatus(Question q, bool isApproved)
        {
            int index = q.Id - 1;
            User user = q.User;
            string text = q.Text;
            string cor = q.RightAnswer;
            string wrong1 = q.WrongAnswer1;
            string wrong2 = q.WrongAnswer2;
            string wrong3 = q.WrongAnswer3;
            Subject sub = q.Subject;
            Questions.RemoveAt(index);
            q = new Question()
            {
                Id = index + 1,
                User = user,
                UserId = q.User.Id,
                Text = text,
                RightAnswer = cor,
                WrongAnswer1 = wrong1,
                WrongAnswer2 = wrong2,
                WrongAnswer3 = wrong3,
                Subject = sub,
                SubjectId = sub.Id,
            };
            if (isApproved)
            {
                q.Status = QuestionStatuses.Where(x => x.Id == 1).FirstOrDefault();
                q.StatusId = q.Status.Id;
            }
            else
            {
                q.Status = QuestionStatuses.Where(x => x.Id == 3).FirstOrDefault();
                q.StatusId = q.Status.Id;
            }
            Questions.Insert(index, q);
        }
        public void ResetPoints(User u)
        {
            int index = u.Id - 1;
            Rank rank = u.Rank;
            int rankId = u.Rankid;
            string email = u.Email;
            string pass = u.Pswrd;
            string username = u.Username;
            int qAdded = u.Questionsadded;
            Players.RemoveAt(index);
            u = new User()
            {
                Id = index + 1,
                Rank = rank,
                Rankid = rankId,
                Email = email,
                Username = username,
                Pswrd = pass,
                Questionsadded = qAdded,
                Points = 0,
            };
            Players.Insert(index, u);
        }
        private async void AddPlayerToQuestions()
        {
            foreach (Question q in Questions)
            {
                q.User = Players.Where(x => x.Id == q.UserId).FirstOrDefault();
            }
        }
        private async void AddStatusesToQuestions()
        {
            foreach (Question q in Questions)
            {
                q.Status = QuestionStatuses.Where(x => x.Id == q.StatusId).FirstOrDefault();
            }
        }
        private async void AddRanksToPlayers()
        {
            foreach (User p in Players)
            {
                p.Rank = Ranks.Where(x => x.Rankid == p.Rankid).FirstOrDefault();
            }
        }
        private async void AddSubjectsToQuestions()
        {
            foreach (Question q in Questions)
            {
                q.Subject = Subjects.Where(x => x.Id == q.SubjectId).FirstOrDefault();
            }
        }
        public bool LogUser(string Username, string password)
        {
            try
            {
                LoggedPlayer = Players.Where(x => x.Username == Username && x.Pswrd == password).FirstOrDefault();
                return LoggedPlayer != null;
            }
            catch
            {
                return false;
            }
        }
        internal class PlayerService
        {
            public List<User> Players { get; set; }
            public PlayerService()
            {
                FillList();
            }
            private void FillList()
            {
                Players = new List<User>
                {
                new User()
                {
                    Id = 1,
                    Email = "talkazyo@gmail.com",
                    Pswrd = "12345678",
                    Username = "Ro",
                    Rankid = 1,
                    Points = 0,
                    Questionsadded = 5,
                },
                new User()
                {
                    Id = 2,
                    Email = "nobitches@gmail.com",
                    Pswrd = "1234",
                    Username = "MegaMind",
                    Rankid = 3,
                    Points = 0,
                    Questionsadded = 0,
                },
                new User()
                {
                    Id = 3,
                    Email = "hola@gmail.com",
                    Pswrd = "12345",
                    Username = "Hola",
                    Rankid = 3,
                    Points = 10,
                    Questionsadded = 0,
                }
                };
            }
        }
        internal class QuestionService
        {
            public List<Question> Questions { get; set; }
            public QuestionService()
            {
                FillList();
            }
            private void FillList()
            {
                Questions = new List<Question>();
                Questions.Add(new Question()
                {
                    Id = 1,
                    UserId = 1,
                    RightAnswer = "Usain Bolt",
                    WrongAnswer1 = "Tyson Gay",
                    WrongAnswer2 = "Yohan Blake",
                    WrongAnswer3 = "Asafa Powell",
                    Text = "Who holds the current world record for the fastest 100 meters run?",
                    SubjectId = 1,
                    StatusId = 2
                });
                Questions.Add(new Question()
                {
                    Id = 2,
                    UserId = 1,
                    RightAnswer = "Joe Biden",
                    WrongAnswer1 = "Donald J Trump",
                    WrongAnswer2 = "Barak Obama",
                    WrongAnswer3 = "George Washington",
                    Text = "Who is the current president of the USA?",
                    SubjectId = 2,
                    StatusId = 2
                });
                Questions.Add(new Question()
                {
                    Id = 3,
                    UserId = 1,
                    RightAnswer = "6 million",
                    WrongAnswer1 = "5 million",
                    WrongAnswer2 = "7 million",
                    WrongAnswer3 = "8 million",
                    Text = "How many jews did Hitler kill during the holocaust?",
                    SubjectId = 3,
                    StatusId = 2
                });
                Questions.Add(new Question()
                {
                    Id = 4,
                    UserId = 1,
                    RightAnswer = "118",
                    WrongAnswer1 = "120",
                    WrongAnswer2 = "125",
                    WrongAnswer3 = "112",
                    Text = "How many elements are in the periodic table?",
                    SubjectId = 4,
                    StatusId = 2
                });
                Questions.Add(new Question()
                {
                    Id = 5,
                    UserId = 1,
                    RightAnswer = "6",
                    WrongAnswer1 = "5",
                    WrongAnswer2 = "7",
                    WrongAnswer3 = "8",
                    Text = "How many classes are there in 11th grade in Ramon High School?",
                    SubjectId = 5,
                    StatusId = 2
                });
            }
        }
        internal class RankService
        {
            public List<Rank> Ranks { get; set; }
            public RankService()
            {
                FillList();
            }
            private void FillList()
            {
                Ranks = new List<Rank>
            {
                new Rank()
                {
                    Rankid = 1,
                    RankName = "Admin"
                },
                new Rank()
                {
                    Rankid = 2,
                    RankName = "Master"
                },
                new Rank()
                {
                    Rankid = 3,
                    RankName = "Rookie"
                }
            };
            }
        }
        internal class QuestionStatusService
        {
            public List<Status> QuestionStatuses { get; set; }
            public QuestionStatusService()
            {
                FillList();
            }
            private void FillList()
            {
                QuestionStatuses = new List<Status>();
                QuestionStatuses.Add(new Status()
                {
                    Id = 1,
                    CurrentStatus = "Approved"
                });
                QuestionStatuses.Add(new Status()
                {
                    Id = 2,
                    CurrentStatus = "Pending"
                });
                QuestionStatuses.Add(new Status()
                {
                    Id = 3,
                    CurrentStatus = "Denied"
                });
            }
        }
        internal class SubjectService
        {
            public List<Subject> Subjects { get; set; }
            public SubjectService()
            {
                FillList();
            }
            private void FillList()
            {
                Subjects = new List<Subject>();
                Subjects.Add(new Subject()
                {
                    Id = 1,
                    SubjectName = "Sports"
                });
                Subjects.Add(new Subject()
                {
                    Id = 2,
                    SubjectName = "Politics"
                });
                Subjects.Add(new Subject()
                {
                    Id = 3,
                    SubjectName = "History"
                });
                Subjects.Add(new Subject()
                {
                    Id = 4,
                    SubjectName = "Science"
                });
                Subjects.Add(new Subject()
                {
                    Id = 5,
                    SubjectName = "Ramon"
                });
            }
        }
    }
}
