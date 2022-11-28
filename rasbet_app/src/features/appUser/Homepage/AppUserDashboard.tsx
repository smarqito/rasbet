import { observer } from "mobx-react-lite";
import GameList from "../GameList/GameList";
import { Container, Grid } from "semantic-ui-react";
import SportButtons from "./SportButtons";
import BetCart from "../BetCart/BetCart";
import { useContext, useEffect } from "react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import { RouteComponentProps } from "react-router-dom";
import LoadingComponent from "../../../app/layout/LoadingComponent";

interface DetailsParams {
  id: string;
}

const AppUserDashboard: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history,
}) => {
  const rootStore = useContext(RootStoreContext);
  const { getActiveGames, activeGames } = rootStore.gameStore;

  useEffect(() => {
    getActiveGames();
  }, [getActiveGames, activeGames, match.params.id]);

  return (
    <Grid padded divided stackable>
      <Grid.Row columns={3}>
        <Grid.Column width={3}>
          <SportButtons />
        </Grid.Column>
        <Grid.Column width={8}>
          <Container>
            <GameList games={activeGames} />
          </Container>
        </Grid.Column>
        <Grid.Column width={5}>
          <BetCart />
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default observer(AppUserDashboard);
