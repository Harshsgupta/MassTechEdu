namespace MassTechEdu.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public IFormFile? ImagePath { get; set; }

        public ICollection<SubCourse> SubCourses { get; set; } = new List<SubCourse>();

        // Navigation property for Videos
        public ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}
