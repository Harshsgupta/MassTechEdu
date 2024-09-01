using MassTechEdu.Data;
using MassTechEdu.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MassTechEdu.Controllers
{
    public class UserController : Controller
    {
        private readonly MasstechEduContext db;
        public UserController(MasstechEduContext db)
        {
            this.db = db;
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        //Add to cart items button
        public IActionResult AddToCart(int id)
        {
            string session = HttpContext.Session.GetString("Email");
            var data = db.SubCourses.Find(id);
            var obj = new Cart()
            {
                SubCourseName = data.SubCourseName,
                CouurseId = data.CourseId,
                ImagePath = data.ImagePath,
                Price = (double)data.Price,
                Suser = session
            };
            db.Carts.Add(obj);
            db.SaveChanges();
            return RedirectToAction("DisplayCart");
        }
        public IActionResult DisplayCart()
        {
            if (HttpContext.Session.GetString("Email").IsNullOrEmpty())
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                var sess = HttpContext.Session.GetString("Email");
                var prod = db.Carts.Where(x => x.Suser == sess).ToList();
                return View(prod);
            }
        }
        public IActionResult Deleteitem(int id)
        {
            var data = db.Carts.Find(id);
            db.Carts.Remove(data);
            db.SaveChanges();
            return RedirectToAction("DIsplayCart");
        }

        //Course View

        public IActionResult ViewCourse()
        {
            var courses = db.Courses.ToList();
            return View(courses);
        }

        //SubCourse View

        public IActionResult ViewSubCourse(int courseId)
        {
            var subCourses = db.SubCourses
                .Where(sc => sc.CourseId == courseId)
                .ToList();
            if (subCourses == null || !subCourses.Any())
            {
                return NotFound("No sub-courses found for the specified course.");
            }
            return View(subCourses);
        }

        [HttpGet]
        public IActionResult CreateReview(int courseId)
        {
            var course = db.Courses.Find(courseId);
            if (course == null)
            {
                return NotFound();
            }
            ViewBag.CourseName = course.CourseName;
            return View(new Review { Courseid = courseId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateReview(Review review)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                review.Userid = int.Parse(userId);
                review.DateCreated = DateTime.Now;
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("ViewCourse", new { id = review.Courseid });
            }
            return View(review);
        }

        public IActionResult MyReviews()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var reviews = db.Reviews
                .Where(r => r.Userid == userId)
                .Include(r => r.Course)
                .ToList();
            return View(reviews);
        }

        [HttpGet]
        public IActionResult SubmitAssignment(int courseId, int subcourseId)
        {
            ViewBag.CourseId = courseId;
            ViewBag.SubcourseId = subcourseId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitAssignment(Assignment assignment, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                assignment.Userid = userId;

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
                return RedirectToAction("ViewSubCourse", new { courseId = assignment.Courseid });
            }
            return View(assignment);
        }

        public IActionResult MyAssignments()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var assignments = db.Assignments
                .Where(a => a.Userid == userId)
                .Include(a => a.Course)
                .Include(a => a.Subcourse)
                .ToList();
            return View(assignments);
        }


        //Payment Gateway
        //[HttpPost]
        //public JsonResult CreateOrder(decimal amount)
        //{
        //    RazorpayClient client=new RazorpayClient(RazorpaySettings.Key,RazorpaySettings.Secret);
        //    Dictionary<string, object> options = new Dictionary<string, object>
        //    {
        //        {"amount",amount*100 },
        //        {"currency","INR" },
        //        {"payment_capture","1" }
        //    };
        //    Order order=client.Order.Create(options);
        //    return Json(order["id"].ToString());
        //}
    }
}
