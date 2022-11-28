import { IOdd } from "./odd";

export type BetTypeState = "FINISHED" | "UNFINISHED";

export interface IBetType {
  id: number;
  type: string;
  odds: IOdd[];
}

export interface IBetInfo {
  id: number;
  oddsId: number[];
}

export interface H2h extends IBetType {
  away: string;
}

export interface IndividualResult extends IBetType {}
