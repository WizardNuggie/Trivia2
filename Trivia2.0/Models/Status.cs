using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trivia2._0.Models;

public partial class Status
{
    public int Id { get; set; }
    public string CurrentStatus { get; set; }
    public virtual ICollection<Question> Questions { get; } = new List<Question>();
}
