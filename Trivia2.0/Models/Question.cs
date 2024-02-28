using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trivia2._0.Models;

public partial class Question
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string RightAnswer { get; set; }
    public string WrongAnswer1 { get; set; }
    public string WrongAnswer2 { get; set; }
    public string WrongAnswer3 { get; set; }
    public int UserId { get; set; }
    public int StatusId { get; set; }
    public int SubjectId { get; set; }
    public virtual Status Status { get; set; }
    public virtual Subject Subject { get; set; }
    public virtual User User { get; set; }
}
