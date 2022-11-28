export interface IOdd {
  id: number;
  name: string;
  value: number;
}

export interface IChangeOdd {
  specialistId: string;
  betTypeId: number;
  newOdds: Map<number, number>
}