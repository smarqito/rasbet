import { IBet } from "./bet";
import { IUser } from "./user";

export type GameState = "Open" | "Suspended" | "Finished";

export interface Sport {
  name: string;
}

export interface IGame {
  start: Date;
  sport: Sport;
  state: GameState;
  bets: IBet[];
  specialist: IUser;
}

export interface CollectiveGame extends IGame {
  home: string;
  away: string;
}

export interface IndividualGame extends IGame {
  players: string[];
}
