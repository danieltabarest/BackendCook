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
    public class IngredientGroupsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: IngredientGroups
        public async Task<ActionResult> Index()
        {
            return View(await db.IngredientGroups.ToListAsync());
        }

        // GET: IngredientGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientGroups ingredientGroups = await db.IngredientGroups.FindAsync(id);
            if (ingredientGroups == null)
            {
                return HttpNotFound();
            }
            return View(ingredientGroups);
        }

        // GET: IngredientGroups/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        public async Task<ActionResult> Create(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var ingredientes = await db.Ingredientes.FindAsync(id);
                if (ingredientes == null)
                {
                    return HttpNotFound();
                }

                var view = new IngredientGroups { IngredientId = ingredientes.IngredientId, };
                return View(view);
            }
            catch (Exception ex)
            {
                return View();
            }
        }


        // POST: IngredientGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IngredientGroupId,Name")] IngredientGroups ingredientGroups)
        {
            if (ModelState.IsValid)
            {
                db.IngredientGroups.Add(ingredientGroups);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ingredientGroups);
        }

        // GET: IngredientGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientGroups ingredientGroups = await db.IngredientGroups.FindAsync(id);
            if (ingredientGroups == null)
            {
                return HttpNotFound();
            }
            return View(ingredientGroups);
        }

        // POST: IngredientGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IngredientGroupId,Name")] IngredientGroups ingredientGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredientGroups).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ingredientGroups);
        }

        // GET: IngredientGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientGroups ingredientGroups = await db.IngredientGroups.FindAsync(id);
            if (ingredientGroups == null)
            {
                return HttpNotFound();
            }
            return View(ingredientGroups);
        }

        // POST: IngredientGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            IngredientGroups ingredientGroups = await db.IngredientGroups.FindAsync(id);
            db.IngredientGroups.Remove(ingredientGroups);
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
