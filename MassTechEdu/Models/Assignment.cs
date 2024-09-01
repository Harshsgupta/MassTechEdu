using System;
using System.Collections.Generic;

namespace MassTechEdu.Models;

public partial class Assignment
{
    public int Assignmentid { get; set; }

    public int? Courseid { get; set; }

    public int? Subcourseid { get; set; }

    public int? Userid { get; set; }

    public string? AssignmentName { get; set; }

    public string? AssignmentDescription { get; set; }

    public DateTime? DueDate { get; set; }

    public string? FilePath { get; set; }

    public string? FileName { get; set; }

    public int? FileSize { get; set; }

    public DateTime? DateSubmitted { get; set; }

    public virtual Course? Course { get; set; }

    public virtual SubCourse? Subcourse { get; set; }

    public virtual User? User { get; set; }
}
