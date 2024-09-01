using System;
using System.Collections.Generic;

namespace MassTechEdu.Models;

public partial class PaymentDetail
{
    public int PaymentDetailId { get; set; }

    public string PaymentId { get; set; } = null!;

    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public int SubCourseId { get; set; }

    public string SubCourseName { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual SubCourse SubCourse { get; set; } = null!;
}
