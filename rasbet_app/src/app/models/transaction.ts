export interface ITransaction {
    id: number;
    balance: number;
    date: Date;
}

export interface IDeposit extends ITransaction {

}

export interface IWithdraw extends ITransaction {
    
}