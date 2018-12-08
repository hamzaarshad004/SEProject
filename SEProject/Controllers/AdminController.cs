using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEProject.Models;
using Microsoft.AspNet.Identity;
using System.IO;

namespace SEProject.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        // GET: Admin
        public ActionResult Add()
        {
            return View("Add");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SchoolViewModel obj, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file!=null)
                    {
                        //var allowedExtensions = new[] {".Jpg", ".png", ".jpg", "jpeg" };
                        SchoolBookDBEntities db = new SchoolBookDBEntities();
                        School school = new School();
                        school.Name = obj.SchoolName;
                        school.City = obj.City;
                        school.Address = obj.Address;
                        school.PhoneNo = obj.PhoneNo;
                        school.Adm_Fee = obj.AdmissionFee;
                        school.Fee_PG_Middle = obj.FeePG_Middle;
                        school.Fee_Matric_OLevel = obj.FeeMatric_Olevels;
                        school.AddedBy = User.Identity.GetUserId();

                        //var ext = Path.GetExtension(file.FileName);

                        string fileName = Path.GetFileName(file.FileName);
                        string path = "~/Content/Images/" + fileName;
                        file.SaveAs(Server.MapPath(path));

                        school.ImageUrl = path;
                        db.Schools.Add(school);
                        db.SaveChanges();
                        SchoolViewModel vm = new SchoolViewModel();
                        return RedirectToAction("Add");
                    }

                    ViewBag.FileStatus = "School Successfully Added";
                }
                catch
                {
                    ViewBag.FileStatus = "Some Error Occurred";
                }
            }
            return View("Add");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Dashboard()
        {
            SchoolBookDBEntities db = new SchoolBookDBEntities();
            List<School> Schools = db.Schools.ToList();
            List<SchoolViewModel> SchoolsView = new List<SchoolViewModel>();
            foreach(School obj in Schools)
            {
                if (obj.AddedBy == User.Identity.GetUserId())
                {
                    SchoolViewModel school = new SchoolViewModel();
                    school.SchoolName = obj.Name;
                    school.City = obj.City;
                    school.Address = obj.Address;
                    school.PhoneNo = obj.PhoneNo;
                    school.AdmissionFee = obj.Adm_Fee;
                    school.FeePG_Middle = obj.Fee_PG_Middle;
                    school.FeeMatric_Olevels = obj.Fee_Matric_OLevel;
                    school.file = obj.ImageUrl;
                    school.id = obj.SchoolD;
                    SchoolsView.Add(school);
                }
            }

            return View(SchoolsView);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            if (id != 0)
            {
                try
                {
                    SchoolBookDBEntities db = new SchoolBookDBEntities();
                    School obj = db.Schools.Find(id);
                    SchoolViewModel school = new SchoolViewModel();
                    school.SchoolName = obj.Name;
                    school.City = obj.City;
                    school.Address = obj.Address;
                    school.PhoneNo = obj.PhoneNo;
                    school.AdmissionFee = obj.Adm_Fee;
                    school.FeePG_Middle = obj.Fee_PG_Middle;
                    school.FeeMatric_Olevels = obj.Fee_Matric_OLevel;
                    school.file = obj.ImageUrl;
                    school.id = obj.SchoolD;
                    return View(school);
                }
                catch
                {
                    return RedirectToAction("Dashboard");
                }
            }
            else
            {
                return RedirectToAction("Dashboard");
            }
            
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SchoolViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file != null)
                    {
                        SchoolBookDBEntities db = new SchoolBookDBEntities();
                        School obj = db.Schools.Find(model.id);
                        string previous_image_path = obj.ImageUrl;
                        if (System.IO.File.Exists(previous_image_path))
                        {
                            System.IO.File.Delete(previous_image_path);
                        }
                        string fileName = Path.GetFileName(file.FileName);
                        string path = "~/Content/Images/" + fileName;
                        file.SaveAs(Server.MapPath(path));
                        model.file = path;

                        School school = db.Schools.Single(s => s.SchoolD == model.id);
                        school.Name = model.SchoolName;
                        school.Address = model.Address;
                        school.City = model.City;
                        school.Adm_Fee = model.AdmissionFee;
                        school.Fee_PG_Middle = model.FeePG_Middle;
                        school.Fee_Matric_OLevel = model.FeeMatric_Olevels;
                        school.ImageUrl = model.file;
                        school.PhoneNo = model.PhoneNo;
                        db.SaveChanges();

                        ViewBag.FileStatus = "Succesfully Edited";
                    }
                    else
                    {
                        SchoolBookDBEntities db = new SchoolBookDBEntities();
                        School obj = db.Schools.Find(model.id);
                        School school = db.Schools.Single(s => s.SchoolD == model.id);
                        school.Name = model.SchoolName;
                        school.Address = model.Address;
                        school.City = model.City;
                        school.Adm_Fee = model.AdmissionFee;
                        school.Fee_PG_Middle = model.FeePG_Middle;
                        school.Fee_Matric_OLevel = model.FeeMatric_Olevels;
                        school.PhoneNo = model.PhoneNo;
                        db.SaveChanges();

                        ViewBag.FileStatus = "Succesfully Edited";
                    }
                   
                }
                catch
                {
                    ViewBag.FileStatus = "Some error in Updating";
                }
            }
            return View("Edit");
        }

    }
}