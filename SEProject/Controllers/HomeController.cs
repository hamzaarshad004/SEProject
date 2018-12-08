using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEProject.Models;
using Microsoft.AspNet.Identity;

namespace SEProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SchoolBookDBEntities db = new SchoolBookDBEntities();
            List<School> Schools = db.Schools.ToList();
            List<SchoolViewModel> schools = new List<SchoolViewModel>();
            List<Rating_Reviews> ratings = db.Rating_Reviews.ToList();
            List<SchoolViewModel> Output = new List<SchoolViewModel>();
            foreach (School obj in Schools)
            {
                SchoolViewModel s = new SchoolViewModel();
                s.SchoolName = obj.Name;
                s.City = obj.City;
                s.PhoneNo = obj.PhoneNo;
                s.file = obj.ImageUrl;
                s.id = obj.SchoolD;
                int Ratings = 0;
                int Ratings_Count = 0;
                foreach (Rating_Reviews r in ratings)
                {
                    if (r.SchoolID == obj.SchoolD)
                    {
                        Ratings += r.Rating;
                        Ratings_Count += 1;
                    }
                }
                if (Ratings_Count == 0)
                {
                    s.Rating = Ratings;
                }
                else
                {
                    s.Rating = Ratings / Ratings_Count;
                }
                schools.Add(s);
            }
            schools.OrderBy(x => x.Rating);
            schools.Reverse();
            int index = 0;
            if (schools.Count() > 6)
            {
                index = 6;
            }
            else
            {
                index = schools.Count();
            }
            for (int i = 0; i < index; i++)
            {
                Output.Add(schools[i]);
            }
            return View(Output);
        }

        public ActionResult Schools()
        {
            SchoolBookDBEntities db = new SchoolBookDBEntities();
            List<School> Schools = db.Schools.ToList();
            List<SchoolViewModel> schools = new List<SchoolViewModel>();
            List<Rating_Reviews> ratings = db.Rating_Reviews.ToList();
            foreach (School obj in Schools)
            {
                SchoolViewModel s = new SchoolViewModel();
                s.SchoolName = obj.Name;
                s.City = obj.City;
                s.PhoneNo = obj.PhoneNo;
                s.file = obj.ImageUrl;
                s.id = obj.SchoolD;
                int Ratings = 0;
                int Ratings_Count = 0;
                foreach (Rating_Reviews r in ratings)
                {
                    if (r.SchoolID == obj.SchoolD)
                    {
                        Ratings += r.Rating;
                        Ratings_Count += 1;
                    }
                }
                if (Ratings_Count == 0)
                {
                    s.Rating = Ratings;
                }
                else
                {
                    s.Rating = Ratings / Ratings_Count;
                }
                schools.Add(s);
            }
            return View(schools);
        }

        public ActionResult Details(int id = 0)
        {
            if (id != 0)
            {
                try
                {
                    SchoolBookDBEntities db = new SchoolBookDBEntities();
                    School school = db.Schools.Find(id);
                    SchoolViewModel obj = new SchoolViewModel();
                    List<Rating_Reviews> ratings = db.Rating_Reviews.ToList();
                    obj.SchoolName = school.Name;
                    obj.City = school.City;
                    obj.Address = school.Address;
                    obj.AdmissionFee = school.Adm_Fee;
                    obj.FeePG_Middle = school.Fee_PG_Middle;
                    obj.FeeMatric_Olevels = school.Fee_Matric_OLevel;
                    obj.PhoneNo = school.PhoneNo;
                    obj.file = school.ImageUrl;
                    obj.id = school.SchoolD;
                    int Ratings = 0;
                    int Ratings_Count = 0;
                    obj.Ratings_List = new List<Rating_Reviews>();
                    foreach (Rating_Reviews r in ratings)
                    {
                        if (r.SchoolID == obj.id)
                        {
                            Ratings += r.Rating;
                            Ratings_Count += 1;
                            obj.Ratings_List.Add(r);
                        }
                    }
                    if (Ratings_Count == 0)
                    {
                        obj.Rating = Ratings;
                    }
                    else
                    {
                        obj.Rating = Ratings / Ratings_Count;
                    }
                    return View("Details", obj);
                }
                catch
                {
                    return RedirectToAction("Schools");
                }
            }
            else
            {
                return RedirectToAction("Schools");
            }
        }

        [HttpPost]
        public ActionResult Rate(int id, Rating_Review model, string url)
        {
            SchoolBookDBEntities db = new SchoolBookDBEntities();
            Rating_Reviews r = new Rating_Reviews();
            var user = db.AspNetUsers.Single(u => u.UserName == User.Identity.Name);
            string name = user.FullName;
            r.AddedBy = name;
            r.SchoolID = id;
            r.Rating = model.Rating;
            if (model.Review != null)
            {
                r.Review = model.Review;
            }
            db.Rating_Reviews.Add(r);
            db.SaveChanges();
            return Redirect(url);
        }
    }
}