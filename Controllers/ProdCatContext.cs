using Microsoft.EntityFrameworkCore;
using ProductsCategories.Models;

namespace ProjName.Models
{
    public class ProdCatContext : DbContext
    {
        public ProdCatContext(DbContextOptions options) : base(options) { }

        // for every model / entity that is going to be part of the db
        // the names of these properties will be the names of the tables in the db
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
