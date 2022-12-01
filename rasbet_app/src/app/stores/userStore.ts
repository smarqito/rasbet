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
      // const user = await Agent.User.login(values);
      // runInAction(() => {
      //   this.user = user;
      //   this.role = user.role;
      // });

      const user: IUser = {
        id: "1",
        name: "Marco Antonio",
        email: "smarqito@gmail.com",
        language: "Português",
        token: "tokenMarco",
        role: "Admin",
      };
      if (user.role == "Admin") {
        if (values.email == user.email && values.password == "123456") {
          this.user = user;
          this.role = user.role;
          this.rootStore.commonStore.setToken(user.token);
          history.push(getIn[user.role] + `/${user.name}`);
        } else {
          toast.error("Utilizador ou Password errados!");
        }
      } else toast.error("Utilizador não válido!");
    } catch (error) {
      this.loading = false;
      toast.error("Utilizador ou Password errados!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action loginAppUser = async (values: IUserLogin) => {
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

      if (user.role == "AppUser") {
        if (values.email == user.email && values.password == "123456") {
          this.user = user;
          this.role = user.role;
          this.rootStore.commonStore.setToken(user.token);
          history.push(getIn[user.role] + `/${user.name}`);
        } else {
          toast.error("Utilizador ou Password errados!");
        }
      } else toast.error("Utilizador não válido!");
    } catch (error) {
      this.loading = false;
      toast.error("Utilizador ou Password errados!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action loginSpecialist = async (values: IUserLogin) => {
    this.loading = true;
    try {
      // const user = await Agent.User.login(values);
      // runInAction(() => {
      //   this.user = user;
      //   this.role = user.role;
      // });

      const user: IUser = {
        id: "3",
        name: "Miguel",
        email: "miguel@gmail.com",
        language: "Português",
        token: "tokenMiguel",
        role: "Specialist",
      };

      if (user.role == "Specialist") {
        if (values.email == user.email && values.password == "123456") {
          this.user = user;
          this.role = user.role;
          this.rootStore.commonStore.setToken(user.token);
          history.push(getIn[user.role] + `/${user.name}`);
        } else {
          toast.error("Utilizador ou Password errados!");
        }
      } else toast.error("Utilizador não válido!");
    } catch (error) {
      this.loading = false;
      toast.error("Utilizador ou Password errados!");
      throw error;
    } finally {
      this.loading = false;
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
    } catch (error) {
      this.loading = false;
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
      // await Agent.User.logout(this.user!.id);
      this.rootStore.commonStore.setToken(null);
      this.user = null;
      history.push("/");
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
      var password = "";
      //password = Agent.User.changePassByEmail(email);
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
        if (newUser) this.user = newUser;
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
      await Agent.User.updateAppUserSensitive(email, pass, iban, phone);
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

  @action updateAppUserSensitiveConfirm = async (code: string) => {
    this.loading = true;
    try {
      await Agent.User.updateAppUserSensitiveConfirm(code);
      toast.info("Alteração efetuado com sucesso!");
    } catch (error) {
      toast.error("Falha ao atualizar os dados sensíveis do utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateAdminSensitiveConfirm = async (code: string) => {
    this.loading = true;
    try {
      await Agent.User.updateAdminSensitiveConfirm(code);
      toast.info("Alteração efetuado com sucesso!");
    } catch (error) {
      toast.error("Falha ao atualizar os dados sensíveis do utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action updateSpecialistSensitiveConfirm = async (code: string) => {
    this.loading = true;
    try {
      await Agent.User.updateSpecialistSensitiveConfirm(code);
      toast.info("Alteração efetuado com sucesso!");
    } catch (error) {
      toast.error("Falha ao atualizar os dados sensíveis do utilizador!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getUserBetsOpen = async (id: string) => {
    this.loading = true;
    try {
      var bets: IBet[] = await Agent.Bet.getUserBetsOpen(id);

      if (bets) {
        return bets;
      } else toast.error("Sem apostas realizadas!");
    } catch (error) {
      toast.error("Sem apostas realizadas!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getUserBetsWon = async (id: string) => {
    this.loading = true;
    try {
      var bets: IBet[] = await Agent.Bet.getUserBetsWon(id);

      if (bets) {
        return bets;
      } else toast.error("Sem apostas realizadas!");
    } catch (error) {
      toast.error("Sem apostas realizadas!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getUserBetsLost = async (id: string) => {
    this.loading = true;
    try {
      var bets: IBet[] = await Agent.Bet.getUserBetsLost(id);

      if (bets) {
        return bets;
      } else toast.error("Sem apostas realizadas!");
    } catch (error) {
      toast.error("Sem apostas realizadas!");
      throw error;
    } finally {
      this.loading = false;
    }
  };
}
