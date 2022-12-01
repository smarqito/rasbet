import { action, makeObservable, observable, runInAction } from "mobx";
import { toast } from "react-toastify";
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

  @action removeSimpleSelection = (oddId: number) => {
    try {
      var newSimples: ISimpleDetails[] = [];

      for (let index = 0; index < this.simpleBets.length; index++) {
        const element = this.simpleBets[index];
        if (element.selection.odd.id != oddId) newSimples.push(element);
      }

      this.simpleBets = newSimples;
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    }
  };

  @action removeMultipleSelection = (oddId: number) => {
    try {
      var newMultiple: ISelection[] = [];

      for (let index = 0; index < this.betMultiple.selections.length; index++) {
        const element = this.betMultiple.selections[index];
        if (element.odd.id != oddId) newMultiple.push(element);
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
      let amountValue = Object.values(amount);
      let amountValue2: number = Number(amountValue);
      console.log(amountValue2);
      if (amountValue2 > 0.01) {
        var selection: ISelection = {
          betType: betType,
          oddValue: oddValue,
          odd: odd,
          game: game,
        };

        var betSimple: ISimpleDetails = {
          selection: selection,
          amount: amountValue2,
          userId: userId,
        };

        var exists = 0;

        for (let index = 0; index < this.simpleBets.length; index++) {
          const element = this.simpleBets[index];
          if (element.selection.odd.id == selection.odd.id) {
            exists = 1;
          }
        }
        console.log(betSimple);

        if (!exists) {
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
      var selection: ISelection = {
        betType: betType,
        oddValue: oddValue,
        odd: odd,
        game: game,
      };

      var exists = 0;

      for (let index = 0; index < this.betMultiple.selections.length; index++) {
        const element = this.betMultiple.selections[index];

        if (element.odd.id == odd.id) exists = 1;
      }

      if (!exists) {
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
    var res = 0;
    this.simpleBets.map((x) => (res += x.amount * x.selection.oddValue));
    return res;
  };

  @action getGanhosMultiple = () => {
    var oddMultiple = 1;
    this.betMultiple.selections.map((x) => (oddMultiple *= x.oddValue));

    return oddMultiple * this.betMultiple.amount;
  };

  @action getSimpleAmount = () => {
    var res = 0;
    this.simpleBets.map((x) => (res += x.amount));
    return res;
  };

  @action getOddMultiple = () => {
    var res = 1;

    this.betMultiple.selections.map((x) => (res *= x.oddValue));

    return res;
  };

  @action createBetSimple = async () => {
    this.loading = true;
    try {
      for (let index = 0; index < this.simpleBets.length; index++) {
        const element = this.simpleBets[index];

        var selection: ICreateSelection = {
          bettypeId: element.selection.betType.id,
          oddValue: element.selection.oddValue,
          oddId: element.selection.odd.id,
          gameId: element.selection.game.id,
        };

        var createBet: ICreateBetSimple = {
          selection: selection,
          amount: element.amount,
          userId: element.userId,
        };

        await Agent.Bet.createBetSimple(createBet);
      }

      runInAction(() => {
        this.clearSimple();
      });
    } catch (error) {
      this.loading = false;
      toast.error("Erro ao enviar a aposta. Tente mais tarde!");
      throw error;
    } finally {
      this.loading = false;
      toast.info("Aposta(s) simple(s) criada(s) com sucesso!");
    }
  };

  @action createBetMultiple = async () => {
    this.loading = true;
    try {
      if (this.betMultiple.selections.length < 2) {
        var createBet: ICreateBetMultiple = {
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

          var selection: ICreateSelection = {
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
      } else toast.error("Tem de ter, no mínimo, 2 seleções!");
    } catch (error) {
      toast.error("Erro ao enviar a aposta. Tente mais tarde!");
      throw error;
    } finally {
      toast.info("Aposta criadas com sucesso!");
      this.loading = false;
    }
  };
}
