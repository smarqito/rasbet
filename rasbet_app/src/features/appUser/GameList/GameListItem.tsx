import React, { useContext } from "react";
import {
  Button,
  Card,
  Grid,
  Header,
  Label,
  SemanticCOLORS,
} from "semantic-ui-react";
import { IActiveGame } from "../../../app/models/game";
import { formatRelative } from "date-fns";
import { pt } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import { RootStoreContext } from "../../../app/stores/rootStore";
import ModalAddToCart from "./ModalAddToCart";
import { NavLink } from "react-router-dom";
interface IProps {
  id: number;
  game: IActiveGame;
}
const GameListItem: React.FC<IProps> = ({ id, game }) => {
  const rootStore = useContext(RootStoreContext);
  const { openModal } = rootStore.modalStore;

  function buttonColor(odd: number): SemanticCOLORS {
    var grey: SemanticCOLORS = "grey";
    var red: SemanticCOLORS = "red";
    var green: SemanticCOLORS = "green";

    var highest = { id: -1, value: 0 };
    var lowest = { id: -1, value: 100 };

    game.statistic.statitics.forEach((value, key) => {
      if (value > highest.value) {
        highest.value = value;
        highest.id = key;
      }

      if (value < lowest.value) {
        lowest.value = value;
        lowest.id = key;
      }
    });

    if (odd == highest.id) return green;

    if (odd == lowest.id) return red;

    return grey;
  }

  return (
    <Card fluid key={id}>
      <Grid padded>
        <Grid.Row columns={5} verticalAlign="middle" key={"gameItem"}>
          <Grid.Column
            stretched
            width={7}
            key={"gameName"}
            as={NavLink}
            to={`/user/game/details/${game.game.id}`}
          >
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
                    <Button as="div" labelPosition="right">
                      <Button
                        color={buttonColor(odd.id)}
                        type="submit"
                        onClick={() =>
                          openModal(<ModalAddToCart game={game.game} odd={odd} />)
                        }
                      >
                        <div style={{ fontSize: "12px" }}>{odd.name}</div>
                        <div style={{ fontSize: "12px" }}>{odd.value}</div>
                      </Button>
                      <Label
                        as="a"
                        basic
                        key={"stats"}
                        pointing="left"
                        color={buttonColor(odd.id)}
                      >
                        {game.statistic.statitics.get(odd.id)}%
                      </Label>
                    </Button>
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
