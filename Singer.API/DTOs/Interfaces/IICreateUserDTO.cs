namespace Singer.DTOs
{
   public interface ICreateUserDTO
   {
      string FirstName { get; }
      string LastName { get; }

      string Email { get; }
      string UserName { get; }
   }
}
