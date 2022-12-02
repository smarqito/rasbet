import { observer } from "mobx-react-lite";
import React, { useContext, useEffect, useState } from "react";
import { RouteComponentProps } from "react-router-dom";
import { Button, Container, Grid, Header, Segment } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { RootStoreContext } from "../../../app/stores/rootStore";
import SpecialistGameList from "./GameList/SpecialistGameList";

interface DetailsParams {
  id: string;
}

const SpecialistHomepage: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history,
}) => {
  const rootStore = useContext(RootStoreContext);
  const {
    getActiveAndSuspended,
    gamesFiltered,
    getAllSports,
    allSports,
    getActiveGamesBySport,
    clearActive,
    clearFiltered,
    clearSports,
    loading,
  } = rootStore.gameStore;
  const [sport, setSport] = useState({ name: "Futebol" });

  useEffect(() => {
    clearActive();
    clearFiltered();
    clearSports();

    getActiveAndSuspended();
    getAllSports();
    getActiveGamesBySport(sport);
  }, [setSport, sport, match.params.id]);

  return (
    <Grid padded divided stackable>
      <Grid.Row columns={2} key={"AppUserPage"}>
        <Grid.Column width={4} key={"sport"}>
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
        <Grid.Column width={12} key={"gameList"}>
          <Container>
            <Header as="h3">Todos os jogos</Header>
            <SpecialistGameList games={gamesFiltered} loading={loading} />
          </Container>
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default observer(SpecialistHomepage);
