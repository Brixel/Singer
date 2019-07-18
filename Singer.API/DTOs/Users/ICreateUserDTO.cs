namespace Singer.DTOs.Users
{
   public interface ICreateUserDTO
   {
      string FirstName { get; set; }

      string LastName { get; set; }

      string Email { get; set; }
   }
}
