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
import { IBet } from "../models/bet";
import {
  getIn,
  IAppUser,
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
  @observable appUserDetails: IAppUser | null = null;
  @observable userBetsFiltered: IBet[] = [];
  @observable loading = false;

  @action clearBets = () => {
    this.userBetsFiltered = [];
  };

  @action clearUser = () => {
    this.user = null;
  };

  @action clearAppUserDetails = () => {
    this.appUserDetails = null;
  };

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

  @action loginAdmin = async (values: IUserLogin) => {
    this.loading = true;
    try {
      const user = await Agent.User.login(values);
      runInAction(() => {
        if (user) {
          if (user.role == "Admin") {
            this.user = user;
            this.role = user.role;
            this.rootStore.commonStore.setToken(user.token);
            history.push(getIn[user.role] + `/${user.id}`);
          } else toast.error("Utilizador não válido!");
        } else toast.error("Utilizador ou Password errados!");
      });
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action loginAppUser = async (values: IUserLogin) => {
    this.loading = true;
    try {
      const user = await Agent.User.login(values);
      this.rootStore.betStore.loadCart();
      runInAction(() => {
        if (user) {
          if (user.role == "AppUser") {
            this.user = user;
            this.role = user.role;
            this.rootStore.commonStore.setToken(user.token);
            history.push(getIn[user.role] + `/${user.id}`);
          } else toast.error("Utilizador não válido!");
        } else toast.error("Utilizador ou Password errados!");
      });
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action loginSpecialist = async (values: IUserLogin) => {
    this.loading = true;
    try {
      const user = await Agent.User.login(values);
      runInAction(() => {
        if (user) {
          if (user.role == "Specialist") {
            this.user = user;
            this.role = user.role;
            this.rootStore.commonStore.setToken(user.token);
            history.push(getIn[user.role] + `/${user.id}`);
          } else toast.error("Utilizador não válido!");
        } else toast.error("Utilizador ou Password errados!");
      });
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action registerAppUser = async (values: IAppUserRegister) => {
    this.loading = true;
    console.log(values);
    try {
      if (values.password !== values.passwordRepeated) {
        toast.error("Palavras-pass não coincidem!");
        throw new Error();
      }

      await Agent.User.registerAppUser(values);
      toast.info("Utilizador criado com sucesso!");
      history.push("/");
    } catch (error) {
      toast.error("Erro ao registar utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action createAdmin = async (values: IUserRegister) => {
    this.loading = true;
    try {
      await Agent.User.registerAdmin(values);
      toast.info("Administrador criado com sucesso!");
    } catch (error) {
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action createSpecialist = async (values: IUserRegister) => {
    this.loading = true;
    try {
      await Agent.User.registerSpecialist(values);
      toast.info("Especialista criado com sucesso!");
    } catch (error) {
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action logout = async () => {
    try {
      await Agent.User.logout(this.user!.id);
      this.rootStore.commonStore.setToken(null);
      this.clearUser();
      history.push("/");
      window.localStorage.clear();
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action changePassByEmail = async (email: string) => {
    this.loading = true;
    try {
      await Agent.User.changePass(email);
      toast.info("Email enviado com nova Palavra-Passe!", {
        onClose: history.goBack,
        autoClose: 1000,
      });
    } catch (error) {
      toast.info("Email inválido!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getAppUser = async (id: string) => {
    this.loading = true;
    try {
      let newUser: IAppUser;
      newUser = await Agent.User.getAppUser(id);

      runInAction(() => {
        if (newUser) this.appUserDetails = newUser;
        else toast.error("Falha no load do utilizador!");
      });
    } catch (error) {
      toast.error("Falha ao ir buscar o utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getAdmin = async (id: string) => {
    this.loading = true;
    try {
      let newUser: IUser;
      newUser = await Agent.User.getAdmin(id);

      runInAction(() => {
        if (newUser) this.user = newUser;
        else toast.error("Falha no load do utilizador!");
      });
    } catch (error) {
      toast.error("Falha ao ir buscar o utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getSpecialist = async (id: string) => {
    this.loading = true;
    try {
      let newUser: IUser;
      newUser = await Agent.User.getSpecialist(id);

      runInAction(() => {
        if (newUser) {
          this.user = newUser;
          console.log(newUser);
        } else toast.error("Falha no load do utilizador!");
      });
    } catch (error) {
      toast.error("Falha ao ir buscar o utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateAppUser = async (
    email: string,
    name: string,
    lang: string,
    coin: string,
    notif: boolean
  ) => {
    this.loading = true;
    try {
      await Agent.User.updateAppUser(name, email, lang, coin, notif);
      toast.info("Alteração efetuado com sucesso!");
    } catch (error) {
      toast.error("Falha ao atualizar o utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateAdmin = async (email: string, name: string, lang: string) => {
    this.loading = true;
    try {
      await Agent.User.updateAdmin(email, name, lang);
      toast.info("Alteração efetuado com sucesso!");
    } catch (error) {
      toast.error("Falha ao atualizar o utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateSpecialist = async (
    email: string,
    pass: string,
    lang: string
  ) => {
    this.loading = true;
    try {
      await Agent.User.updateSpecialist(email, pass, lang);
      toast.info("Alteração efetuado com sucesso!");
    } catch (error) {
      toast.error("Falha ao atualizar o utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateAppUserSensitive = async (
    email: string,
    pass: string,
    iban: string,
    phone: string
  ) => {
    this.loading = true;
    try {
      await Agent.User.updateAppUserSensitive(email, iban, phone, pass);
    } catch (error) {
      toast.error("Falha ao atualizar os dados sensíveis do utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateAdminSensitive = async (email: string, pass: string) => {
    this.loading = true;
    try {
      await Agent.User.updateAdminSensitive(email, pass);
    } catch (error) {
      toast.error("Falha ao atualizar os dados sensíveis do utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateSpecialistSensitive = async (email: string, pass: string) => {
    this.loading = true;
    try {
      await Agent.User.updateSpecialSensitive(email, pass);
    } catch (error) {
      toast.error("Falha ao atualizar os dados sensíveis do utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateAppUserSensitiveConfirm = async (
    email: string,
    code: string
  ) => {
    this.loading = true;
    try {
      console.log(email, code);
      await Agent.User.updateAppUserSensitiveConfirm(email, code);
      toast.info("Alteração efetuado com sucesso!");
    } catch (error) {
      toast.error("Falha ao atualizar os dados sensíveis do utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateAdminSensitiveConfirm = async (email: string, code: string) => {
    this.loading = true;
    try {
      await Agent.User.updateAdminSensitiveConfirm(email, code);
      toast.info("Alteração efetuado com sucesso!");
    } catch (error) {
      toast.error("Falha ao atualizar os dados sensíveis do utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateSpecialistSensitiveConfirm = async (
    email: string,
    code: string
  ) => {
    this.loading = true;
    try {
      await Agent.User.updateSpecialistSensitiveConfirm(email, code);
      toast.info("Alteração efetuado com sucesso!");
    } catch (error) {
      toast.error("Falha ao atualizar os dados sensíveis do utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getUserBetsOpen = async (id: string, start: Date, end: Date) => {
    this.loading = true;
    try {
      let bets: IBet[] = await Agent.Bet.getUserBetsOpen(id, start, end);

      runInAction(() => {
        if (bets) {
          this.userBetsFiltered = bets;
        } else toast.info("Sem apostas realizadas!");
      });
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getUserBetsWon = async (id: string, start: Date, end: Date) => {
    this.loading = true;
    try {
      let bets: IBet[] = await Agent.Bet.getUserBetsWon(id, start, end);

      runInAction(() => {
        if (bets) {
          this.userBetsFiltered = bets;
        } else toast.info("Sem apostas realizadas!");
      });
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getUserBetsClosed = async (id: string, start: Date, end: Date) => {
    this.loading = true;
    try {
      let bets: IBet[] = await Agent.Bet.getUserBetsClose(id, start, end);

      runInAction(() => {
        if (bets) {
          this.userBetsFiltered = bets;
        } else toast.info("Sem apostas realizadas!");
      });
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };
}
