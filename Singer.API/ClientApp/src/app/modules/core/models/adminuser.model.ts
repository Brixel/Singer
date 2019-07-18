export interface AdminUserDTO{
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
}

export class AdminUser{
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;

   constructor(id: string, firstName: string, lastName: string, userName: string, email:string) {
     this.id = id;
     this.firstName = firstName;
     this.lastName = lastName;
     this.email = email;
     this.userName = userName;
   }
}
