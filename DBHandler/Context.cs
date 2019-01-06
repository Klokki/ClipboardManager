using Microsoft.EntityFrameworkCore;

namespace DBHandler
{
    public class Context : DbContext
    {
        public DbSet<Model.Clip> Clip { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=db.sqlite");
        }
    }
}
