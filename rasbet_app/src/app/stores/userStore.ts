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
  getIn,
  IAppUserRegister,
  IUser,
  IUserLogin,
  IUserRegister,
  Role,
} from "../models/user";
import { RootStore } from "./rootStore";

export default class UserStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    makeObservable(this);
    this.rootStore = rootStore;
  }

  @observable user: IUser | null = null;
  @observable role: string | null = null;
  @observable loading = false;

  @action hasRole = (role: string) => {
    return this.user!.role == role;
  };

  @computed get isLoggedIn() {
    return !!this.user;
  }

  @action login = async (values: IUserLogin) => {
    try {
      const user = await Agent.User.login(values);
      runInAction(() => {
        this.user = user;
      });
      history.push(getIn[user.role]);
    } catch (error) {
      toast.error("Utilizador ou Password errados");
      throw error;
    }
  };

  @action registerAppUser = async (values: IAppUserRegister) => {
    try {
      var a = await Agent.User.registerAppUser(values);
      history.push("/");
      toast.info("Utilizador criado com sucesso!");
    } catch (error) {
      throw error;
    }
  };

  @action createAdmin = async (values: IUserRegister) => {
    try {
      var a = await Agent.User.registerAdmin(values);
      toast.info("Administrador criado com sucesso!");
    } catch (error) {
      throw error;
    }
  };

  @action createSpecialist = async (values: IUserRegister) => {
    try {
      var a = await Agent.User.registerSpecialist(values);
      toast.info("Especialista criado com sucesso!");
    } catch (error) {
      throw error;
    }
  };
}
