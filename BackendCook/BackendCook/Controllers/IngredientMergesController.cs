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
    public class IngredientMergesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: IngredientMerges
        public async Task<ActionResult> Index()
        {
            var ingredientMerges = db.IngredientMerges.Include(i => i.Ingredient);
            return View(await ingredientMerges.ToListAsync());
        }

        // GET: IngredientMerges/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMerge ingredientMerge = await db.IngredientMerges.FindAsync(id);
            if (ingredientMerge == null)
            {
                return HttpNotFound();
            }
            return View(ingredientMerge);
        }

        // GET: IngredientMerges/Create
        public ActionResult Create()
        {
            ViewBag.IngredientId = new SelectList(db.Ingredientes, "IngredientId", "Name");
            return View();
        }

        // POST: IngredientMerges/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IngredientMergeId,Name,IngredientId")] IngredientMerge ingredientMerge)
        {
            if (ModelState.IsValid)
            {
                db.IngredientMerges.Add(ingredientMerge);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.IngredientId = new SelectList(db.Ingredientes, "IngredientId", "Name", ingredientMerge.IngredientId);
            return View(ingredientMerge);
        }

        // GET: IngredientMerges/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMerge ingredientMerge = await db.IngredientMerges.FindAsync(id);
            if (ingredientMerge == null)
            {
                return HttpNotFound();
            }
            ViewBag.IngredientId = new SelectList(db.Ingredientes, "IngredientId", "Name", ingredientMerge.IngredientId);
            return View(ingredientMerge);
        }

        // POST: IngredientMerges/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IngredientMergeId,Name,IngredientId")] IngredientMerge ingredientMerge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredientMerge).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.IngredientId = new SelectList(db.Ingredientes, "IngredientId", "Name", ingredientMerge.IngredientId);
            return View(ingredientMerge);
        }

        // GET: IngredientMerges/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMerge ingredientMerge = await db.IngredientMerges.FindAsync(id);
            if (ingredientMerge == null)
            {
                return HttpNotFound();
            }
            return View(ingredientMerge);
        }

        // POST: IngredientMerges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IngredientMerge ingredientMerge = await db.IngredientMerges.FindAsync(id);
            db.IngredientMerges.Remove(ingredientMerge);
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
