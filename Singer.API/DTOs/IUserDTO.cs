using Singer.Helpers;

namespace Singer.DTOs
{
   public interface IUserDTO : IIdentifiable 
   {
      string Name { get; set; }

      string Email { get; set; }

      string UserName { get; set; }
   }
}
