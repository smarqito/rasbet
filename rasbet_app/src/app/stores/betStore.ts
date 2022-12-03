import {
  action,
  computed,
  makeObservable,
  observable,
  runInAction,
} from "mobx";
import { toast } from "react-toastify";
import { history } from "../..";
import Agent from "../api/agent";
import {
  ICreateBetMultiple,
  ICreateBetSimple,
  ICreateSelection,
  IMultipleDetails,
  ISelection,
  ISimpleDetails,
} from "../models/bet";
import { IBetType } from "../models/betType";
import { CollectiveGame } from "../models/game";
import { IOdd } from "../models/odd";
import { RootStore } from "./rootStore";

export default class BetStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    makeObservable(this);
    this.rootStore = rootStore;
  }

  @observable simpleBets: ISimpleDetails[] = [];
  @observable betMultiple: IMultipleDetails = {
    selections: [],
    amount: 0,
    userId: "",
  };
  @observable loading = false;

  getLocalStorageItem = (key: string) => {
    return JSON.parse(window.localStorage.getItem(key) ?? "{}");
  };

  setLocalStorageItem = (key: string, item: any) => {
    window.localStorage.setItem(key, JSON.stringify(item));
  };

  removeStorageItem = (key: string) => {
    window.localStorage.removeItem(key);
  };

  @action clearSimple = () => {
    this.simpleBets = [];
  };

  @action clearMultiple = () => {
    this.betMultiple = {
      selections: [],
      amount: 0,
      userId: "",
    };
  };

  @action loadCart() {
    let loaded = JSON.parse(window.localStorage.getItem("betSimple") ?? "{}");
    this.simpleBets = Object.values(loaded);
  }

  @action removeSimpleSelection = (oddId: number) => {
    try {
      let newSimples: ISimpleDetails[] = [];

      for (let index = 0; index < this.simpleBets.length; index++) {
        const element = this.simpleBets[index];
        if (element.selection.odd.id != oddId) newSimples.push(element);
        this.removeStorageItem("betSimple");
      }

      this.simpleBets = newSimples;
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    }
  };

  @action removeMultipleSelection = (oddId: number) => {
    try {
      let newMultiple: ISelection[] = [];

      for (let index = 0; index < this.betMultiple.selections.length; index++) {
        const element = this.betMultiple.selections[index];
        if (element.odd.id != oddId) newMultiple.push(element);
        this.removeStorageItem("betMultiple");
      }

      this.betMultiple.selections = newMultiple;
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    }
  };

  @action addBetSimple = (
    amount: number,
    userId: string,
    betType: IBetType,
    oddValue: number,
    odd: IOdd,
    game: CollectiveGame
  ) => {
    try {
      if (amount > 0.01) {
        let selection: ISelection = {
          betType: betType,
          oddValue: oddValue,
          odd: odd,
          game: game,
        };

        let betSimple: ISimpleDetails = {
          selection: selection,
          amount: amount,
          userId: userId,
        };

        let exists = 0;

        for (let index = 0; index < this.simpleBets.length; index++) {
          const element = this.simpleBets[index];
          if (element.selection.odd.id == selection.odd.id) {
            exists = 1;
          }
        }

        if (!exists) {
          let stored = this.getLocalStorageItem("betSimple");
          stored[betType.id] = betSimple;
          this.setLocalStorageItem("betSimple", stored);
          this.simpleBets.push(betSimple);
        } else {
          toast.info("A seleção já foi inserida!");
        }
      } else toast.error("Montante deve ser superior a 10 cêntimos!");
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    }
  };

  @action addMultipleSelection = (
    betType: IBetType,
    oddValue: number,
    odd: IOdd,
    game: CollectiveGame
  ) => {
    try {
      let selection: ISelection = {
        betType: betType,
        oddValue: oddValue,
        odd: odd,
        game: game,
      };

      let exists = this.betMultiple.selections.filter(
        (x) => x.odd.id == odd.id
      );
      if (exists.length === 0) {
        let stored = this.getLocalStorageItem("betMultiple");
        stored[betType.id] = selection;
        this.setLocalStorageItem("betMultiple", stored);
        this.betMultiple.selections.push(selection);
      } else {
        toast.info("A seleção já foi inserida!");
      }
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    }
  };

  @action getNumSimple = () => {
    return this.simpleBets.length;
  };

  @action getNumMulti = () => {
    return this.betMultiple.selections.length;
  };

  @action getGanhosSimple = () => {
    let res = 0;
    this.simpleBets.map((x) => (res += x.amount * x.selection.oddValue));
    return res;
  };

  @action getGanhosMultiple = () => {
    let oddMultiple = 1;
    this.betMultiple.selections.map((x) => (oddMultiple *= x.oddValue));

    return oddMultiple * this.betMultiple.amount;
  };

  @computed get getSimpleAmount() {
    return this.simpleBets.map((x) => x.amount * 1).reduce((p, c) => p + c, 0);
  }

  @computed get getOddMultiple() {
    return this.betMultiple.selections
      .map((x) => x.oddValue.valueOf())
      .reduce((p, c) => p * c, 1);
  }

  @action createBetSimple = async () => {
    this.loading = true;
    try {
      if (this.getSimpleAmount <= this.rootStore.walletStore.wallet!.balance) {
        for (let index = 0; index < this.simpleBets.length; index++) {
          const element = this.simpleBets[index];

          let selection: ICreateSelection = {
            bettypeId: element.selection.betType.id,
            oddValue: element.selection.oddValue,
            oddId: element.selection.odd.id,
            gameId: element.selection.game.id,
          };

          let createBet: ICreateBetSimple = {
            selection: selection,
            amount: element.amount,
            userId: element.userId,
          };

          await Agent.Bet.createBetSimple(createBet);
        }

        runInAction(() => {
          this.clearSimple();
        });
      } else {
        history.push(`/user/profile`);
        toast.info("Não possui o valor necessário na carteira!");
      }
    } catch (error) {
      toast.error("Erro ao enviar a aposta. Tente mais tarde!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action createBetMultiple = async () => {
    this.loading = true;
    try {
      if (
        (this.betMultiple.amount = this.rootStore.walletStore.wallet!.balance)
      ) {
        if (this.betMultiple.selections.length > 1) {
          let createBet: ICreateBetMultiple = {
            selections: [],
            amount: this.betMultiple.amount,
            userId: this.betMultiple.userId,
          };

          for (
            let index = 0;
            index < this.betMultiple.selections.length;
            index++
          ) {
            const element = this.betMultiple.selections[index];

            let selection: ICreateSelection = {
              bettypeId: element.betType.id,
              oddValue: element.oddValue,
              oddId: element.odd.id,
              gameId: element.game.id,
            };

            createBet.selections.push(selection);
          }

          await Agent.Bet.createBetMultiple(createBet);

          runInAction(() => {
            this.clearMultiple();
          });
          toast.info("Aposta criadas com sucesso!");
        } else toast.error("Tem de ter, no mínimo, 2 seleções!");
      } else {
        history.push(`/user/profile`);
        toast.info("Não possui o valor necessário na carteira!");
      }
    } catch (error) {
      toast.error("Erro ao enviar a aposta. Tente mais tarde!");
      throw error;
    } finally {
      this.loading = false;
    }
  };
}
