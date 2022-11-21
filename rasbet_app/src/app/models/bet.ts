import { IBetType } from "./betType";
import { IGame } from "./game";
import { IOdd } from "./odd";
import { IUser } from "./user";

export type betState = "Open" | "Won" | "Lost";

export interface ISelection {
  id: number;
  odd: IOdd;
  oddValue: number;
  betType: IBetType;
  game: IGame;
  win: boolean;
}

export interface IBet {
  id: number;
  amount: number;
  wonValue: number;
  start: Date;
  end?: Date;
  user: IUser;
  state: betState;
}

export interface IBetSimple extends IBet {
  selection: ISelection;
}

export interface IBetMultiple extends IBet {
  oddMultiple: number;
  selections: ISelection[];
}
