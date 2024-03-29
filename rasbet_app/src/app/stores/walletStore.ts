import { action, computed, makeObservable, observable, runInAction } from "mobx";
import { toast } from "react-toastify";
import Agent from "../api/agent";
import { ICreateTransaction, ITransaction } from "../models/transaction";
import { IWallet } from "../models/wallet";
import { RootStore } from "./rootStore";

export default class WalletStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    makeObservable(this);
    this.rootStore = rootStore;
  }

  @observable wallet: IWallet | null = null;
  @observable allTransactions: ITransaction[] = [];
  @observable loading = false;

  @action clearWallet = () => {
    this.wallet = null;
  };

  @action clearTransaction = () => {
    this.allTransactions = [];
  };

  @computed get getBalance() {
    return this.wallet?.balance.toFixed(2);
  }

  @action getWallet = async (userId: string) => {
    this.loading = true;
    try {
      let newWallet = await Agent.Wallet.get(userId);

      runInAction(() => {
        this.clearWallet();
        if (newWallet) this.wallet = newWallet;
        else toast.error("Erro ao obter a carteira do utilizador!");
      });
    } catch (error) {
      toast.error("Erro ao obter a carteira do utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action depositFunds = async (tran: ICreateTransaction) => {
    this.loading = true;
    try {
      if (
        this.wallet!.userId == tran.userId
      )
        await Agent.Wallet.depositFunds(tran);
      else toast.error("Erro ao depositar!");
    } catch (error) {
      toast.error("Ocorreu um eror interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action withdrawFunds = async (tran: ICreateTransaction) => {
    this.loading = true;
    try {
      console.log(this.rootStore.userStore.appUserDetails?.iban);
      if (this.rootStore.userStore.appUserDetails?.iban !== undefined) {
        if (
          this.wallet!.userId == tran.userId &&
          this.wallet!.balance >= tran.value
        )
          await Agent.Wallet.withdrawFunds(tran);
        else toast.error("Erro ao levantar (sem crédito)!");
      } else toast.error("Erro ao levantar (sem IBAN)!");
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getTransactions = async (userId: string, start: Date, end: Date) => {
    this.loading = true;
    try {
      console.log(this.wallet!.userId);
      console.log(userId);
      console.log(start);
      console.log(end);
      if (this.wallet!.userId == userId) {
        let transactions = await Agent.Wallet.getTransactions(
          userId,
          start,
          end
        );

        runInAction(() => {
          if (transactions) {
            this.clearTransaction();
            this.allTransactions = transactions;
          } else toast.error("Não existe transações!");
        });
      } else toast.error("Ocorreu um erro interno!");
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };
}
