import { ITransaction } from "./transaction";
import { IUser } from "./user";

export interface IWallet {
    user: IUser;
    balance: number;
    transactions: ITransaction[];
}