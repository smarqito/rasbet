export type type = "Deposit" | "Withdraw";

export interface ITransaction {
  userId: string;
  value: number;
  date: Date;
  type: type;
}

export interface ICreateTransaction {
  userId: string;
  value: number;
}

export class CreateDeposit implements ITransaction {
  userId: string = "";
  value: number = 0;
  date: Date = new Date();
  type: type = "Deposit";
  constructor(init?: ITransaction) {
    Object.assign(this, init);
  }
}

export class CreateWithdraw implements ITransaction {
  userId: string = "";
  value: number = 0;
  date: Date = new Date();
  type: type = "Withdraw";
  constructor(init?: ITransaction) {
    Object.assign(this, init);
  }
}
