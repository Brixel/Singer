namespace Singer.DTOs.Users
{
   public interface IUpdateUserDTO
   {
      string FirstName { get; set; }

      string LastName { get; set; }

      string Email { get; set; }
   }
}
