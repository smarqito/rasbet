import { IBetType } from "./betType";

export type GameState = "Open" | "Suspended" | "Finished";

export interface ISport {
  name: string;
}

export interface IGame {
  id: number;
  state: GameState;
  startTime: Date;
  sportName: string;
  mainBet: IBetType;
}

export interface CollectiveGame extends IGame {
  homeTeam: string;
  awayTeam: string;
}

export interface IndividualGame extends IGame {
  players: string[];
}

export interface IStatistics {
  betCount: number;
  statistics: {[key: number] : number};
}

export interface IActiveGame extends CollectiveGame{
  statistics: IStatistics;
}