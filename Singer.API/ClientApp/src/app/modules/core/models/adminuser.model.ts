export class AdminUser {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   userId: string;

   constructor(id: string, firstName: string, lastName: string, userName: string, userId: string, email: string) {
      this.id = id;
      this.firstName = firstName;
      this.lastName = lastName;
      this.email = email;
      this.userName = userName;
      this.userId = userId;
   }
}
