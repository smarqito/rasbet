import { IBet } from "./bet";
import { IWallet } from "./wallet";

export type Role = "AppUser" | "Admin" | "Specialist";

export interface IUser {
  name: string;
  email: string;
  language: string;
  token: string;
  role: Role;
}

export interface AppUserInfo {
  IBAN?: string;
  NIF: string;
  DOB: Date;
  phoneNum: string;
  wallet: IWallet;
  betHistory: IBet[];
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
  role: Role;
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
  language: string = "";
  role: Role = "AppUser";
  NIF: string = "";
  DOB: Date = new Date();
  notif: boolean = true;
  constructor(init?: IAppUserRegister) {
    Object.assign(this, init);
  }
}

export class AdminRegisterFromValues implements IUserRegister {
  name: string = "";
  email: string = "";
  password: string = "";
  language: string = "PT";
  role: Role = "Admin";
  constructor(init?: IUserRegister) {
    Object.assign(this, init);
  }
}

export class SpecialistRegisterFromValues implements IUserRegister {
  name: string = "";
  email: string = "";
  password: string = "";
  language: string = "PT";
  role: Role = "Specialist";
  constructor(init?: IUserRegister) {
    Object.assign(this, init);
  }
}

export const getIn = {
  AppUser: "/homePage",
  Admin: "/adminPage",
  Specialist: "/specialistPage",
};
