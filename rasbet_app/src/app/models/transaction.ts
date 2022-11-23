export type type = "Deposit" | "Withdraw";

export interface ITransaction {
  balance: number;
  date: Date;
  type: type;
}