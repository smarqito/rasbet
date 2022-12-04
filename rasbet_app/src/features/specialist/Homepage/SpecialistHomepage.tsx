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
    getAllSports,
    allSports,
    setSelectedSport,
    clearAllGames,
    clearSports,
    loading,
  } = rootStore.gameStore;

  useEffect(() => {
    clearAllGames();
    clearSports();

    getAllSports();
  }, [match.params.id]);

  if (loading) {
    <LoadingComponent content="A carregarpágina do utilizador!" />;
  }

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
                      onClick={() => setSelectedSport(x)}
                    />
                  </Grid.Row>
                );
              })}
            </Grid>
          </Segment>
        </Grid.Column>
        <Grid.Column width={12} key={"gameList"}>
          <Container style={{ overflow: "scroll", maxHeight: "90vh" }}>
            <Header as="h3">Todos os jogos</Header>
            <SpecialistGameList />
          </Container>
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default observer(SpecialistHomepage);
