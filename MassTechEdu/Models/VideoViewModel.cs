namespace MassTechEdu.Models
{
    public class VideoViewModel
    {
        public int VideoId { get; set; }
        public string? TopicName { get; set; }
        public string? Url { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }
        public int SubCourseId { get; set; }
        public SubCourse? SubCourse { get; set; }
        public IFormFile? ImagePath { get; set; }
    }
}
