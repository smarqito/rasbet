import { makeObservable, observable } from "mobx";
import { RootStore } from "./rootStore";

export default class WalletStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    makeObservable(this);
    this.rootStore = rootStore;
  }

  @observable coisas: string = "null";
}
