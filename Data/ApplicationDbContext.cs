using Microsoft.EntityFrameworkCore;

namespace Singer.Data
{
   public class ApplicationDbContext : DbContext
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
      {
      }
   }
}
