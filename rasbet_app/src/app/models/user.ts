import { IBet } from "./bet";
import { IWallet } from "./wallet";

export interface IUser {
  id: string;
  name: string;
  email: string;
  language: string;
  token: string;
}

export interface AppUser extends IUser {
  IBAN: string;
  NIF: string;
  DOB: Date;
  phoneNum: string;
  wallet: IWallet;
  betHistory: IBet[];
  coin: string;
  notif: boolean;
}

export interface Admin {

}

export interface Specialist {
    
}