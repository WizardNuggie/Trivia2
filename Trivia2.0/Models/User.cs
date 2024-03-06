using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Trivia2._0.Models;

public partial class User
{
    public int Id { get; set; }//id
    public string Email { get; set; }//email entry
    public string Pswrd { get; set; }//pswrd entry
    public string Username { get; set; }//username entry
    public int Points { get; set; }//0
    public int Questionsadded { get; set; }//0
    public int Rankid { get; set; }//3
    public virtual ICollection<Question> Questions { get; } = new List<Question>();
    public virtual Rank Rank { get; set; }//service.addrankstoplayers
}
