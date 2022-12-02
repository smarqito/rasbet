import { action, makeObservable, observable, reaction } from "mobx";
import { RootStore } from "./rootStore";

export default class CommonStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    makeObservable(this);
    this.rootStore = rootStore;

    reaction(
      () => this.token,
      (token) => {
        if (token) {
          window.localStorage.setItem("jwt", token);
        } else {
          window.localStorage.removeItem("jwt");
        }
      }
    );
    reaction(
      () => this.role,
      (role) => {
        if (role) {
          window.localStorage.setItem("role", role);
        } else {
          window.localStorage.removeItem("role");
        }
      }
    );
  }

  @observable token: string | null = window.localStorage.getItem("jwt");
  @observable role: string | null = window.localStorage.getItem("role");
  @observable appLoaded = false;

  @action setToken = (token: string | null) => {
    this.token = token;
  };

  @action setRole = (role: string | null) => {
    this.role = role;
  };

  @action setAppLoaded = () => {
    this.appLoaded = true;
  };
}
