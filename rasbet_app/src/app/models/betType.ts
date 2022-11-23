import { IGame } from "./game";
import { IOdd } from "./odd";
import { IUser } from "./user";

export type BetTypeState = "FINISHED" | "UNFINISHED";

export interface IBetType {
  lastUpdate: Date;
  state: BetTypeState;
  specialist: IUser;
  gameId: IGame;
  odds: IOdd[];
}

export interface H2h extends IBetType {
  away: string;
}

export interface IndividualResult extends IBetType {}
