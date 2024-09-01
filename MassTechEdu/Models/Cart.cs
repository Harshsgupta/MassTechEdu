using System;
using System.Collections.Generic;

namespace MassTechEdu.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int SubCourseId { get; set; }

    public string? SubCourseName { get; set; }

    public int CouurseId { get; set; }

    public string? ImagePath { get; set; }

    public double Price { get; set; }

    public string? Suser { get; set; }

    public virtual SubCourse SubCourse { get; set; } = null!;
}
