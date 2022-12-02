import axios, { AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { IBet, ICreateBetMultiple, ICreateBetSimple } from "../models/bet";
import { CollectiveGame, IActiveGame, ISport } from "../models/game";
import { IChangeOdd } from "../models/odd";
import { ICreateTransaction, ITransaction } from "../models/transaction";
import {
  IAppUser,
  IAppUserRegister,
  IUser,
  IUserLogin,
  IUserRegister,
} from "../models/user";
import { IWallet } from "../models/wallet";

axios.defaults.baseURL = "http://localhost:8000";

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.request.use(
  (config) => {
    const token = window.localStorage.getItem("jwt");
    if (token) config.headers!.Authorization = `Bearer ${token}`;
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

axios.interceptors.response.use(undefined, (error) => {
  const { status, data, config } = error.response;
  toast.error(Object.values<string>(data.errors)[0]);
});

const requests = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  del: (url: string) => axios.delete(url).then(responseBody),
  patch: (url: string, body: {}) => axios.patch(url, body).then(responseBody),
};

const User = {
  login: (user: IUserLogin): Promise<IUser> =>
    requests.post(`/user/login`, user),
  registerAppUser: (user: IAppUserRegister) => requests.post(`/User/user`, user),
  registerAdmin: (user: IUserRegister) => requests.post(`/user/admin`, user),
  registerSpecialist: (user: IUserRegister) =>
    requests.post(`/user/specialist`, user),
  getAppUser: (id: string): Promise<IAppUser> =>
    requests.get(`/user/appuser?id=${id}`),
  getSpecialist: (id: string): Promise<IUser> =>
    requests.get(`/user/specialist?id=${id}`),
  getAdmin: (id: string): Promise<IUser> =>
    requests.get(`/user/admin?id=${id}`),
  updateAppUser: (
    name: string,
    email: string,
    lang: string,
    coin: string,
    notif: boolean
  ) => requests.put(`/user/update/user`, { email, name, lang, coin, notif }),
  updateSpecialist: (email: string, name: string, lang: string) =>
    requests.put(`/user/update/specialist`, { email, name, lang }),
  updateAdmin: (email: string, name: string, lang: string) =>
    requests.put(`/user/update/admin`, { email, name, lang }),
  updateAppUserSensitive: (
    email: string,
    pass: string,
    iban: string,
    phone: string
  ) => requests.put(`/user/sensitive/user`, { email, pass, iban, phone }),
  updateAdminSensitive: (email: string, pass: string) =>
    requests.put(`/user/sensitive/admin`, { email, pass }),
  updateSpecialSensitive: (email: string, pass: string) =>
    requests.put(`/user/sensitive/specialist`, { email, pass }),
  updateAppUserSensitiveConfirm: (email: string, code: string) =>
    requests.put(`/user/sensitive/confirm`, { email, code }),
  updateAdminSensitiveConfirm: (email: string, code: string) =>
    requests.put(`/user/sensitive/admin/confirm`, { email, code }),
  updateSpecialistSensitiveConfirm: (email: string, code: string) =>
    requests.put(`/user/sensitive/specialist/confirm`, { email, code }),
  changePass: (email: string) => requests.put(`/user/forgotPWD`, { email }),
  logout: (id: string) => requests.post(`/user/logout`, id),
};

const Bet = {
  createBetSimple: (createBet: ICreateBetSimple): Promise<IBet> =>
    requests.post(`/Bet/simple`, createBet),
  createBetMultiple: (createBet: ICreateBetMultiple): Promise<IBet> =>
    requests.post(`/Bet/multiple`, createBet),
  getUserBetsOpen: (userId: string, start: Date, end: Date): Promise<IBet[]> =>
    requests.get(`/Bet/open?userId=${userId}&start=${end}`),
  getUserBetsWon: (userId: string, start: Date, end: Date): Promise<IBet[]> =>
    requests.get(`/Bet/won?userId=${userId}&start=${end}`),
  getUserBetsLost: (userId: string, start: Date, end: Date): Promise<IBet[]> =>
    requests.get(`/Bet/lost?userId=${userId}&start=${end}`),
  getUserBetsClose: (userId: string, start: Date, end: Date): Promise<IBet[]> =>
    requests.get(`/Bet/closed?userId=${userId}&start=${end}`),
};

const Game = {
  suspendGame: (gameId: number, specialistId: string) =>
    requests.patch(`/GameOdd/suspend`, { gameId, specialistId }),
  finishGame: (gameId: number, result: string, specialistId: string) =>
    requests.patch(`/GameOdd/finish`, { gameId, result, specialistId }),
  activateGame: (gameId: number, specialistId: string) =>
    requests.patch(`/GameOdd/activate`, { gameId, specialistId }),
  changeOdds: (change: IChangeOdd) => requests.patch(`/GameOdd/odds`, change),
  getActiveGames: (): Promise<IActiveGame[]> =>
    requests.get(`/GameOdd/activeGames`),
  getAllSports: (): Promise<ISport[]> => requests.get(`/GameOdd/sports`),
  getActiveAndSuspended: (): Promise<IActiveGame[]> =>
    requests.get(`/GameOdd/ActiveAndSuspended`),
  getGame: (gameId: number): Promise<CollectiveGame> =>
    requests.get(`/GameOdd?gameId=${gameId}`),
};

const Wallet = {
  get: (userId: string): Promise<IWallet> =>
    requests.get(`/Wallet?userId=${userId}`),
  depositFunds: (createTran: ICreateTransaction) =>
    requests.put(`/wallet/deposit`, createTran),
  withdrawFunds: (createTran: ICreateTransaction) =>
    requests.put(`/wallet/withdraw`, createTran),
  getTransactions: (
    userId: string,
    start: Date,
    end: Date
  ): Promise<ITransaction[]> =>
    requests.get(
      `/wallet/transactions?userId=${userId}&start=${start}&end=${end}`
    ),
};

const Agent = {
  User,
  Bet,
  Game,
  Wallet,
};

export default Agent;
