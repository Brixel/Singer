using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Singer.Models;

namespace Singer.Data
{
   public class ApplicationDbContext : IdentityDbContext<User,IdentityRole<Guid>, Guid>
   {
      public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options)
      {
      }
   }
}
