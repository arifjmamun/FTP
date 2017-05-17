using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Core.Context;
using Core.Models.EntityModels;
using System.IO;
using FileInfo = Core.Models.EntityModels.FileInfo;

namespace Web.Controllers
{
    public class UploadsController : Controller
    {
        private FTPContext db = new FTPContext();

        // GET: Uploads
        public ActionResult Index()
        {
            var uploads = db.Uploads.Include(u => u.Category).Include(u => u.SubCategory);
            return View(uploads.ToList());
        }

        // GET: Uploads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload upload = db.Uploads.Find(id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            return View(upload);
        }

        // GET: Uploads/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            //ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName");
            var listofdrive = DriveInfo.GetDrives().Where(x => x.IsReady && x.DriveType.ToString() == "Fixed").Select(x => new { VolumeLabel = !string.IsNullOrEmpty(x.VolumeLabel) ? x.VolumeLabel : x.Name, x.Name }).ToList();
            ViewBag.DriveLetter = new SelectList(listofdrive, "Name", "VolumeLabel");
            return View();
        }

        // POST: Uploads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UploadId,DriveLetter,Title,CategoryId,SubCategoryId")] Upload upload, ICollection<HttpPostedFileBase> selectedFiles, HttpPostedFileBase thumbnail)
        {
            if (selectedFiles.Count == 0) ModelState.AddModelError("SelectedFiles","You must have to select file to upload");

            if (ModelState.IsValid)
            {
                upload.UploadPath = upload.Category.CategoryName + "/" + upload.SubCategory.SubCategoryName + "/";
                string savePath = upload.DriveLetter + upload.UploadPath;
                db.Uploads.Add(upload);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", upload.CategoryId);
            //ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", upload.SubCategoryId);
            var listofdrive = DriveInfo.GetDrives().Where(x => x.IsReady && x.DriveType.ToString() == "Fixed").Select(x => new { VolumeLabel = !string.IsNullOrEmpty(x.VolumeLabel) ? x.VolumeLabel : x.Name, x.Name }).ToList();
            ViewBag.DriveLetter = new SelectList(listofdrive, "Name", "VolumeLabel", upload.DriveLetter);
            return View(upload);
        }

        // GET: Uploads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload upload = db.Uploads.Find(id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", upload.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", upload.SubCategoryId);
            return View(upload);
        }

        // POST: Uploads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UploadId,Title,CategoryId,SubCategoryId,UploadPath,PublishDate,LastUpdate")] Upload upload)
        {
            if (ModelState.IsValid)
            {
                db.Entry(upload).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", upload.CategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "SubCategoryId", "SubCategoryName", upload.SubCategoryId);
            return View(upload);
        }

        // GET: Uploads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upload upload = db.Uploads.Find(id);
            if (upload == null)
            {
                return HttpNotFound();
            }
            return View(upload);
        }

        // POST: Uploads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Upload upload = db.Uploads.Find(id);
            db.Uploads.Remove(upload);
            db.SaveChanges();
            return RedirectToAction("Index");
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
