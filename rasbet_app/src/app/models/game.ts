import { IBetType } from "./betType";
import { IUser } from "./user";

export type GameState = "Open" | "Suspended" | "Finished";

export interface Sport {
  name: string;
}

export interface IGame {
  start: Date;
  sport: Sport;
  state: GameState;
  bets: IBetType[];
  specialist: IUser;
  mainBet: IBetType;
}

export interface IActiveGame {

}

export interface CollectiveGame extends IGame {
  home: string;
  away: string;
}

export interface IndividualGame extends IGame {
  players: string[];
}
