import { IBet } from "./bet";
import { Specialist } from "./user";

export type GameState = "Open" | "Suspended" | "Finished";

export interface Sport {
  id: number;
  name: string;
}

export interface IGame {
  id: number;
  start: Date;
  sport: Sport;
  state: GameState;
  bets: IBet[];
  specialist: Specialist;
}

export interface CollectiveGame extends IGame {
  home: string;
  away: string;
}

export interface IndividualGame extends IGame {
  players: string[];
}
