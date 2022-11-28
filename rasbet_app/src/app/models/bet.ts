export type betState = "Open" | "Won" | "Lost";

export interface ISelection {
  id: number;
  oddId: number;
  oddValue: number;
  betTypeId: number;
  gameId: number;
  win: boolean;
}

export interface IBet {
  id: number;
  amount: number;
  wonValue: number;
  start: Date;
  end?: Date;
  user: string;
  state: betState;
}

export interface IBetSimple extends IBet {
  selection: ISelection;
}

export interface IBetMultiple extends IBet {
  oddMultiple: number;
  selections: ISelection[];
}

export interface ICreateBet {
  amount: number;
  userId: string;
}

export interface ICreateSelection {
  betTypeId: number;
  oddId: number;
  odd: number;
  gameId: number;
}

export interface ICreateBetSimple extends ICreateBet {
  selection: ICreateSelection;
}

export interface ICreateBetMultiple extends ICreateBet {
  selections: ICreateSelection[];
}
