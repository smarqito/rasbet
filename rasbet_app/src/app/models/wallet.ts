import { ITransaction } from "./transaction";
import { IUser } from "./user";

export interface IWallet {
    userId: string;
    balance: number;
}
