import React, { useContext, useEffect } from "react";
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
  game: IActiveGame;
}
const GameListItem: React.FC<IProps> = ({ game }) => {
  const rootStore = useContext(RootStoreContext);
  const { openModal } = rootStore.modalStore;

  function buttonColor(odd: number): SemanticCOLORS {
    let grey: SemanticCOLORS = "grey";
    let red: SemanticCOLORS = "red";
    let green: SemanticCOLORS = "green";
    let highest = { idValue: -1, value: 0 };
    let lowest = { idValue: -1, value: 100 };

    for (let x in game.statistics.statistics) {
      let value = game.statistics.statistics[parseInt(x)];
      if (value! > highest.value) {
        highest.value = value!;
        highest.idValue = parseInt(x);
      } else if (value! < lowest.value) {
        lowest.value = value!;
        lowest.idValue = parseInt(x);
      }
    }

    let allEqual = true;
    for (let x in game.statistics.statistics) {
      let value = game.statistics.statistics[parseInt(x)];
      for (let y in game.statistics.statistics) {
        let valueY = game.statistics.statistics[parseInt(y)];

        if (value !== valueY) allEqual = false;
      }
    }

    if (allEqual) return grey;

    if (odd == highest.idValue) return green;

    if (odd == lowest.idValue) return red;

    return grey;
  }

  return (
    <Card fluid key={game.id}>
      <Grid padded>
        <Grid.Row columns={5} verticalAlign="middle" key={"gameItem"}>
          <Grid.Column
            stretched
            width={7}
            key={"gameName"}
            as={NavLink}
            to={`/user/game/details/${game.id}`}
          >
            <Header as={"h3"}>
              {game.homeTeam} - {game.awayTeam}
            </Header>
            <div>{game.startTime.toLocaleString()}</div>
          </Grid.Column>
          {game.mainBet.odds.map((odd) => {
            return (
              <Grid.Column key={odd.id} width={3} textAlign="center">
                <Grid key={"stats&odds"}>
                  <Grid.Row key={odd.name}>
                    <Button as="div" labelPosition="right">
                      <Button
                        color={buttonColor(odd.id)}
                        type="submit"
                        onClick={() =>
                          openModal(<ModalAddToCart game={game} odd={odd} />)
                        }
                      >
                        <div style={{ fontSize: "12px" }}>{odd.name}</div>
                        <div style={{ fontSize: "12px" }}>
                          {odd.oddValue.toFixed(2)}
                        </div>
                      </Button>
                      <Label
                        as="a"
                        basic
                        key={"stats"}
                        pointing="left"
                        color={buttonColor(odd.id)}
                      >
                        {game.statistics.statistics[odd.id]}%
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
