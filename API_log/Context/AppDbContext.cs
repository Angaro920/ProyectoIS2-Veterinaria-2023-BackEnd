using API_Log.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Log.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options) 
        {   

        }
        public DbSet<Tbl_Empleados>Tbl_Empleados { get; set; }
    }

}
