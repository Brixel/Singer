using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Singer.Models;

namespace Singer.Data
{
   public class ApplicationDbContext : IdentityDbContext<User>
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
      {
      }
   }
}
