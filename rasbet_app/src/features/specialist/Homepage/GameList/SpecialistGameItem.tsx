import React, { useContext } from "react";
import {
  Button,
  Card,
  Divider,
  Grid,
  Header,
  Label,
  Rail,
  Segment,
  SemanticCOLORS,
} from "semantic-ui-react";
import { formatRelative } from "date-fns";
import { pt } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import { NavLink } from "react-router-dom";
import { GameState, IActiveGame } from "../../../../app/models/game";
import { RootStoreContext } from "../../../../app/stores/rootStore";
import ChangeOdd from "./ChangeOdd";
import ChangeGameState from "./ChangeGameState";
interface IProps {
  game: IActiveGame;
}
const GameListItem: React.FC<IProps> = ({ game }) => {
  const rootStore = useContext(RootStoreContext);
  const { openModal } = rootStore.modalStore;

  function buttonColor(odd: number): SemanticCOLORS {
    var grey: SemanticCOLORS = "grey";
    var red: SemanticCOLORS = "red";
    var green: SemanticCOLORS = "green";

    var highest = { id: -1, value: 0 };
    var lowest = { id: -1, value: 100 };

    for (let x in game.statistics.statistics) {
      let value = game.statistics.statistics[parseInt(x)];
      if (value! > highest.value) {
        highest.value = value!;
        highest.id = parseInt(x);
      }

      if (value! < lowest.value) {
        lowest.value = value!;
        lowest.id = parseInt(x);
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

    if (odd == highest.id) return green;

    if (odd == lowest.id) return red;

    return grey;
  }

  function handleColor(state: GameState): SemanticCOLORS {
    var res: SemanticCOLORS;

    if (state === "Open") res = "olive";
    else res = "black";
    return res;
  }

  return (
    <Card fluid key={game.id} color={handleColor(game.state)}>
      <Grid padded>
        <Grid.Row columns={5} verticalAlign="middle" key={"gameItem"}>
          <Grid.Column stretched width={7} key={"gameName"}>
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
                          openModal(<ChangeOdd odd={odd} bet={game.mainBet} />)
                        }
                      >
                        <div style={{ fontSize: "12px" }}>{odd.name}</div>
                        <div style={{ fontSize: "12px" }}>{odd.oddValue}</div>
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
        <Divider section />
        <Grid.Row columns={2} verticalAlign="middle">
          <Grid.Column width={13}>
            <Button
              basic
              fluid
              color="black"
              content="Alterar jogo"
              onClick={() => openModal(<ChangeGameState />)}
            />
          </Grid.Column>
          <Grid.Column width={3}>
            <Segment inverted color={handleColor(game.state)}>
              {game.state}
            </Segment>
          </Grid.Column>
        </Grid.Row>
      </Grid>
    </Card>
  );
};

export default observer(GameListItem);
