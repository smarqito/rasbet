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
  iban: string;
  nif: string;
  dob: Date;
  phoneNumber: string;
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
  passwordRepeated: string;
  nif: string;
  dob: string;
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
export interface IUserChangeSensitive {
  password: string;
}

export interface IAppUserChangeSensitive extends IUserChangeSensitive {
  IBAN: string;
  phoneNumber: string;
}

export interface IUserChangeProfile {
  name: string;
  lang: string;
}

export class UserChangeProfile implements IUserChangeProfile {
  name: string = "";
  lang: string = "";

  constructor(init?: IUserChangeProfile) {
    Object.assign(this, init);
  }
}
export class UserChangeSensitive implements IUserChangeSensitive {
  password: string = "";

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
  IBAN: string = "";
  phoneNumber: string = "";
  password: string = "";

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
  passwordRepeated: string = "";
  language: string = "Pt";
  nif: string = "";
  dob: string = new Date().toISOString();
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
