import {
  action,
  computed,
  makeObservable,
  observable,
  reaction,
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
    reaction(
      () => this.user,
      (user) => {
        if (user) {
          window.localStorage.setItem("roles", user.role);
        } else {
          window.localStorage.removeItem("roles");
        }
      }
    );
  }

  @observable user: IUser | null = null;
  @observable role: string | null = null;
  @observable loading = false;
  @observable submitting = false;

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
      if (values.password !== values.repetePass) {
        toast.error("Palavras-pass não coincidem!");
        throw new Error();
      }

      var a = await Agent.User.registerAppUser(values);
      toast.info("Utilizador criado com sucesso!");
      history.push("/");
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

  @action logout = async () => {
    try {
      await Agent.User.logout(this.user!.id);
      this.rootStore.commonStore.setToken(null);
      this.user = null;
      history.push("/");
    } catch (error) {}
  };

  @action getUser = async () => {
    try {
    } catch (error) {
      throw error;
    }
  };
}
