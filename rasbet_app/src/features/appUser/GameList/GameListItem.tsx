import React, { useContext } from "react";
import {
  Button,
  Card,
  Grid,
  Header,
  Label,
  Statistic,
} from "semantic-ui-react";
import { CollectiveGame, IActiveGame } from "../../../app/models/game";
import { formatRelative } from "date-fns";
import { pt } from "date-fns/locale";
import { NavLink } from "react-router-dom";
import { observer } from "mobx-react-lite";
import { RootStoreContext } from "../../../app/stores/rootStore";
import ModalAddToCart from "./ModalAddToCart";
interface IProps {
  id: number;
  game: IActiveGame;
}
const GameListItem: React.FC<IProps> = ({ id, game }) => {
  const rootStore = useContext(RootStoreContext);
  const { openModal } = rootStore.modalStore;

  return (
    <Card fluid key={id}>
      <Grid padded>
        <Grid.Row columns={5} verticalAlign="middle" key={"gameItem"}>
          <Grid.Column stretched width={7} key={"gameName"}>
            <Header as={"h3"}>
              {game.game.home} - {game.game.away}
            </Header>
            {formatRelative(game.game.start, new Date(), { locale: pt })}
          </Grid.Column>
          {game.game.mainBet.odds.map((odd) => {
            return (
              <Grid.Column key={odd.id} width={3} textAlign="center">
                <Grid key={"stats&odds"}>
                  <Grid.Row key={odd.name}>
                    <Button
                      color="olive"
                      type="submit"
                      onClick={() => openModal(<ModalAddToCart game={game} odd={odd} />)}
                    >
                      {odd.name} <p /> {odd.value}€
                    </Button>
                  </Grid.Row>
                  <Grid.Row key={game.statistic.statitics.get(odd.id)}>
                    <Label>
                      {game.statistic.statitics.get(odd.id)}% Apostas
                    </Label>
                  </Grid.Row>
                </Grid>
              </Grid.Column>
            );
          })}
        </Grid.Row>
      </Grid>
    </Card>
  );
};

export default observer(GameListItem);
