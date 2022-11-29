import { IBetType } from "./betType";

export type GameState = "Open" | "Suspended" | "Finished";

export interface ISport {
  name: string;
}

export interface IGame {
  id: number;
  start: Date;
  sport: string;
  mainBet: IBetType;
}

export interface CollectiveGame extends IGame {
  home: string;
  away: string;
}

export interface IndividualGame extends IGame {
  players: string[];
}

export interface IStatistics {
  betCount: number;
  statitics: Map<number, number>;
}

export interface IActiveGame {
  game: CollectiveGame;
  statistic: IStatistics;
}