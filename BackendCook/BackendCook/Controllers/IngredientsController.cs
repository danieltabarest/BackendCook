﻿using System;
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
    public class IngredientsController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        // GET: Ingredients
        public async Task<ActionResult> Index()
        {
            return View(await db.Ingredientes.ToListAsync());
        }

        // GET: Ingredients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = await db.Ingredientes.FindAsync(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // GET: Ingredients/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ingredients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IngredientId,Name,Amount")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Ingredientes.Add(ingredient);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        // GET: Ingredients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = await db.Ingredientes.FindAsync(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IngredientId,Name,Amount")] Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredient).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }

        // GET: Ingredients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ingredient ingredient = await db.Ingredientes.FindAsync(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        // POST: Ingredients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ingredient ingredient = await db.Ingredientes.FindAsync(id);
            db.Ingredientes.Remove(ingredient);
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
        /*****************group ingrediente**/
        public async Task<ActionResult> CreateGroup(int? id)
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

                var view = new IngredientMerge { IngredientId = ingredientes.IngredientId, };
                return View(view);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        // GET: IngredientMerge
        public async Task<ActionResult> IndexGroup()
        {
            return View(await db.IngredientMerge.ToListAsync());
        }

        // GET: IngredientMerge/Details/5
        public async Task<ActionResult> DetailsGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMerge IngredientMerge = await db.IngredientMerge.FindAsync(id);
            if (IngredientMerge == null)
            {
                return HttpNotFound();
            }
            return View(IngredientMerge);
        }

        // GET: IngredientMerge/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}



        // POST: IngredientMerge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGroup( IngredientMerge IngredientMerge)
        {
            if (ModelState.IsValid)
            {
                db.IngredientMerge.Add(IngredientMerge);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            
            return View(IngredientMerge);
        }

        // GET: IngredientMerge/Edit/5
        public async Task<ActionResult> EditGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IngredientMerge IngredientMerge = await db.IngredientMerge.FindAsync(id);
            if (IngredientMerge == null)
            {
                return HttpNotFound();
            }
            return View(IngredientMerge);
        }

        // POST: IngredientMerge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IngredientGroupId,Name")] IngredientMerge IngredientMerge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(IngredientMerge).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(IngredientMerge);
        }

 
  

    

    }
}
