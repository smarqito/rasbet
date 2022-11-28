import axios, { AxiosResponse } from "axios";
import { toast } from "react-toastify";
import {
  IBet,
  IBetMultiple,
  IBetSimple,
  ICreateBetMultiple,
  ICreateBetSimple,
} from "../models/bet";
import { IActiveGame, IGameInfo } from "../models/game";
import { IChangeOdd, IOdd } from "../models/odd";
import { ICreateTransaction, ITransaction } from "../models/transaction";
import {
  IAppUser,
  IAppUserRegister,
  IUser,
  IUserLogin,
  IUserRegister,
} from "../models/user";
import { IWallet } from "../models/wallet";

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

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
  registerAppUser: (user: IAppUserRegister) => requests.post(`/user`, user),
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
    email: string,
    name: string,
    lang: string,
    coin: string,
    notif: boolean
  ) => requests.put(`/user/update/user`, { email, name, lang, coin, notif }),
  updateSpecialist: (email: string, pass: string, lang: string) =>
    requests.put(`/user/update/specialist`, { email, pass, lang }),
  updateAdmin: (email: string, pass: string, lang: string) =>
    requests.put(`/user/update/admin`, { email, pass, lang }),
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
  updateAppUserSensitiveConfirm: (code: string) =>
    requests.put(`/user/sensitive/confirm`, { code }),
  updateAdminSensitiveConfirm: (code: string) =>
    requests.put(`/user/sensitive/admin/confirm`, { code }),
  updateSpecialistSensitiveConfirm: (code: string) =>
    requests.put(`/user/sensitive/specialist/confirm`, { code }),
  // Necessário logout?
  logout: (id: string) => requests.post(`/user/logout`, id),
};

const Bet = {
  createBetSimple: (createBet: ICreateBetSimple): Promise<IBetSimple> =>
    requests.post(`/bet/simple`, createBet),
  createBetMultiple: (createBet: ICreateBetMultiple): Promise<IBetMultiple> =>
    requests.post(`/bet/multiple`, createBet),
  getUserBetsOpen: (userId: number): Promise<IBet[]> =>
    requests.get(`/bet/open`),
  getUserBetsWon: (userId: number): Promise<IBet[]> => requests.get(`/bet/won`),
  getUserBetsLost: (userId: number): Promise<IBet[]> =>
    requests.get(`/bet/lost`),
};

const Game = {
  suspendGame: (gameId: number, specialistId: string) =>
    requests.patch(`/gameOdd/suspend`, { gameId, specialistId }),
  finishGame: (gameId: number, result: string, specialistId: string) =>
    requests.patch(`/gameOdd/finish`, { gameId, result, specialistId }),
  activateGame: (gameId: number, specialistId: string) =>
    requests.patch(`/gameOdd/activate`, { gameId, specialistId }),
  getActiveGames: (): Promise<IActiveGame[]> =>
    requests.get(`/gameOdd/activeGames`),
  getOdd: (oddId: number, betTypeId: number): Promise<IOdd> =>
    requests.get(`/gameOdd/odd/?oddId=${oddId}&betTypeId=${betTypeId}`),
  changeOdds: (change: IChangeOdd) => requests.patch(`/gameOdd/odds`, change),
  getGameInfo: (gameId: number, detailed: boolean): Promise<IGameInfo> =>
    requests.get(`/gameOdd/GameInfo?gameId=${gameId}&detailed=${detailed}`),
};

const Wallet = {
  get: (userId: number): Promise<IWallet> =>
    requests.get(`/user/?userId=${userId}`),
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
