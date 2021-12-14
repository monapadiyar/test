using My_School_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace My_School_Assignment.Controllers
{

    public class UserController : Controller
    {
        MySchoolEntities _DbContext = new MySchoolEntities();


        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership mb)
        {
            
            bool b = _DbContext.Users.Any(y => y.Username.ToLower() == mb.Username.ToLower() && y.Password == mb.Password);
            if (b.Equals(true))
            {
                bool CheckAdmin = _DbContext.Admins.Any(x => x.Username.ToLower() == mb.Username.ToLower());
                if(CheckAdmin.Equals(false))
                {
                    FormsAuthentication.SetAuthCookie(mb.Username, false);
                    var tid = _DbContext.Teachers.Where(x => x.email.ToLower() == mb.Username.ToLower()).SingleOrDefault().T_Id;
                    TempData["tid1"] = tid;
                    return RedirectToAction("StudentDetail");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(mb.Username, false);
                    return RedirectToAction("TeacherDetail");   
                }
            }
            else
            {

                ModelState.AddModelError("", "Invalid Username and password");
                return View();
            }

        }

       
        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Students()
        {
            List<combineEntity> GridView = (from s in _DbContext.students
                                            join
                                            c in _DbContext.classes1 on s.classid equals c.classid
                   
                                            select new combineEntity
                                            {
                                                id = s.id,
                                                studentname = s.studentname,
                                                classname = c.classname,
                                                studob1 = s.dateofbirth,


                                            }).ToList();
           

            foreach (var a in GridView)
            {
                a.studob = a.studob1.Value.ToString("MM/dd/yyyy");
            }

            return View(GridView.OrderBy(s => s.studentname));
        }

        [Authorize(Roles = "Teacher")]
        public ActionResult StudentDetail()
        {

            var tid = (int)TempData["tid1"];
            TempData.Keep();
            List<combineEntity> GridView = (from s in _DbContext.students join
                                            c in _DbContext.classes1 on s.classid equals c.classid
                                            join acs in _DbContext.AssignClass2 on c.classid equals acs.C_Id
                                            where acs.T_Id == tid
                                            select new combineEntity
                                            {
                                                id = s.id,
                                                studentname = s.studentname,
                                                classname = c.classname,
                                                studob1 = s.dateofbirth,
                                               }).ToList();
            ViewBag.TeacherId = tid;

            foreach (var a in GridView)
            {
                a.studob = a.studob1.Value.ToString("MM/dd/yyyy");
            }

            return View(GridView.OrderBy(s => s.studentname));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Teacher()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Teacher(SignupModel sm)
        {
            Teacher tr = new Teacher();


            bool b = _DbContext.Teachers.Any(y => y.email.ToLower() == sm.email.ToLower());
            if (b.Equals(false))
            {
                tr.firstname = sm.firstname;
                tr.lastname = sm.lastname;
                tr.dateofjoin = sm.dateofjoin.Value;
                tr.dateofbirth = sm.dateofbirth.Value;
                tr.email = sm.email;
                tr.password1 = sm.password;

                _DbContext.Teachers.Add(tr);
                _DbContext.SaveChanges();

                User us = new User();
                us.Username = sm.email;
                us.Password = sm.password;
                _DbContext.Users.Add(us);
                _DbContext.SaveChanges();

                UserRoleMapping urm = new UserRoleMapping();
                urm.UserId = us.Id;
                urm.RoleId = (int)CommanEnum.RoleEnum.Role;
                _DbContext.UserRoleMappings.Add(urm);
                _DbContext.SaveChanges();

                return RedirectToAction("TeacherDetail", "User");
            }
            else
            {
                ModelState.AddModelError("", "This email already exist.. Please enter new one!!");
                return View();
            }

        }


        public PartialViewResult ClassShow(int td1)
        {
            List<classModel> list =
                (from c in _DbContext.classes1
                 select new classModel
                 {
                     classid = c.classid,
                     classname = c.classname
                 }).ToList();


            var List = (from c in _DbContext.AssignClass2
                        where c.T_Id == td1
                        select c.C_Id).ToList();

            foreach (var a in list)
            {
                a.classselected = List.Where(x => x == a.classid).Any() ? true : false;
            }


            ViewBag.ClassList = new SelectList(list, "classid", "classname", "classselected");
            return PartialView("_ClassShow");
        }

        [HttpPost]
        public JsonResult AssignClass(int teacherId, List<int> classArray)
        {
            AssignClass2 ac;

            List<AssignClass2> cs = _DbContext.AssignClass2.ToList();

            bool a = cs.Where(x => x.T_Id.Equals(teacherId)).Any() ? true : false;
            List<AssignClass2> listAc = new List<AssignClass2>();

            if (a.Equals(true))
            {
                List<AssignClass2> ca = _DbContext.AssignClass2.ToList();
                var cas = ca.Where(x => x.T_Id.Equals(teacherId));

                _DbContext.AssignClass2.RemoveRange(cas);
                _DbContext.SaveChanges();
            }
            for (var i = 0; i < classArray.Count; i++)
            {
                ac = new AssignClass2();

                ac.T_Id = teacherId;
                ac.C_Id = classArray[i];

                listAc.Add(ac);

            }
            _DbContext.AssignClass2.AddRange(listAc);
            _DbContext.SaveChanges();

            return Json(0);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult TeacherDetail()
        {
            List<SignupModel> sm = (from tc in _DbContext.Teachers

                                    select new SignupModel
                                    {
                                        T_Id = tc.T_Id,
                                        firstname = tc.firstname,
                                        lastname = tc.lastname,
                                        dateofbirth = tc.dateofbirth,
                                        dateofjoin = tc.dateofjoin,
                                        email = tc.email,
                                    }).ToList();
            foreach (var c in sm)
            {
               
                c.classname = GetClassesName(c.T_Id);
            }

            return View(sm);
        }

        private string GetClassesName(int T_Id)
        {
            var a = _DbContext.AssignClass2.Where(x => x.T_Id == T_Id).Select(x => x.C_Id);

            var aa = _DbContext.classes1.Where(x => a.Contains(x.classid)).Select(x => x.classname).ToList();

            return aa.Any() ? aa.Aggregate((i, j) => i + "," + j).ToString() : "";
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteTeacher(int td)
        {


            var temail = _DbContext.Teachers.Where(x => x.T_Id == td).Select(x => x.email).FirstOrDefault();

            List<AssignClass2> ca = _DbContext.AssignClass2.ToList();
            var id = _DbContext.Users.Where(x => x.Username == temail).Select(x => x.Id).FirstOrDefault();
           List<UserRoleMapping>  urm = _DbContext.UserRoleMappings.Where(x => x.UserId == id).ToList();
            _DbContext.UserRoleMappings.RemoveRange(urm);

            User us = _DbContext.Users.Find(id);
            _DbContext.Users.Remove(us);

            var cas = ca.Where(x => x.T_Id.Equals(td));
            _DbContext.AssignClass2.RemoveRange(cas);

            Teacher tc = _DbContext.Teachers.Find(td);

            _DbContext.Teachers.Remove(tc);

            _DbContext.SaveChanges();
            return RedirectToAction("TeacherDetail");
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditTeacher(int? td)
        {
            SignupModel sm = new SignupModel();
            Teacher tc = _DbContext.Teachers.Find(td);
            sm.T_Id = tc.T_Id;
            sm.firstname = tc.firstname;
            sm.lastname = tc.lastname;
            sm.dateofjoin = tc.dateofjoin;
            sm.t_doj1 = tc.dateofjoin.ToString("MMM/dd/yyyy");
            sm.dateofbirth = tc.dateofbirth;
            sm.t_dob1 = tc.dateofbirth.ToString("MMM/dd/yyyy");
            sm.email = tc.email;

            return View(sm);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditTeacher1(SignupModel sm)
        {
            Teacher tc = _DbContext.Teachers.Find(sm.T_Id);

            List<Teacher> tcl = _DbContext.Teachers.ToList();

            tc.firstname = sm.firstname;
            tc.lastname = sm.lastname;
            tc.dateofjoin = Convert.ToDateTime(sm.t_doj1);
            tc.dateofbirth = Convert.ToDateTime(sm.t_dob1);

            _DbContext.SaveChanges();
            return RedirectToAction("TeacherDetail");
        }


        [HttpPost]
        public JsonResult CheckEmail(SignupModel data1)
        {
            Teacher tc = _DbContext.Teachers.Find(data1.T_Id);
            List<Teacher> tcl = _DbContext.Teachers.ToList();
            bool b = tcl.Any(y => y.email.ToLower() == data1.email.ToLower());
            if (tc.email == data1.email)
                return Json(2);
            if (b.Equals(false))
                return Json(1);
            else  
                return Json(0);

        }
    }

}