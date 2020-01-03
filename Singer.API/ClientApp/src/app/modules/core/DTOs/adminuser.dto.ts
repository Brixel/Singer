export interface AdminUserDTO {
   id: string;
   firstName: string;
   lastName: string;
   email: string;
   userName: string;
   userId: string;
}

export interface UpdateAdminUserDTO {
   firstName: string;
   lastName: string;
   email: string;
}

export interface CreateAdminUserDTO {
   firstName: string;
   lastName: string;
   email: string;
}
