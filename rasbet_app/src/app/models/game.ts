import { IBetType } from "./betType";

export type GameState = "Open" | "Suspended" | "Finished";
export interface IGame {
  id: number;
  name: string;
  start: Date;
  sport: string;
  state: GameState;
  bets: IBetType[];
  mainBet: IBetType;
}

export interface IActiveGame {
  id: number;
  name: string;
  start: Date;
  sport: string;
  mainBet: IBetType;
}

export interface IGameInfo {
  start: Date;
  state: GameState;
  bets: IBetType[];
}

export interface CollectiveGame extends IGame {
  home: string;
  away: string;
}

export interface IndividualGame extends IGame {
  players: string[];
}
