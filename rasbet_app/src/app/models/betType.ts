import { IOdd } from "./odd";

export type BetTypeState = "FINISHED" | "UNFINISHED";

export interface IBetType {
  id: number;
  type: string;
  odds: IOdd[];
}
