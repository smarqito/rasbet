import { IBetType } from "./betType";
import { CollectiveGame, IGame } from "./game";
import { IOdd } from "./odd";

export type betState = "Open" | "Won" | "Lost";

export interface IBet {
  amount: number;
  wonValue: number;
  start: Date;
  end?: Date;
  state: betState;
  user: string;
  selections: ISelectionGame[];
  odd: number
}

export interface ISelectionGame {
  odd: number;
  game: IGame;
  win: boolean;
}

export interface ISelection {
  betType: IBetType;
  oddValue: number;
  odd: IOdd;
  game: CollectiveGame;
}

export interface IBetDetails {
  amount: number;
  userId: string;
}

export interface ISimpleDetails extends IBetDetails {
  selection: ISelection;
}

export interface IMultipleDetails extends IBetDetails {
  selections: ISelection[];
}

export interface ICreateSelection {
  betTypeId: number;
  oddId: number;
  odd: number;
  gameId: number;
}

export interface ICreateBet {
  amount: number;
  userId: string;
}

export interface ICreateBetSimple extends ICreateBet {
  selection: ICreateSelection;
}

export interface ICreateBetMultiple extends ICreateBet {
  selections: ICreateSelection[];
}
