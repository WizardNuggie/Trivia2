using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trivia2._0.Models;

public partial class Rank
{
    public int Rankid { get; set; }
    public string RankName { get; set; }
    public virtual ICollection<User> Users { get; } = new List<User>();
}
