using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Trivia2._0.Models;

public partial class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Pswrd { get; set; }
    public string Username { get; set; }
    public int Points { get; set; }
    public int Questionsadded { get; set; }
    public int Rankid { get; set; }
    public virtual Rank Rank { get; set; }
}
public class UserLog
{
    public string Email { get; set; }
    public string Password { get; set; }
}
public class UserLogResponse
{
    public int playerId { get; set; }
    public string playerEmail { get; set; }
    public string playerPassword { get; set; }
    public string playerName { get; set; }
    public int playerScore { get; set; }
    public PlayerRank playerRank { get; set; }
    public QuestionResponse[] questions { get; set; }
}

public class PlayerRank
{
    public int rankId { get; set; }
    public string rankName { get; set; }
}

public class QuestionResponse
{
    public int questionId { get; set; }
    public QuestionTopic questionTopic { get; set; }
    public int questionPlayerId { get; set; }
    public string questionText { get; set; }
    public string questionAnswerText { get; set; }
    public string questionWrongText1 { get; set; }
    public string questionWrongText2 { get; set; }
    public string questionWrongText3 { get; set; }
    public int questionStatusId { get; set; }
}

public class QuestionTopic
{
    public int topicId { get; set; }
    public string topicName { get; set; }
}
