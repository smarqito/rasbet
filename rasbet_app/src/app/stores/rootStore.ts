import { configure } from "mobx";
import BetStore from "./betStore";
import BetTypeStore from "./betTypeStore";
import GameStore from "./gameStore";
import OddStore from "./oddStore";
import TransactionStore from "./transactionStore";
import UserStore from "./userStore";
import WalletStore from "./walletStore";

configure({enforceActions: "always"});

export class RootStore {
    betStore: BetStore;
    bettypeStore: BetTypeStore;
    gameStore: GameStore;
    oddStore: OddStore;
    transactionStore: TransactionStore;
    userStore: UserStore;
    walletStore: WalletStore;

    constructor() {
        this.betStore = new BetStore();
        this.bettypeStore = new BetTypeStore();
        this.gameStore = new GameStore();
        this.oddStore = new OddStore();
        this.transactionStore = new TransactionStore();
        this.userStore = new UserStore();
        this.walletStore = new WalletStore();
    }
}