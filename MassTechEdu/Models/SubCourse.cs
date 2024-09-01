using System;
using System.Collections.Generic;

namespace MassTechEdu.Models;

public partial class SubCourse
{
    public int SubCourseId { get; set; }

    public string SubCourseName { get; set; } = null!;

    public decimal Price { get; set; }

    public int CourseId { get; set; }

    public string ImagePath { get; set; } = null!;

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<PaymentDetail> PaymentDetails { get; set; } = new List<PaymentDetail>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
