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
    public class RecipesController : Controller
    {
        private DataContextLocal db = new DataContextLocal();

        #region Recipes
        // GET: Recipes
        public async Task<ActionResult> Index()
        {
            var recipes = db.Recipes.Include(r => r.Chef);
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
        #endregion

        #region group ingrediente
        /*****************group ingrediente**/
        public async Task<ActionResult> CreateGroupIngredient(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var recipes = await db.Recipes.FindAsync(id);
                if (recipes == null)
                {
                    return HttpNotFound();
                }
                var view = new IngredientGroups { RecipeId = recipes.RecipeId, };
                return View(view);
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        // GET: IngredientGroups
        public async Task<ActionResult> IndexGroup()
        {
            return View(await db.IngredientGroups.ToListAsync());
        }

        // GET: IngredientGroups/Details/5
        public async Task<ActionResult> DetailsGroupIngredient(int? id)
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



        // POST: IngredientGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateGroupIngredient(IngredientGroups ingredientGroups)
        {
            if (ModelState.IsValid)
            {
                db.IngredientGroups.Add(ingredientGroups);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction(string.Format("Details/{0}", ingredientGroups.RecipeId));
            }

            return View(ingredientGroups);
        }

        // GET: IngredientGroups/Edit/5
        public async Task<ActionResult> EditGroupIngredient(int? id)
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
        public async Task<ActionResult> EditGroupIngredient([Bind(Include = "IngredientGroupId,Name")] IngredientGroups ingredientGroups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredientGroups).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ingredientGroups);
        }
        #endregion

        #region group coisine 
        // GET: CuisineGroups
        public async Task<ActionResult> IndexCuisineGroups()
        {
            return View(await db.CuisineGroups.ToListAsync());
        }

        // GET: CuisineGroups/Details/5
        public async Task<ActionResult> DetailsCuisineGroups(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recipes = await db.Recipes.FindAsync(id);
            if (recipes == null)
            {
                return HttpNotFound();
            }
            var view = new CuisineGroup{ RecipeId = recipes.RecipeId, };
            return View(view);
        }

        // GET: CuisineGroups/Create
        public async Task<ActionResult> CreateCuisineGroups(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var recipes = await db.Recipes.FindAsync(id);
            if (recipes == null)
            {
                return HttpNotFound();
            }
            var view = new CuisineGroup { RecipeId = recipes.RecipeId, };
            return View(view);
        }

        // POST: CuisineGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCuisineGroups(CuisineGroup cuisineGroup)
        {
            if (ModelState.IsValid)
            {
                db.CuisineGroups.Add(cuisineGroup);
                await db.SaveChangesAsync();
                //return RedirectToAction("Index");
                return RedirectToAction(string.Format("Details/{0}", cuisineGroup.RecipeId));
            }

            return View(cuisineGroup);
        }

        // GET: CuisineGroups/Edit/5
        public async Task<ActionResult> EditCuisineGroups(int? id)
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
        public async Task<ActionResult> EditCuisineGroups([Bind(Include = "CuisineGroupId,Name")] CuisineGroup cuisineGroup)
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
        public async Task<ActionResult> DeleteCuisineGroups(int? id)
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

        //// POST: CuisineGroups/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    CuisineGroup cuisineGroup = await db.CuisineGroups.FindAsync(id);
        //    db.CuisineGroups.Remove(cuisineGroup);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #endregion
    }
}
