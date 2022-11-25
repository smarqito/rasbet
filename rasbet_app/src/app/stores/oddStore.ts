import { makeObservable } from "mobx";
import { RootStore } from "./rootStore";

export default class OddStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    makeObservable(this);
    this.rootStore = rootStore;
  }
}
