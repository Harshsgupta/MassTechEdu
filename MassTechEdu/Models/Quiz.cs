using System;
using System.Collections.Generic;

namespace MassTechEdu.Models;

public partial class Quiz
{
    public int Quizid { get; set; }

    public int? Courseid { get; set; }

    public int? Subcourseid { get; set; }

    public int? Userid { get; set; }

    public string? QuizName { get; set; }

    public string? QuizDescription { get; set; }

    public int? QuizDuration { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public virtual Course? Course { get; set; }

    public virtual SubCourse? Subcourse { get; set; }

    public virtual User? User { get; set; }
}
