﻿using System;
using System.Collections.Generic;
using System.Linq;
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
    }
    internal class PlayerService
    {
        public List<User> Players { get; set; }
        public PlayerService()
        {
            FillList();
        }
        private async void FillList()
        {
            Players = new List<User>();
            Players.Add(new User()
            {
                Email = "talkazyo@gmail.com",
                Pswrd = "12345678",
                Username = "Ro",
                Rankid = 1,
                Points = 0,
            });
            Players.Add(new User()
            {
                Email = "nobitches@gmail.com",
                Pswrd = "1234",
                Username = "MegaMind",
                Rankid = 3,
                Points = 0,
            });
        }
    }
    internal class QuestionService
    {
        public List<Question> Questions { get; set; }
        public QuestionService()
        {
            FillList();
        }
        private async void FillList()
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
        private async void FillList()
        {
            Ranks = new List<Rank>();
            Ranks.Add(new Rank()
            {
                Rankid = 1,
                RankName = "Admin"
            });
            Ranks.Add(new Rank()
            {
                Rankid = 2,
                RankName = "Master"
            });
            Ranks.Add(new Rank()
            {
                Rankid = 3,
                RankName = "Rookie"
            });
        }
    }
    internal class QuestionStatusService
    {
        public List<Status> QuestionStatuses { get; set; }
        public QuestionStatusService()
        {
            FillList();
        }
        private async void FillList()
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
        private async void FillList()
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