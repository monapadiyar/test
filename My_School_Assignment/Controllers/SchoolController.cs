using My_School_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace My_School_Assignment.Controllers
{
    [Authorize]
    public class SchoolController : Controller
    {
        MySchoolEntities _DbContext = new MySchoolEntities();

        [Authorize(Roles = "Admin")]
        public ActionResult AddClass()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult AddClass(string classname)
        {
            List<classes1> cs = _DbContext.classes1.ToList();
            var a = cs.Any(x => x.classname.ToLower().Equals(classname.ToLower()));
            if (a.Equals(false))
            {
                classes1 cls1 = new classes1();
                cls1.classname = classname;
                _DbContext.classes1.Add(cls1);
                _DbContext.SaveChanges();
                return Json(1);
            }
            else
                return Json(0);

        }


        [Authorize(Roles = "Teacher")]
        [HttpGet]
        public ActionResult AddStudent(int? id3)
        {
            List<GetClass> list = (from c in _DbContext.classes1
                                   join ac in _DbContext.AssignClass2 on c.classid equals ac.C_Id
                                   where ac.T_Id == id3
                                   select new GetClass
                                   {
                                       C_id = c.classid,
                                       c_name = c.classname
                                   }).ToList();
            ViewBag.ClassList = new SelectList(list, "C_id", "c_name");
            return View();
        }


        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult AddStudent1(GetClass gc)
        {

            student st = new student();
            st.classid = gc.C_id;
            st.studentname = gc.studentname;
            st.dateofbirth = gc.dateofbirth;
            _DbContext.students.Add(st);
            _DbContext.SaveChanges();
            return RedirectToAction("StudentDetail", "User");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Classes()
        {
            List<classes1> ViewClass = _DbContext.classes1.ToList();

            return View(ViewClass);
        }


        public void PostDeleteClass(int? id)
        {

            classes1 cs = _DbContext.classes1.Find(id);
            var asign = cs.AssignClass2;

            var stuList = cs.students;

            if (stuList.Any())
            {
                _DbContext.students.RemoveRange(stuList);
                _DbContext.SaveChanges();

            }
            _DbContext.AssignClass2.RemoveRange(asign);
            _DbContext.classes1.Remove(cs);
            _DbContext.SaveChanges();
        }


        public ActionResult DeleteStudent(int id1)
        {
            student st = _DbContext.students.Find(id1);
            _DbContext.students.Remove(st);
            _DbContext.SaveChanges();
            return RedirectToAction("StudentDetail", "User");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditClass(int id2)
        {
            classes1 cs = _DbContext.classes1.Find(id2);
            return View(cs);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult EditClass1(classes1 data1)
        {
            classes1 cs1 = _DbContext.classes1.Find(data1.classid);
            if (cs1.classname == data1.classname)
            {
                return Json(2);
            }
            List<classes1> cs2 = _DbContext.classes1.ToList();
            var a = cs2.Any(x => x.classname.ToLower().Equals(data1.classname.ToLower()));

            if (a.Equals(false))
            {
                cs1.classname = data1.classname;
                _DbContext.SaveChanges();
                return Json(0);
            }
            else
                return Json(1);
        }


        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult EditStudent(int? stid, int? t_id)
        {
            StudentCombineEntity cmEnty = new StudentCombineEntity();
            student st = _DbContext.students.Find(stid);
            List<StudentCombineEntity> list = (from c in _DbContext.classes1
                                               join ac in _DbContext.AssignClass2 on c.classid equals ac.C_Id
                                               where ac.T_Id == t_id
                                               select new StudentCombineEntity
                                               {
                                                   classid = c.classid,
                                                   classname = c.classname
                                               }).ToList();
            ViewBag.ClassList = new SelectList(list, "classid", "classname");
            cmEnty.id = st.id;
            cmEnty.classid = st.classid;
            cmEnty.studentname = st.studentname;
            cmEnty.studob2 = st.dateofbirth;
            cmEnty.studob = st.dateofbirth.Value.ToString("MMM/dd/yyyy");
            return View(cmEnty);
        }

        [Authorize(Roles = "Teacher")]
        [HttpPost]
        public ActionResult EditStudent1(StudentCombineEntity scs)
        {

            student st = _DbContext.students.Find(scs.id);

            st.studentname = scs.studentname;
            st.classid = scs.classid;
            st.dateofbirth = Convert.ToDateTime(scs.studob);
            _DbContext.SaveChanges();
            return RedirectToAction("StudentDetail", "User");
        }


    }
}