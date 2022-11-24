import { configure } from "mobx";
import { createContext } from "react";
import BetStore from "./betStore";
import BetTypeStore from "./betTypeStore";
import CommonStore from "./commonStore";
import GameStore from "./gameStore";
import ModalStore from "./modalStore";
import OddStore from "./oddStore";
import TransactionStore from "./transactionStore";
import UserStore from "./userStore";
import WalletStore from "./walletStore";

configure({ enforceActions: "always" });

export class RootStore {
  betStore: BetStore;
  bettypeStore: BetTypeStore;
  gameStore: GameStore;
  oddStore: OddStore;
  transactionStore: TransactionStore;
  userStore: UserStore;
  walletStore: WalletStore;
  modalStore: ModalStore;
  commonStore: CommonStore;

  constructor() {
    this.betStore = new BetStore();
    this.bettypeStore = new BetTypeStore();
    this.gameStore = new GameStore();
    this.oddStore = new OddStore();
    this.transactionStore = new TransactionStore();
    this.userStore = new UserStore(this);
    this.walletStore = new WalletStore();
    this.modalStore = new ModalStore(this);
    this.commonStore = new CommonStore(this);
  }
}

export const RootStoreContext = createContext(new RootStore());
