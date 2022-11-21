import { IGame } from "./game";
import { IOdd } from "./odd";
import { Specialist } from "./user";

export type BetTypeState = "FINISHED" | "UNFINISHED";

export interface IBetType {
  id: number;
  lastUpdate: Date;
  state: BetTypeState;
  specialist: Specialist;
  gameId: IGame;
  odds: IOdd[];
}

export interface H2h extends IBetType {
  away: string;
}

export interface IndividualResult extends IBetType {}
