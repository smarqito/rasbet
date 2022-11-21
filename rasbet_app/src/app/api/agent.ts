import axios, { AxiosResponse } from "axios";
import { url } from "inspector";

axios.defaults.baseURL = process.env.REACT_APP_API_URL;

const responseBody = (response: AxiosResponse) => response.data;

const requests = {
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  del: (url: string) => axios.delete(url).then(responseBody),
  patch: (url: string, body: {}) => axios.patch(url, body).then(responseBody),
};

const User = {};

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
