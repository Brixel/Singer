namespace Singer.DTOs.Users
{
   public class AdminUserDTO : UserDTO
   {
      public string UserName { get; set; }
   }

   public class CreateAdminUserDTO : CreateUserDTO
   {
   }

   public class UpdateAdminUserDTO : UpdateUserDTO
   {
   }
}
