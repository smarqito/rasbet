export interface IOdd {
  id: number;
  name: string;
  oddValue: number;
}

export interface IChangeOdd {
  specialistId: string;
  betTypeId: number;
  newOdds: IOdds[];
}

export interface IOdds {
  oddId: number;
  oddValue: number; 
}

export class ChangeOddValues implements IChangeOdd {
  specialistId: string = "";
  betTypeId: number = -1;
  newOdds: IOdds[] = [];

  constructor(init?: IChangeOdd) {
    Object.assign(this, init);
  }
}
