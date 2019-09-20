using System;
using System.ComponentModel.DataAnnotations.Schema;
using Singer.Helpers;

namespace Singer.Models.Users
{
   public interface IUser : IIdentifiable
   {
      Guid UserId { get; set; }

      User User { get; set; }
   }
}
