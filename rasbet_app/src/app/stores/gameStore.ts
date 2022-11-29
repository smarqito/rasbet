import {
  action,
  computed,
  makeObservable,
  observable,
  runInAction,
} from "mobx";
import { toast } from "react-toastify";
import Agent from "../api/agent";
import { IBetType } from "../models/betType";
import {
  CollectiveGame,
  IActiveGame,
  IGame,
  ISport,
  IStatistics,
} from "../models/game";
import { RootStore } from "./rootStore";

export default class GameStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    makeObservable(this);
    this.rootStore = rootStore;
  }

  @observable game: IGame | null = null;
  @observable activeGames: IActiveGame[] = [];
  @observable gamesFiltered: IActiveGame[] = [];
  @observable allSports: ISport[] = [];
  @observable loading = false;

  @action clearGame = () => {
    this.game = null;
  };

  @action clearActive = () => {
    this.activeGames = [];
  };

  @action clearFiltered = () => {
    this.gamesFiltered = [];
  };

  @action clearSports = () => {
    this.allSports = [];
  };

  getGame = (id: number) => {
    let game = null;
    for (let index = 0; index < this.activeGames.length; index++) {
      const element = this.activeGames[index];
      if (element.game.id == id) {
        game = element;
        break;
      }
    }

    if (!game) throw "Não existe o jogo na lista!";

    return game;
  };

  // @action loadGame = async (id: number) => {
  //   this.loading = true;
  //   let game = this.getGame(id);
  //   try {
  //     // let gameInfo = await Agent.Game.getGame(id, true);
  //     runInAction(() => {
  //         this.game = gameinfo;
  //       }
  //     });

  //     this.loading = false;
  //   } catch (error) {
  //     toast.error("Não é possível apresentar o jogo!");
  //     throw error;
  //   }
  // };

  @action getActiveGames = async () => {
    this.loading = true;

    try {
      // var games = await Agent.Game.getActiveGames();

      // runInAction(() => {
      //   this.activeGames = games;
      // });

      var betType1: IBetType = {
        id: 0,
        type: "UNFINISHED",
        odds: [
          { id: 1, name: "Sporting", value: 1.19 },
          { id: 2, name: "Empate", value: 3.19 },
          { id: 3, name: "Varzim", value: 7.19 },
        ],
      };

      var betType2: IBetType = {
        id: 1,
        type: "UNFINISHED",
        odds: [
          { id: 1, name: "Benfica", value: 1.19 },
          { id: 2, name: "Empate", value: 3.19 },
          { id: 3, name: "Porto", value: 7.19 },
        ],
      };

      var game1: CollectiveGame = {
        id: 0,
        start: new Date(),
        sport: "Futebol",
        mainBet: betType1,
        home: "Sporting",
        away: "Varzim",
      };

      var game2: CollectiveGame = {
        id: 1,
        start: new Date(),
        sport: "Futebol",
        mainBet: betType2,
        home: "Benfica",
        away: "Porto",
      };

      var statitics1: IStatistics = {
        betCount: 3,
        statitics: new Map<number, number>(),
      };

      var statitics2: IStatistics = {
        betCount: 3,
        statitics: new Map<number, number>(),
      };

      statitics1.statitics.set(1, 23);
      statitics1.statitics.set(2, 47);
      statitics1.statitics.set(3, 30);

      statitics2.statitics.set(1, 30);
      statitics2.statitics.set(2, 60);
      statitics2.statitics.set(3, 10);

      var a1: IActiveGame = {
        game: game1,
        statistic: statitics1,
      };

      var a2: IActiveGame = {
        game: game2,
        statistic: statitics2,
      };

      this.activeGames.push(a1);
      this.activeGames.push(a2);

      this.loading = false;
    } catch (error) {
      this.loading = false;
      toast.error("Sem jogos ativos!");
      throw error;
    }
  };

  @action getActiveGamesBySport = (sport: ISport) => {
    try {
      var gamesBySport = this.activeGames.filter(
        (x) => x.game.sport == sport.name
      );
      this.gamesFiltered = gamesBySport;

    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    }
  };

  @action getAllSports = async () => {
    this.loading = true;
    try {
      // var sports = await Agent.Game.getAllSports();

      // runInAction(() => {
      //   this.allSports = sports;
      // });

      var s1: ISport = { name: "Futebol" };
      var s2: ISport = { name: "teste" };

      this.allSports.push(s1);
      this.allSports.push(s2);

      this.loading = false;
    } catch (error) {
      toast.error("Sem desportos disponíveis!");
      throw error;
    }
  };
}
