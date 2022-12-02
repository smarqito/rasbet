import { configure } from "mobx";
import { createContext } from "react";
import BetStore from "./betStore";
import CommonStore from "./commonStore";
import GameStore from "./gameStore";
import ModalStore from "./modalStore";
import UserStore from "./userStore";
import WalletStore from "./walletStore";

configure({ enforceActions: "always" });

export class RootStore {
  betStore: BetStore;
  gameStore: GameStore;
  userStore: UserStore;
  walletStore: WalletStore;
  modalStore: ModalStore;
  commonStore: CommonStore;

  constructor() {
    this.betStore = new BetStore(this);
    this.gameStore = new GameStore(this);
    this.userStore = new UserStore(this);
    this.walletStore = new WalletStore(this);
    this.modalStore = new ModalStore(this);
    this.commonStore = new CommonStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());
