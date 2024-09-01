using System;
using System.Collections.Generic;

namespace MassTechEdu.Models;

public partial class Video
{
    public int VideoId { get; set; }

    public string TopicName { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int CourseId { get; set; }

    public int SubCourseId { get; set; }

    public string ImagePath { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual SubCourse SubCourse { get; set; } = null!;
}
