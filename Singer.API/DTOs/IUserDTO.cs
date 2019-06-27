using Singer.Helpers;

namespace Singer.DTOs
{
   public interface IUserDTO : IIdentifiable 
   {
      string FirstName { get; set; }

      string LastName { get; set; }

      string Email { get; set; }

      string UserName { get; set; }
   }
}
