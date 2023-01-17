import {
  action,
  computed,
  makeObservable,
  observable,
  runInAction,
} from "mobx";
import { toast } from "react-toastify";
import Agent from "../api/agent";
import {
  CollectiveGame,
  IActiveGame,
  ISport,
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
  @observable allGames: CollectiveGame[] = [];
  @observable allSports: ISport[] = [];
  @observable loading = false;
  @observable selectedSport: ISport = { name: "Football" };
  @observable subbedGames: number[] = [];

  @action clearGame = () => {
    this.game = null;
  };

  @action setSelectedSport = (sport: ISport) => {
    this.selectedSport = sport;
  };
  @action clearActive = () => {
    this.activeGames = [];
  };

  @action clearFiltered = () => {
    this.gamesFiltered = [];
  };

  @action clearSubbed = () => {
    this.subbedGames = [];
  };

  @action clearAllGames = () => {
    this.allGames = [];
  };

  @action clearSports = () => {
    this.allSports = [];
  };

  @action isGameSubbed (gameId: number) {
    return this.subbedGames.includes(gameId);
  }

  getGame = (id: number) => {
    let game = null;
    for (let index = 0; index < this.activeGames.length; index++) {
      const element = this.activeGames[index];
      if (element.id === id) {
        game = element;
        break;
      }
    }

    if (!game) throw Error("Não existe o jogo na lista!");

    return game;
  };

  getAnyGame = (id: number) => {
    let game = null;
    for (let index = 0; index < this.allGames.length; index++) {
      const element = this.allGames[index];
      if (element.id === id) {
        game = element;
        break;
      }
    }

    if (!game) throw Error("Não existe o jogo na lista!");

    return game;
  };

  @action loadAnyGame = async (id: number) => {
    this.loading = true;
    let game = this.getAnyGame(id);
    try {
      runInAction(() => {
        this.game = game;
      });
    } catch (error) {
      toast.error("Não é possível apresentar o jogo!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action loadGame = async (id: number) => {
    this.loading = true;
    let game = this.getGame(id);
    try {
      runInAction(() => {
        this.game = game;
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
      let games = await Agent.Game.getActiveGames();
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
      let games = await Agent.Game.getActiveAndSuspended();

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

  @computed get getActiveGamesBySport() {
    try {
      // if(this.activeGames.length === 0) this.getActiveGames();
      let gamesBySport = this.activeGames.filter(
        (x) =>
          x.sportName.toLowerCase() === this.selectedSport.name.toLowerCase()
      );
      return gamesBySport;
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    }
  }

  @computed get getAllGamesBySport() {
    try {
      // if(this.activeGames.length === 0) this.getActiveGames();
      let gamesBySport = this.allGames.filter(
        (x) =>
          x.sportName.toLowerCase() === this.selectedSport.name.toLowerCase()
      );
      return gamesBySport;
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    }
  }

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
      console.log(JSON.stringify(change));
      await Agent.Game.changeOdds(change);
      toast.info("Odd alterada!");
    } catch (error) {
      toast.error("Erro ao alterar a odd!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action getSubbedGames = async (userId: string) => {
    this.loading = true;
    try {
      let games = await Agent.Game.getSubbedGames(userId);
      runInAction(() => {
        if (games) {
          this.subbedGames = games;
        } 
      });
    } catch (error) {
      toast.error("Ocorreu um erro interno!");
      throw error;
    } finally {
      this.loading = false;
    }
  };

  @action addFollowerToGame = async (gameId: number, userId: string) => {
    this.loading = true;
    try {
      await Agent.Game.addFollower(userId, gameId);
      toast.info("Seguindo jogo!");
    } catch (error) {
      toast.error("Erro ao seguir jogo!");
      throw error;
    } finally {
      this.loading = false;
    }
  }

  @action removeFollowerFromGame = async (gameId: number, userId: string) => {
    this.loading = true;
    try {
      await Agent.Game.removeFollower(userId, gameId);
      toast.info("Deixou de seguir jogo!");
    } catch (error) {
      toast.error("Erro ao deixar de seguir jogo!");
      throw error;
    } finally {
      this.loading = false;
    }
  }

}
