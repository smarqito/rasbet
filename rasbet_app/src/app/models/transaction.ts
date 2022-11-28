export type type = "Deposit" | "Withdraw";

export interface ITransaction {
  id: number;
  userId: string;
  value: number;
  date: Date;
  type: type;
}

export interface ICreateTransaction {
  userId: string;
  value: number;
}