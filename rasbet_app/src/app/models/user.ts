export type Role = "AppUser" | "Admin" | "Specialist";

export interface IUser {
  id: string;
  name: string;
  email: string;
  language: string;
  token: string;
  role: Role;
}

export interface IAppUser extends IUser {
  IBAN?: string;
  NIF: string;
  DOB: Date;
  phoneNum?: string;
  coin: string;
  notif: boolean;
}

export interface IUserLogin {
  email: string;
  password: string;
}

export interface IUserRegister {
  name: string;
  email: string;
  password: string;
  repetePass: string;
  language: string;
}

export interface IAppUserRegister extends IUserRegister {
  NIF: string;
  DOB: Date;
  notif: boolean;
}

export interface IUserPwd extends IUserLogin {
  newPassword: string;
  repetePass: string;
}

export class UserPwdFormValues implements IUserPwd {
  email: string = "";
  password: string = "";
  newPassword: string = "";
  repetePass: string = "";
  constructor(init?: IUserPwd) {
    Object.assign(this, init);
  }
}

export class AppUserRegisterFormValues implements IAppUserRegister {
  name: string = "";
  email: string = "";
  password: string = "";
  repetePass: string = "";
  language: string = "Pt";
  role: Role = "AppUser";
  NIF: string = "";
  DOB: Date = new Date();
  notif: boolean = false;
  constructor(init?: IAppUserRegister) {
    Object.assign(this, init);
  }
}

export class AdminRegisterFromValues implements IUserRegister {
  name: string = "";
  email: string = "";
  password: string = "";
  repetePass: string = "";
  language: string = "PT";
  constructor(init?: IUserRegister) {
    Object.assign(this, init);
  }
}

export class SpecialistRegisterFromValues implements IUserRegister {
  name: string = "";
  email: string = "";
  password: string = "";
  repetePass: string = "";
  language: string = "PT";
  constructor(init?: IUserRegister) {
    Object.assign(this, init);
  }
}

export const getIn = {
  AppUser: "/user/homepage",
  Admin: "/admin/homepage",
  Specialist: "/specialist/homepage",
};
