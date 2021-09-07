using JobWebSite.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace JobWebSite.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        [HttpGet]
        public ActionResult Contact()
        {
            

            return View();
        }


        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();

            var LoginInfo = new NetworkCredential("abanobyoussef1996@gmail.com", "hana1996");

            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("abanobyoussef1996@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل: " + contact.Name + "<br>" +
                            "بريد المرسل: " + contact.Email + "<br>" +
                            "عنوان الرسالة: " + contact.Subject + "<br>" +
                            "نص الرسالة: <b>" + contact.Meassage + "</b>";
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com",587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = LoginInfo;
            smtpClient.Send(mail);
            return RedirectToAction("Index");
        }


        [Authorize]
        public ActionResult GetJobsPublisher()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.ID
                       where job.User.Id==UserId
                       select app;


            var grouped = from j in Jobs
                          group j by j.Job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              items = gr

                          };

            return View(grouped.ToList());
        }


        [Authorize]
        public ActionResult Details(int id)
        {
            var job = db.Jobs.Find(id);

            if(job==null)
            {
                return HttpNotFound();
            }
            return View(job);
        }


        [Authorize]
        //Home/Apply
        //Home/Apply/id 
        // both are right for developer-made action both will work automaticlly 
        [Authorize]
        public ActionResult Apply(int id)
        {
            Session["JobId"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult Apply(string message )
        {
            var userid = User.Identity.GetUserId();
            var jobid =(int) Session["JobId"];

            var check = db.ApplyForJobs.Where(a => a.JobId == jobid && a.UserId == userid).ToList();

            if(check.Count<1)
            {
                var job = new ApplyForJob()
                {
                    UserId = userid,
                    JobId = jobid,
                    Message = message,
                    ApplyDate = DateTime.Now
                };

                db.ApplyForJobs.Add(job);
                db.SaveChanges();

                ViewBag.res = "تمت الاضافه بنجاح";
            }
            else
            {
                ViewBag.res = "لقد سبق و تقدمت الى نفس الوظيفه";
            }


            return View();
        }

        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }


        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);

            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }


        [Authorize]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["date"] = job.ApplyDate;
            return View(job);
        }

        // POST: Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplyForJob job)
        {
            // MUST send jibid and userid hidden 
            if (ModelState.IsValid)
            {
                job.ApplyDate = (DateTime)Session["date"];
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }



        [Authorize]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            db.ApplyForJobs.Remove(job);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result= db.Jobs.Where(a=>a.JobTitle.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();

            return View(result);
        }
    }
}