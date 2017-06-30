using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain;
using BackendCook.Models;

namespace BackendCook.Controllers
{
    public class CuisineMergesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: CuisineMerges
        public async Task<ActionResult> Index()
        {
            var cuisineMerges = db.CuisineMerges.Include(c => c.Cuisine);
            return View(await cuisineMerges.ToListAsync());
        }

        // GET: CuisineMerges/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuisineMerge cuisineMerge = await db.CuisineMerges.FindAsync(id);
            if (cuisineMerge == null)
            {
                return HttpNotFound();
            }
            return View(cuisineMerge);
        }

        // GET: CuisineMerges/Create
        public ActionResult Create()
        {
            ViewBag.CuisineId = new SelectList(db.Cuisines, "CuisineId", "Name");
            return View();
        }

        // POST: CuisineMerges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CuisineMergeId,Name,CuisineId")] CuisineMerge cuisineMerge)
        {
            if (ModelState.IsValid)
            {
                db.CuisineMerges.Add(cuisineMerge);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CuisineId = new SelectList(db.Cuisines, "CuisineId", "Name", cuisineMerge.CuisineId);
            return View(cuisineMerge);
        }

        // GET: CuisineMerges/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuisineMerge cuisineMerge = await db.CuisineMerges.FindAsync(id);
            if (cuisineMerge == null)
            {
                return HttpNotFound();
            }
            ViewBag.CuisineId = new SelectList(db.Cuisines, "CuisineId", "Name", cuisineMerge.CuisineId);
            return View(cuisineMerge);
        }

        // POST: CuisineMerges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CuisineMergeId,Name,CuisineId")] CuisineMerge cuisineMerge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuisineMerge).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CuisineId = new SelectList(db.Cuisines, "CuisineId", "Name", cuisineMerge.CuisineId);
            return View(cuisineMerge);
        }

        // GET: CuisineMerges/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuisineMerge cuisineMerge = await db.CuisineMerges.FindAsync(id);
            if (cuisineMerge == null)
            {
                return HttpNotFound();
            }
            return View(cuisineMerge);
        }

        // POST: CuisineMerges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CuisineMerge cuisineMerge = await db.CuisineMerges.FindAsync(id);
            db.CuisineMerges.Remove(cuisineMerge);
            await db.SaveChangesAsync();
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
