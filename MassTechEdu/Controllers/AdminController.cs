using MassTechEdu.Data;
using MassTechEdu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MassTechEdu.Controllers
{
    public class AdminController : Controller
    {
        //[Authorize(Roles = "Admin")]
        private readonly MasstechEduContext db;
        private readonly IWebHostEnvironment env;
        public AdminController(MasstechEduContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Dashboard()
        {
            return View();
        }


        //Add Courses
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCourse(CourseViewModel model)
        {
            if (model.ImagePath != null && model.ImagePath.Length > 0)
            {
                var path = env.WebRootPath;
                var filepath = Path.Combine("Content/Images", model.ImagePath.FileName);
                var fullpath = Path.Combine(path, filepath);
                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(stream);
                }
                var Course = new Course()
                {
                    CourseName = model.CourseName,

                    ImagePath = filepath
                };
                db.Courses.Add(Course);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            TempData["msg"] = "Please upload an image file.";
            return View(model);
        }

        //Add SubCourses
        public IActionResult AddSubCourse()
        {
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCourse(SubCourseViewModel model)
        {
            if (model.ImagePath != null && model.ImagePath.Length > 0)
            {
                var path = env.WebRootPath;
                var filepath = Path.Combine("Content/Images", model.ImagePath.FileName);
                var fullpath = Path.Combine(path, filepath);
                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(stream);
                }
                var SubCourse = new SubCourse()
                {
                    CourseId = model.CourseId,
                    SubCourseName = model.SubCourseName,
                    Price = model.Price,
                    Course = model.Course,
                    ImagePath = filepath
                };
                db.SubCourses.Add(SubCourse);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName", model.CourseId);
            return View(model);
        }

        //UserList
        public IActionResult UsersList()
        {
            var users = GetUsersFromDatabase().ToList();
            return View(users);
        }

        [HttpPost]
        public IActionResult BlockUnblockUser(int userId, bool block)
        {
            var user = db.Users.FirstOrDefault(u => u.UserId == userId);
            if (user != null)
            {
                user.IsBlocked = block;
                db.SaveChanges();
            }
            return RedirectToAction("UsersList");
        }
        private IQueryable<User> GetUsersFromDatabase()
        {
            return db.Users.AsQueryable();
        }

        //Video
        public IActionResult UploadVideo()
        {
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideo(VideoViewModel model)
        {
            if (model.ImagePath != null && model.ImagePath.Length > 0)
            {
                var path = env.WebRootPath;
                var filepath = Path.Combine("Content/Images", model.ImagePath.FileName);
                var fullpath = Path.Combine(path, filepath);
                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    model.ImagePath.CopyTo(stream);
                }
                var Video = new Video()
                {
                    SubCourseId = model.SubCourseId,
                    TopicName = model.TopicName,
                    Url = model.Url,
                    CourseId = model.CourseId,
                    SubCourse = model.SubCourse,
                    ImagePath = filepath
                };
                db.Videos.Add(Video);
                db.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            ViewData["SubCourseId"] = new SelectList(db.SubCourses, "SubCourseId", "SubCourseName", model.SubCourseId);
            return View(model);
        }
        public IActionResult GetSubCourses(int courseId)
        {
            var subCourses = db.SubCourses
                               .Where(sc => sc.CourseId == courseId)
                               .Select(sc => new { sc.SubCourseId, sc.SubCourseName })
                               .ToList();
            return Json(subCourses);
        }

        //VideoList
        public IActionResult VideosList()
        {
            var videos = GetVideosListFromDatabase();
            return View(videos);
        }

        private IEnumerable<VideoListModel> GetVideosListFromDatabase()
        {
            // Group videos by SubCourse and count the number of videos in each group
            var videoList = db.Videos
                .Include(v => v.SubCourse)
                .GroupBy(v => v.SubCourse.SubCourseName)
                .Select(group => new VideoListModel
                {
                    SubCourseName = group.Key,
                    VideoCount = group.Count()
                })
                .ToList();
            return videoList;
        }

        //Reviews
        public IActionResult Reviews()
        {
            var reviews = db.Reviews.Include(r => r.Course).Include(r => r.User).ToList();
            return View(reviews);
        }

        public IActionResult CreateReview()
        {
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReview(Review review)
        {
            if (ModelState.IsValid)
            {
                review.DateCreated = DateTime.Now;
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction(nameof(Reviews));
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName", review.Courseid);
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Email", review.Userid);
            return View(review);
        }

        public IActionResult EditReview(int id)
        {
            var review = db.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName", review.Courseid);
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Email", review.Userid);
            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditReview(int id, Review review)
        {
            if (id != review.Courseid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(review);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Courseid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Reviews));
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName", review.Courseid);
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Email", review.Userid);
            return View(review);
        }

        public IActionResult DeleteReview(int id)
        {
            var review = db.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [HttpPost, ActionName("DeleteReview")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReviewConfirmed(int id)
        {
            var review = db.Reviews.Find(id);
            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction(nameof(Reviews));
        }

        private bool ReviewExists(int id)
        {
            return db.Reviews.Any(e => e.Courseid == id);
        }

        // Quizzes CRUD operations
        public IActionResult Quizzes()
        {
            var quizzes = db.Quizzes.Include(q => q.Course).Include(q => q.Subcourse).ToList();
            return View(quizzes);
        }

        public IActionResult CreateQuiz()
        {
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewData["SubcourseId"] = new SelectList(db.SubCourses, "SubCourseId", "SubCourseName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateQuiz(Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                quiz.DateCreated = DateTime.Now;
                quiz.DateModified = DateTime.Now;
                db.Quizzes.Add(quiz);
                db.SaveChanges();
                return RedirectToAction(nameof(Quizzes));
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName", quiz.Courseid);
            ViewData["SubcourseId"] = new SelectList(db.SubCourses, "SubCourseId", "SubCourseName", quiz.Subcourseid);
            return View(quiz);
        }

        // Add similar Edit and Delete methods for Quizzes

        // Assignments CRUD operations
        public IActionResult Assignments()
        {
            var assignments = db.Assignments.Include(a => a.Course).Include(a => a.Subcourse).Include(a => a.User).ToList();
            return View(assignments);
        }

        public IActionResult CreateAssignment()
        {
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName");
            ViewData["SubcourseId"] = new SelectList(db.SubCourses, "SubCourseId", "SubCourseName");
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAssignment(Assignment assignment, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    assignment.FilePath = "/uploads/" + fileName;
                    assignment.FileName = fileName;
                    assignment.FileSize = (int)file.Length;
                }

                assignment.DateSubmitted = DateTime.Now;
                db.Assignments.Add(assignment);
                db.SaveChanges();
                return RedirectToAction(nameof(Assignments));
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName", assignment.Courseid);
            ViewData["SubcourseId"] = new SelectList(db.SubCourses, "SubCourseId", "SubCourseName", assignment.Subcourseid);
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Email", assignment.Userid);
            return View(assignment);
        }

        public IActionResult EditAssignment(int id)
        {
            var assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName", assignment.Courseid);
            ViewData["SubcourseId"] = new SelectList(db.SubCourses, "SubCourseId", "SubCourseName", assignment.Subcourseid);
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Email", assignment.Userid);
            return View(assignment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAssignment(int id, Assignment assignment, IFormFile file)
        {
            if (id != assignment.Assignmentid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null && file.Length > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        // Delete old file if exists
                        if (!string.IsNullOrEmpty(assignment.FilePath))
                        {
                            var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", assignment.FilePath.TrimStart('/'));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        assignment.FilePath = "/uploads/" + fileName;
                        assignment.FileName = fileName;
                        assignment.FileSize = (int)file.Length;
                    }

                    db.Update(assignment);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentExists(assignment.Assignmentid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Assignments));
            }
            ViewData["CourseId"] = new SelectList(db.Courses, "CourseId", "CourseName", assignment.Courseid);
            ViewData["SubcourseId"] = new SelectList(db.SubCourses, "SubCourseId", "SubCourseName", assignment.Subcourseid);
            ViewData["UserId"] = new SelectList(db.Users, "UserId", "Email", assignment.Userid);
            return View(assignment);
        }

        public IActionResult DeleteAssignment(int id)
        {
            var assignment = db.Assignments.Find(id);
            if (assignment == null)
            {
                return NotFound();
            }
            return View(assignment);
        }

        [HttpPost, ActionName("DeleteAssignment")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAssignmentConfirmed(int id)
        {
            var assignment = db.Assignments.Find(id);
            if (assignment != null)
            {
                // Delete the associated file if it exists
                if (!string.IsNullOrEmpty(assignment.FilePath))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", assignment.FilePath.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                db.Assignments.Remove(assignment);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Assignments));
        }

        private bool AssignmentExists(int id)
        {
            return db.Assignments.Any(e => e.Assignmentid == id);
        }
    }
}
