using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Trivia2._0.Models;

public partial class User
{
    public string Email { get; set; }
    public string Pswrd { get; set; }
    public string Username { get; set; }
    public int Points { get; set; }
    public int Questionsadded { get; set; }
    public int Rankid { get; set; }
    public virtual ICollection<Question> Questions { get; } = new List<Question>();
    public virtual Rank Rank { get; set; }
}
