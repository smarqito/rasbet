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
import { IChangeOdd } from "../models/odd";
import { RootStore } from "./rootStore";

export default class GameStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    makeObservable(this);
    this.rootStore = rootStore;
  }

  @observable game: CollectiveGame | null = null;
  @observable activeGames: IActiveGame[] = [];
  @observable gamesFiltered: IActiveGame[] = [];
  @observable allGames: IActiveGame[] = [];
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

  @action loadGame = async (id: number) => {
    this.loading = true;
    let game = this.getGame(id);
    try {
      let gameInfo = await Agent.Game.getGame(id);
      runInAction(() => {
        this.game = gameInfo;
      });
    } catch (error) {
      toast.error("Não é possível apresentar o jogo!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getActiveGames = async () => {
    this.loading = true;

    try {
      var games = await Agent.Game.getActiveGames();

      runInAction(() => {
        if (games) {
          this.activeGames = games;
        } else toast.error("Sem jogos ativos!");
      });
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getActiveAndSuspended = async () => {
    this.loading = true;
    try {
      var games = await Agent.Game.getActiveAndSuspended();

      runInAction(() => {
        if (games) {
          this.allGames = games;
        } else toast.error("Sem jogos disponíveis!");
      });
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
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
      var sports = await Agent.Game.getAllSports();

      runInAction(() => {
        if (sports) {
          this.allSports = sports;
        } else toast.error("Sem desportos disponíveis!");
      });
    } catch (error) {
      toast.error("Sem desportos disponíveis!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action suspendGame = async (gameId: number, specialistId: string) => {
    this.loading = true;
    try {
      await Agent.Game.suspendGame(gameId, specialistId);
      toast.info("Jogo suspendido!");
    } catch (error) {
      toast.error("Erro ao suspender o jogo!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action finishGame = async (
    gameId: number,
    result: string,
    specialistId: string
  ) => {
    this.loading = true;
    try {
      await Agent.Game.finishGame(gameId, result, specialistId);
      toast.info("Jogo terminado!");
    } catch (error) {
      toast.error("Erro ao terminar o jogo!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action activateGame = async (gameId: number, specialistId: string) => {
    this.loading = true;
    try {
      await Agent.Game.activateGame(gameId, specialistId);
      toast.info("Jogo ativado");
    } catch (error) {
      toast.error("Erro ao ativar jogo!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action changeOdds = async (change: IChangeOdd) => {
    this.loading = true;
    try {
      await Agent.Game.changeOdds(change);
      toast.info("Odd alterada!");
    } catch (error) {
      toast.error("Erro ao alterar a odd!");
      throw error;
    } finally {
      this.loading = false;
    }
  };
}
