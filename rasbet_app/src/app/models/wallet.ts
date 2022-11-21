import { ITransaction } from "./transaction";
import { IUser } from "./user";

export interface IWallet {
    id: number;
    user: IUser;
    balance: number;
    transactions: ITransaction[];
}