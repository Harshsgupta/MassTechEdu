namespace MassTechEdu.Models
{
    public class SubCourseViewModel
    {
        public int SubCourseId { get; set; }
        public string SubCourseName { get; set; }
        public decimal Price { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public IFormFile? ImagePath { get; set; }
        public ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}
