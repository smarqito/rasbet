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
          window.localStorage.setItem("role", user.role.toString());
        } else {
          window.localStorage.removeItem("role");
        }
      }
    );
  }

  @observable user: IUser | null = null;
  @observable role: string | null = window.localStorage.getItem("role");
  @observable loading = false;
  @observable submitting = false;

  @action hasRole = (roles: string[]) => {
    let isAuthorized = false;
    if (this.user) {
      roles.forEach(
        (role) => (isAuthorized = isAuthorized || this.role == role)
      );
    }
    return isAuthorized;
  };

  @computed get isLoggedIn() {
    return !!this.user;
  }

  @action login = async (values: IUserLogin) => {
    this.loading = true;
    try {
      // const user = await Agent.User.login(values);
      // runInAction(() => {
      //   this.user = user;
      //   this.role = user.role;
      // });

      const user: IUser = {
        id: "0",
        name: "Jose Alberto",
        email: "jafmalheiro@live.com.pt",
        language: "Português",
        token: "tokenJose",
        role: "AppUser",
      };

      if (values.email == user.email && values.password == "123456") {
        this.user = user;
        this.role = user.role;
        this.rootStore.commonStore.setToken(user.token);
        history.push(getIn[user.role] + `/${user.name}`);
        this.loading = false;
      } else {
        toast.error("Utilizador ou Password errados!");
        this.loading = false;
      }
    } catch (error) {
      this.loading = false;
      toast.error("Utilizador ou Password errados!");
      throw error;
    }
  };

  @action registerAppUser = async (values: IAppUserRegister) => {
    this.loading = true;
    try {
      if (values.password !== values.repetePass) {
        toast.error("Palavras-pass não coincidem!");
        throw new Error();
      }

      var a = await Agent.User.registerAppUser(values);
      toast.info("Utilizador criado com sucesso!");
      history.push("/");
      this.loading = false;
    } catch (error) {
      this.loading = false;
      toast.error("Erro ao registar utilizador!");
      throw error;
    }
  };

  @action createAdmin = async (values: IUserRegister) => {
    this.submitting = true;
    try {
      var a = await Agent.User.registerAdmin(values);
      this.submitting = false;
      toast.info("Administrador criado com sucesso!");
    } catch (error) {
      throw error;
    }
  };

  @action createSpecialist = async (values: IUserRegister) => {
    this.submitting = true;
    try {
      var a = await Agent.User.registerSpecialist(values);
      this.submitting = false;
      toast.info("Especialista criado com sucesso!");
    } catch (error) {
      throw error;
    }
  };

  @action logout = async () => {
    try {
      // await Agent.User.logout(this.user!.id);
      this.rootStore.commonStore.setToken(null);
      this.user = null;
      history.push("/");
    } catch (error) {}
  };

  @action changePassByEmail = async (email: string) => {
    try {
      var password = "";
      //password = Agent.User.changePassByEmail(email);
      toast.info("Email enviado com nova Palavra-Passe!", {
        onClose: history.goBack,
        autoClose: 1000,
      });
    } catch (error) {
      toast.info("Email inválido!");
      throw error;
    }
  };
}
