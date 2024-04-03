using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Trivia2._0.Models;

public partial class Subject
{
    public int Id { get; set; }
    public string SubjectName { get; set; }
}
