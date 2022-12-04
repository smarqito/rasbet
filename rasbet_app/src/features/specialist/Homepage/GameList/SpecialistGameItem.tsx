import React, { useContext } from "react";
import {
  Button,
  Card,
  Divider,
  Grid,
  Header,
  Label,
  Segment,
  SemanticCOLORS,
} from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { CollectiveGame, GameState } from "../../../../app/models/game";
import { RootStoreContext } from "../../../../app/stores/rootStore";
import ChangeOdd from "./ChangeOdd";
import ChangeGameState from "./ChangeGameState";
interface IProps {
  game: CollectiveGame;
}
const GameListItem: React.FC<IProps> = ({ game }) => {
  const rootStore = useContext(RootStoreContext);
  const { openModal } = rootStore.modalStore;

  function handleColor(state: GameState): SemanticCOLORS {
    var res: SemanticCOLORS;

    if (state === "Open") res = "olive";
    else if (state === "Suspended") res = "red";
    return "black";
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
                    <Button
                      color="olive"
                      type="submit"
                      fluid
                      onClick={() =>
                        openModal(<ChangeOdd odd={odd} bet={game.mainBet} />)
                      }
                    >
                      <div style={{ fontSize: "12px" }}>{odd.name}</div>
                      <div style={{ fontSize: "12px" }}>
                        {odd.oddValue.toFixed(2)}
                      </div>
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
              onClick={() => openModal(<ChangeGameState id={game.id} />)}
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
