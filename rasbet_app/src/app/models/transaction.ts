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