import axios, { AxiosResponse } from "axios";
import { toast } from "react-toastify";
import {
  IAppUser,
  IAppUserRegister,
  IUser,
  IUserLogin,
  IUserRegister,
} from "../models/user";

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

const responseBody = (response: AxiosResponse) => response.data;

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
  getAppUser: (id: string): Promise<IAppUser> => requests.get(`/user/appuser?id=${id}$`),
  getSpecialist: (id: string): Promise<IUser> => requests.get(`/user/specialist?id=${id}$`),
  getAdmin: (id: string): Promise<IUser> => requests.get(`/user/admin?id=${id}$`),
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
  updateAppUserSensitiveConfirm: (email: string, pass: string) =>
    requests.put(`/user/sensitive/confirm`, { email, pass }),
  updateAdminSensitiveConfirm: (email: string, pass: string) =>
    requests.put(`/user/sensitive/admin/confirm`, { email, pass }),
  updateSpecialistSensitiveConfirm: (email: string, pass: string) =>
    requests.put(`/user/sensitive/specialist/confirm`, { email, pass }),
  logout: (id: string) => requests.post(`/user/logout`, id),
};

const Bet = {};

const BetType = {};

const Game = {};

const Odd = {};

const Wallet = {};

const Agent = {
  User,
  Bet,
  BetType,
  Game,
  Odd,
  Wallet,
};

export default Agent;
