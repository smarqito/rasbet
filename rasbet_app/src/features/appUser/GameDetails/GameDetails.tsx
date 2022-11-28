import { formatRelative } from "date-fns";
import { pt } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import { useContext, useEffect, useRef } from "react";
import { RouteComponentProps } from "react-router-dom";
import { Button, Card, Divider, Grid, Header, Sticky } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { RootStoreContext } from "../../../app/stores/rootStore";
import BetTypeList from "../BetTypeList/BetTypeList";

interface DetailsParams {
  id: string;
}

const GameDetails: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history,
}) => {
  const rootStore = useContext(RootStoreContext);
  const { loadGame, game, loading } = rootStore.gameStore;
  const stickyRef = useRef(null);

  useEffect(() => {
    loadGame(parseInt(match.params.id));
  }, [loadGame, game]);

  if (loading || !game)
    return <LoadingComponent content="A carregar o jogo!" />;

  return (
    <div ref={stickyRef} style={{ margin: "7em" }}>
      {/* <Sticky context={stickyRef}> */}
      <Card fluid>
        <Grid padded stackable centered  >
          <Grid.Row color="green" columns={9}>
            <Grid.Column>
              <Header as={"h3"}>{game!.name}</Header>
            </Grid.Column>
            <Divider vertical/>
            <Grid.Column>
              {formatRelative(game!.start, new Date(), { locale: pt })}
            </Grid.Column>
          </Grid.Row>
        </Grid>
      </Card>
      {/* </Sticky> */}
      <Card fluid key={game!.id} color="olive">
        <Grid padded centered>
          <Grid.Row>
            <Header as={"h3"}>Resultado (Tempo Regulamentar)</Header>
          </Grid.Row>
        </Grid>
        <Grid padded centered>
          <Grid.Row columns={3}>
            {game!.mainBet.odds.map((odd) => {
              return (
                <Grid.Column key={odd.id}>
                  <Button color="olive" fluid>
                    {odd.name} <p /> {odd.value}€
                  </Button>
                </Grid.Column>
              );
            })}
          </Grid.Row>
        </Grid>
      </Card>
      <Card fluid>
        <BetTypeList betTypes={game!.bets} />
      </Card>
    </div>
  );
};

export default observer(GameDetails);
