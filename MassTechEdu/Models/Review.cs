using System;
using System.Collections.Generic;

namespace MassTechEdu.Models;

public partial class Review
{
    public int Reviewid { get; set; }

    public int Courseid { get; set; }

    public int Userid { get; set; }

    public int Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual Course? Course { get; set; }

    public virtual User? User { get; set; }
}
