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
  language: string;
}

export interface IAppUserRegister extends IUserRegister {
  repetePass: string;
  NIF: string;
  DOB: Date;
  notif: boolean;
}

export interface IUserPwd extends IUserLogin {
  newPassword: string;
  repetePass: string;
}

export interface IAppUserChangeProfile extends IUserChangeProfile {
  coin: string;
  notif: boolean;
}

export interface IAppUserChangeSensitive extends IUserChangeSensitive {
  iban: string;
  phone: string;
}

export interface IUserChangeProfile {
  name: string;
  lang: string;
}

export interface IUserChangeSensitive {
  pass: string;
}

export class UserChangeProfile implements IUserChangeProfile {
  name: string = "";
  lang: string = "";

  constructor(init?: IUserChangeProfile) {
    Object.assign(this, init);
  }
}
export class UserChangeSensitive implements IUserChangeSensitive {
  pass: string = "";

  constructor(init?: IAppUserChangeProfile) {
    Object.assign(this, init);
  }
}
export class AppUserChangeProfile implements IAppUserChangeProfile {
  name: string = "";
  lang: string = "";
  coin: string = "";
  notif: boolean = false;

  constructor(init?: IAppUserChangeProfile) {
    Object.assign(this, init);
  }
}
export class AppUserChangeSensitive implements IAppUserChangeSensitive {
  pass: string = "";
  iban: string = "";
  phone: string = "";

  constructor(init?: IAppUserChangeProfile) {
    Object.assign(this, init);
  }
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

export class UserRegisterFormValues implements IUserRegister {
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
