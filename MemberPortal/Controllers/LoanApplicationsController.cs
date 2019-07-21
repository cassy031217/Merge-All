using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MemberPortal.Models;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;

namespace MemberPortal.Controllers
{
    public class LoanApplicationsController : Controller
    {
        private PortalModel db = new PortalModel();

        // GET: LoanApplications
        public ActionResult Index()
        {

            return View(db.LoanApplications.ToList());

        }

        // GET: LoanApplications/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }

            if (loanApplication == null)
            {
                return RedirectToAction("NoApplicationFound", "LoanApplications");
            }

            if (loanApplication.VerificationStatus == "Pending")
            {
                return RedirectToAction("OngoingVerify", "LoanApplications", new { Id = loanApplication.LAno });
            }
            else if (loanApplication.VerificationStatus ==null)
            {
                return RedirectToAction("Verify", "LoanApplications", new { Id = loanApplication.LAno });
            }
            else if (loanApplication.VerificationStatus == "Defered")
            {
                return RedirectToAction("Defered", "LoanApplications", new { Id = loanApplication.LAno });
            }
            else if (loanApplication.VerificationStatus == "Passed" && loanApplication.EvaluationStatus == null)
            {

                return RedirectToAction("Verified", "LoanApplications", new { Id = loanApplication.LAno });
            }
            else if (loanApplication.EvaluationStatus == "Declined" && loanApplication.ApprovalStatus == null)
            {
                return RedirectToAction("EvalDeclined", "LoanApplications", new { Id = loanApplication.LAno });
            }
            else if (loanApplication.EvaluationStatus == "Passed" && loanApplication.ApprovalStatus == null)
            {
                return RedirectToAction("EvalPassed", "LoanApplications", new { Id = loanApplication.LAno });
            }

            else if (loanApplication.EvaluationStatus == "Conditional" && loanApplication.ApprovalStatus == null && loanApplication.IsAgreeToCondition ==false)
            {
                return RedirectToAction("EvalConditional", "LoanApplications", new { Id = loanApplication.LAno });
            }

            else if (loanApplication.EvaluationStatus == "Conditional")
            {
                return RedirectToAction("EvalConditional", "LoanApplications", new { Id = loanApplication.LAno });
            }

            else if (loanApplication.ApprovalStatus == "Approved" && loanApplication.ReleaseDate == null)
            {
                return RedirectToAction("Approved", "LoanApplications", new { Id = loanApplication.LAno });
            }

            else if (loanApplication.ApprovalStatus == "DisApproved")
            {
                return RedirectToAction("DisApproved", "LoanApplications", new { Id = loanApplication.LAno });
            }

            else if (loanApplication.ApprovalStatus == "Approved" && loanApplication.ReleaseDate != null)
            {
                return RedirectToAction("Released", "LoanApplications", new { Id = loanApplication.LAno });
            }


            TempData["LANo"] = id;


            return View(loanApplication);

        }

        public ActionResult EvalConditional_Requirements(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EvalConditional_Requirements(LoanApplication loanApplication)
        {
            if (ModelState.IsValid)
            {
                var LoanApplicationInDb = db.LoanApplications.Single(c => c.LAno == loanApplication.LAno);

                //if (Request.Files.Count > 0)
                //{
                //    foreach (string fileName in Request.Files)
                //    {
                //        HttpPostedFileBase file = Request.Files[fileName];
                //        string _fileName = Path.GetFileNameWithoutExtension(file.FileName);

                //        //To get file extension
                //        string FileExtension = Path.GetExtension(file.FileName);

                //        //Add Current Date to Attached File Name
                //        _fileName = Global.Cifkey + _fileName.Trim() + FileExtension;

                //        //Get Upload path from web.config File appsettings.
                //        string UploadPath = ConfigurationManager.AppSettings["UploadPath"].ToString();

                //        //it's Create complete path to store in server
                //        //path here : UploadPath = UploadPath + _fileName
                //        string CompletePath = UploadPath + _fileName;

                //        if (fileName == "uploadBtn")
                //        {
                //            LoanApplicationInDb.ApplicationFormPath = CompletePath;
                //        }
                //        else if (fileName == "uploadBtn1")
                //        {
                //            LoanApplicationInDb.File1Path = CompletePath;
                //        }
                //        else if (fileName == "uploadBtn2")
                //        {
                //            LoanApplicationInDb.File2Path = CompletePath;
                //        }
                //        else if (fileName == "uploadBtn3")
                //        {
                //            LoanApplicationInDb.File3Path = CompletePath;
                //        }
                //        else if (fileName == "uploadBtn4")
                //        {
                //            LoanApplicationInDb.File4Path = CompletePath;
                //        }
                //        else if (fileName == "uploadBtn5")
                //        {
                //            LoanApplicationInDb.File5Path = CompletePath;
                //        }
                        

                //        file.SaveAs(CompletePath);
                //    }
                    
                //}
                LoanApplicationInDb.VerificationStatus = "Passed";
                LoanApplicationInDb.IsAgreeToCondition = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: LoanApplications/Create
        public ActionResult Create()
        {
            ViewBag.ProductList = new SelectList(db.LoanProductsMasters, "Productname", "Productname");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppliedAmount,ApplicationDate,Product,TermsQty")] LoanApplication loanApplication)
        {
            if (ModelState.IsValid)
            {
                loanApplication.MemberCode = Global.Cifkey;
                loanApplication.LoanAmount = Convert.ToDecimal(0.00);
                loanApplication.IsAddRequirement = false;
                loanApplication.IsAgreeToCondition = false;
                loanApplication.IsChangeLoanAmount = false;
                loanApplication.IsChangeTerm = false;
                loanApplication.ApplicationDate = DateTime.Now;
                db.LoanApplications.Add(loanApplication);
                db.SaveChanges();
                //TempData["LANo"] = loanApplication.LAno;
                return RedirectToAction("Verify", new { id = loanApplication.LAno });
            }

            return View(loanApplication);
        }

        // GET: LoanApplications/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.ProductList = new SelectList(db.LoanProductsMasters, "Productname", "Productname");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoanApplication loanApplication)

        {
            if (ModelState.IsValid)
            {


                var ent = db.Set<LoanApplication>().SingleOrDefault(o => o.LAno == loanApplication.LAno);
                ent.AppliedAmount = loanApplication.AppliedAmount;
                ent.Product = loanApplication.Product;
                ent.TermsQty = loanApplication.TermsQty;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loanApplication);
        }

        public ActionResult Verify(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

               LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Verify(LoanApplication loanApplication)

        {
            if (ModelState.IsValid)
            {
                var LoanApplicationInDb = db.LoanApplications.Single(c => c.LAno == loanApplication.LAno);
                LoanApplicationInDb.VerificationStatus = "Pending";
                //if (Request.Files.Count > 0)
                //{
                //    foreach (string fileName in Request.Files)
                //    {
                //        HttpPostedFileBase file = Request.Files[fileName];
                //        string _fileName = Path.GetFileNameWithoutExtension(file.FileName);

                //        //To get file extension
                //        string FileExtension = Path.GetExtension(file.FileName);

                //        //Add Current Date to Attached File Name
                //        _fileName = Global.Cifkey + _fileName.Trim() + FileExtension;

                //        //Get Upload path from web.config File appsettings.
                //        string UploadPath = ConfigurationManager.AppSettings["UploadPath"].ToString();

                //        //it's Create complete path to store in server
                //        //path here : UploadPath = UploadPath + _fileName
                //        string CompletePath = UploadPath + _fileName;

                //        if (fileName == "uploadBtn")
                //        {
                //            LoanApplicationInDb.ApplicationFormPath = CompletePath;
                //        }
                //        else if (fileName == "uploadBtn1")
                //        {
                //            LoanApplicationInDb.File1Path = CompletePath;
                //        }
                //        else if (fileName == "uploadBtn2")
                //        {
                //            LoanApplicationInDb.File2Path = CompletePath;
                //        }
                //        else if (fileName == "uploadBtn3")
                //        {
                //            LoanApplicationInDb.File3Path = CompletePath;
                //        }
                //        else if (fileName == "uploadBtn4")
                //        {
                //            LoanApplicationInDb.File4Path = CompletePath;
                //        }
                //        else if (fileName == "uploadBtn5")
                //        {
                //            LoanApplicationInDb.File5Path = CompletePath;
                //        }


                //        file.SaveAs(CompletePath);
                //    }
                //    LoanApplicationInDb.VerificationStatus = "Pending";
                //}
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GoodJob()
        {
            return View();
        }

        public ActionResult OngoingVerify()
        {
            return View();
        }

        public ActionResult Defered(int?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }
        public ActionResult Verified(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }
        [HttpPost]
        public ActionResult Verified(LoanApplication loanApplication)
        {

            return View(loanApplication);
        }

        public ActionResult EvalDeclined(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["LANo"] = id;
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }
        public ActionResult EvalConditional(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["LANo"] = id;
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EvalConditional(LoanApplication loanApplication)
        {

            int LaNo = int.Parse(TempData["LANo"].ToString());
            if (ModelState.IsValid)
            {


                string strConString = SystemSettings.ConnectionStrings.Portal_CONNECTIONSTRING;

                using (SqlConnection con = new SqlConnection(strConString))
                {
                    con.Open();
                    string query = "Update [LoanApplication] SET IsAgreeToCondition=" + "'" + loanApplication.IsAgreeToCondition + "'" + " where LaNo=" + LaNo;
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.ExecuteNonQuery();
                }
                if (loanApplication.IsAddRequirement == true)
                {
                    return RedirectToAction("EvalConditional_Requirements", "LoanApplications", new { Id = LaNo });
                }
                else 
                {
                    return RedirectToAction("Index", "LoanApplications");
                }
            }


            return View(loanApplication);
        }
        public ActionResult EvalPassed()
        {
            return View();
        }

        public ActionResult Approved(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["LANo"] = id;
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }
        public ActionResult DisApproved(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TempData["LANo"] = id;
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }
        
        public ActionResult Released()
        {
            return View();
        }


        // GET: LoanApplications/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            if (loanApplication == null)
            {
                return HttpNotFound();
            }
            return View(loanApplication);
        }

        // POST: LoanApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoanApplication loanApplication = db.LoanApplications.Find(id);
            db.LoanApplications.Remove(loanApplication);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: LoanApplications/Delete/5

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
