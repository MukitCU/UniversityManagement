using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UniversityManagement.Models;

namespace UniversityManagement.Controllers
{
    public class AssignCourseController : Controller
    {
        private UniversityManagementMVCDBEntities db = new UniversityManagementMVCDBEntities();

        // GET: AssignCourse
        public ActionResult Index()
        {
            var assignCourses = db.AssignCourses.Include(a => a.Course).Include(a => a.Department).Include(a => a.Teacher);
            return View(assignCourses.ToList());
        }

        // GET: AssignCourse/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignCourse assignCourse = db.AssignCourses.Find(id);
            if (assignCourse == null)
            {
                return HttpNotFound();
            }
            return View(assignCourse);
        }

        // GET: AssignCourse/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName");
            return View();
        }

        // POST: AssignCourse/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignCourseId,DepartmentId,TeacherId,TakenCredit,RemainingCredit,CourseId")] AssignCourse assignCourse)
        {
            if (ModelState.IsValid)
            {
                db.AssignCourses.Add(assignCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", assignCourse.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptName", assignCourse.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", assignCourse.TeacherId);
            return View(assignCourse);
        }

        // GET: AssignCourse/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignCourse assignCourse = db.AssignCourses.Find(id);
            if (assignCourse == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", assignCourse.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptCode", assignCourse.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", assignCourse.TeacherId);
            return View(assignCourse);
        }

        // POST: AssignCourse/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignCourseId,DepartmentId,TeacherId,TakenCredit,RemainingCredit,CourseId")] AssignCourse assignCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assignCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", assignCourse.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DeptCode", assignCourse.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "TeacherName", assignCourse.TeacherId);
            return View(assignCourse);
        }

        // GET: AssignCourse/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignCourse assignCourse = db.AssignCourses.Find(id);
            if (assignCourse == null)
            {
                return HttpNotFound();
            }
            return View(assignCourse);
        }

        // POST: AssignCourse/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssignCourse assignCourse = db.AssignCourses.Find(id);
            db.AssignCourses.Remove(assignCourse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public JsonResult SelectTeacher(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var teachers = db.Teachers.Where(t => t.DepartmentId == id);
            return Json(teachers, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SelectCourse(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var courses = db.Courses.Where(c => c.DepartmentId == id);
            return Json(courses, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CreditToBeTakenAction(int teacherId)
        {

            db.Configuration.ProxyCreationEnabled = false;
            var creditsToBeTaken = db.Teachers.FirstOrDefault(teacher => teacher.TeacherId == teacherId);
            return Json(creditsToBeTaken, JsonRequestBehavior.AllowGet);
            //var creditsToBeTaken = db.Teachers.Where(teacher => teacher.TeacherId == teacherId).Select(teacher => new { teacher.CreditToBeTaken }).FirstOrDefault();

        }


        public JsonResult CourseName(int courseId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var courseName = db.Courses.FirstOrDefault(course => course.CourseId == courseId);
            //var courseName = db.Courses.Where(course => course.CourseId == courseId).Select(course => course.CourseName).FirstOrDefault();
            return Json(courseName, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CourseCredit(int courseId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var courseCredit = db.Courses.FirstOrDefault(course => course.CourseId == courseId);
            //var courseCredit = db.Courses.Where(course => course.CourseId == courseId).Select(course => course.CourseCredit).FirstOrDefault();
            return Json(courseCredit, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
