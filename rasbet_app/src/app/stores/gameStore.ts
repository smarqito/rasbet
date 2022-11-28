import { action, makeObservable, observable, runInAction } from "mobx";
import { toast } from "react-toastify";
import Agent from "../api/agent";
import { IBetInfo, IBetType } from "../models/betType";
import { IActiveGame, IGame } from "../models/game";
import { RootStore } from "./rootStore";

export default class GameStore {
  rootStore: RootStore;

  constructor(rootStore: RootStore) {
    makeObservable(this);
    this.rootStore = rootStore;
  }

  @observable game: IGame | null = null;
  @observable activeGames: IActiveGame[] = [];
  @observable loading = false;

  @action clearGame = () => {
    this.game = null;
  };

  getGame = (id: number) => {
    let game = null;
    for (let index = 0; index < this.activeGames.length; index++) {
      const element = this.activeGames[index];
      if (element.id == id) {
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
      // let gameInfo = await Agent.Game.getGameInfo(id, true);
      // runInAction(() => {
      //   if (gameInfo) {
      //     let detailed: IGame = {
      //       id: id,
      //       name: game.name,
      //       start: gameInfo.start,
      //       sport: game.sport,
      //       state: gameInfo.state,
      //       bets: gameInfo.bets,
      //       mainBet: game.mainBet,
      //     };
      //     this.game = delotailed;
      //   }
      // });

      let betInfo1: IBetType = {
        id: 0,
        type: "Jogador que marcou mais golos",
        odds: [
          { id: 0, name: "Jogador1", value: 1.19 },
          { id: 1, name: "Jogador2", value: 3.19 },
          { id: 2, name: "Jogador3", value: 7.19 },
        ],
      };

      let betInfo2: IBetType = {
        id: 0,
        type: "Número de golos marcados(total)",
        odds: [
          { id: 3, name: "0-5", value: 1.19 },
          { id: 4, name: "1-2", value: 3.19 },
          { id: 5, name: "3-4", value: 7.19 },
        ],
      };

      let betInfo3: IBetType = {
        id: 0,
        type: "Número de cartões amarelos",
        odds: [
          { id: 6, name: "0-5", value: 1.19 },
          { id: 7, name: "1-2", value: 3.19 },
          { id: 8, name: "3-4", value: 7.19 },
        ],
      };

      let detailed: IGame = {
        id: id,
        name: game.name,
        start: game.start,
        sport: game.sport,
        state: "Open",
        bets: [betInfo1, betInfo2],
        mainBet: game.mainBet,
      };
      this.game = detailed;

      this.loading = false;
    } catch (error) {
      toast.error("Não é possível apresentar o jogo!");
      throw error;
    }
  };

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

      var game1: IActiveGame = {
        id: 0,
        name: "Sporting - Varzim",
        start: new Date(),
        sport: "Futebol",
        mainBet: betType1,
      };

      var game2: IActiveGame = {
        id: 1,
        name: "Benfico - Porto",
        start: new Date(),
        sport: "Futebol",
        mainBet: betType2,
      };

      let games = [game1, game2];
      this.activeGames = games;

      this.loading = false;
    } catch (error) {
      this.loading = false;
      toast.error("Sem jogos ativos!");
      throw error;
    }
  };
}
