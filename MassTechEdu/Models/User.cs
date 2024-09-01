using System;
using System.Collections.Generic;

namespace MassTechEdu.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsBlocked { get; set; }

    public string Role { get; set; } = null!;

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
