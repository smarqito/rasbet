export interface IOdd {
  id: number;
  name: string;
  value: number;
}

export interface IChangeOdd {
  specialistId: string;
  betTypeId: number;
  newOdds: Map<number, number>;
}

export class ChangeOddValues implements IChangeOdd {
  specialistId: string = "";
  betTypeId: number = -1;
  newOdds: Map<number, number> = new Map();

  constructor(init?: IChangeOdd) {
    Object.assign(this, init);
  }
}
