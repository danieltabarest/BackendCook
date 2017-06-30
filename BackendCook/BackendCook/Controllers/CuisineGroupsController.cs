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
    public class CuisineGroupsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: CuisineGroups
        public async Task<ActionResult> Index()
        {
            return View(await db.CuisineGroups.ToListAsync());
        }

        // GET: CuisineGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuisineGroup cuisineGroup = await db.CuisineGroups.FindAsync(id);
            if (cuisineGroup == null)
            {
                return HttpNotFound();
            }
            return View(cuisineGroup);
        }

        // GET: CuisineGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuisineGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CuisineGroupId,Name")] CuisineGroup cuisineGroup)
        {
            if (ModelState.IsValid)
            {
                db.CuisineGroups.Add(cuisineGroup);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cuisineGroup);
        }

        // GET: CuisineGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuisineGroup cuisineGroup = await db.CuisineGroups.FindAsync(id);
            if (cuisineGroup == null)
            {
                return HttpNotFound();
            }
            return View(cuisineGroup);
        }

        // POST: CuisineGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CuisineGroupId,Name")] CuisineGroup cuisineGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cuisineGroup).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cuisineGroup);
        }

        // GET: CuisineGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuisineGroup cuisineGroup = await db.CuisineGroups.FindAsync(id);
            if (cuisineGroup == null)
            {
                return HttpNotFound();
            }
            return View(cuisineGroup);
        }

        // POST: CuisineGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CuisineGroup cuisineGroup = await db.CuisineGroups.FindAsync(id);
            db.CuisineGroups.Remove(cuisineGroup);
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
