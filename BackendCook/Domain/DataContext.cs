using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }


        public System.Data.Entity.DbSet<Domain.Ingredient> Ingredientes { get; set; }

        public System.Data.Entity.DbSet<Domain.Recipe> Recipes { get; set; }

        public System.Data.Entity.DbSet<Domain.Chef> Chefs { get; set; }

        public System.Data.Entity.DbSet<Domain.Cuisine> Cuisines { get; set; }
    }
}
