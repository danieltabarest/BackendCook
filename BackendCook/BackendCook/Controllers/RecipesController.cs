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

namespace BackendCook.Controllers
{
    public class RecipesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Recipes
        public async Task<ActionResult> Index()
        {
            var recipes = db.Recipes.Include(r => r.Chef).Include(r => r.Cuisine).Include(r => r.Ingredient);
            return View(await recipes.ToListAsync());
        }

        // GET: Recipes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = await db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            ViewBag.ChefId = new SelectList(db.Chefs, "ChefId", "FirstName");
            ViewBag.CuisineId = new SelectList(db.Cuisines, "CuisineId", "Name");
            ViewBag.IngredientId = new SelectList(db.Ingredientes, "IngredientId", "Name");
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RecipeId,Name,Direction,Rating,ChefId,CuisineId,IngredientId")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Recipes.Add(recipe);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ChefId = new SelectList(db.Chefs, "ChefId", "FirstName", recipe.ChefId);
            ViewBag.CuisineId = new SelectList(db.Cuisines, "CuisineId", "Name", recipe.CuisineId);
            ViewBag.IngredientId = new SelectList(db.Ingredientes, "IngredientId", "Name", recipe.IngredientId);
            return View(recipe);
        }

        // GET: Recipes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = await db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.ChefId = new SelectList(db.Chefs, "ChefId", "FirstName", recipe.ChefId);
            ViewBag.CuisineId = new SelectList(db.Cuisines, "CuisineId", "Name", recipe.CuisineId);
            ViewBag.IngredientId = new SelectList(db.Ingredientes, "IngredientId", "Name", recipe.IngredientId);
            return View(recipe);
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RecipeId,Name,Direction,Rating,ChefId,CuisineId,IngredientId")] Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recipe).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ChefId = new SelectList(db.Chefs, "ChefId", "FirstName", recipe.ChefId);
            ViewBag.CuisineId = new SelectList(db.Cuisines, "CuisineId", "Name", recipe.CuisineId);
            ViewBag.IngredientId = new SelectList(db.Ingredientes, "IngredientId", "Name", recipe.IngredientId);
            return View(recipe);
        }

        // GET: Recipes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = await db.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Recipe recipe = await db.Recipes.FindAsync(id);
            db.Recipes.Remove(recipe);
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
