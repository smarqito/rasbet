import { observer } from "mobx-react-lite";
import GameList from "../GameList/GameList";
import { Button, Container, Grid, Header, Segment } from "semantic-ui-react";
import BetCart from "../BetCart/BetCart";
import { useContext, useEffect, useState } from "react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import { RouteComponentProps } from "react-router-dom";

interface DetailsParams {
  id: string;
}

const AppUserDashboard: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history,
}) => {
  const rootStore = useContext(RootStoreContext);
  const {
    getActiveGames,
    gamesFiltered,
    getAllSports,
    allSports,
    getActiveGamesBySport,
    clearActive,
    clearFiltered,
    clearSports,
    loading,
  } = rootStore.gameStore;

  const [sport, setSport] = useState({ name: "Football" });

  useEffect(() => {
    clearActive();
    clearFiltered();
    clearSports();

    getActiveGames();
    getAllSports();
    getActiveGamesBySport(sport);
  }, [setSport, sport, match.params.id]);

  return (
    <Grid padded divided stackable>
      <Grid.Row columns={3} key={"AppUserPage"}>
        <Grid.Column width={3} key={"sport"}>
          <Segment>
            <Header as="h3">Desportos</Header>
            <Grid padded centered>
              {allSports.map((x) => {
                return (
                  <Grid.Row key={x.name}>
                    <Button
                      content={x.name}
                      fluid
                      type="button"
                      onClick={() => setSport(x)}
                    />
                  </Grid.Row>
                );
              })}
            </Grid>
          </Segment>
        </Grid.Column>
        <Grid.Column width={8} key={"gameList"}>
          <Container>
            <Header as="h3">Todos os jogos</Header>
            <GameList games={gamesFiltered} loading={loading} />
          </Container>
        </Grid.Column>
        <Grid.Column width={5} key={"BetCart"}>
          <BetCart />
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default observer(AppUserDashboard);
