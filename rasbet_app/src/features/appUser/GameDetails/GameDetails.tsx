import { formatRelative } from "date-fns";
import { pt } from "date-fns/locale";
import { observer } from "mobx-react-lite";
import { useContext, useEffect, useRef } from "react";
import { RouteComponentProps } from "react-router-dom";
import { Button, Card, Divider, Grid, Header, Sticky } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { RootStoreContext } from "../../../app/stores/rootStore";
import BetCart from "../BetCart/BetCart";
import BetTypeList from "../BetTypeList/BetTypeList";
import ModalAddToCart from "../GameList/ModalAddToCart";

interface DetailsParams {
  id: string;
}

const GameDetails: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history,
}) => {
  const rootStore = useContext(RootStoreContext);
  const { loadGame, loading, game } = rootStore.gameStore;
  const { openModal } = rootStore.modalStore;

  useEffect(() => {
    loadGame(parseInt(match.params.id));
  }, [loadGame, game]);

  if (loading || !game)
    return <LoadingComponent content="A carregar o jogo!" />;

  return (
    <Grid>
      <Grid.Row columns={2}>
        <Grid.Column width={12}>
          {/* <Sticky context={stickyRef}> */}
          <Card fluid>
            <Grid padded stackable>
              <Grid.Row color="green" columns={2}>
                <Grid.Column textAlign="right">
                  <Header as={"h3"}>
                    {game.homeTeam} - {game.awayTeam}
                  </Header>
                </Grid.Column>
                <Divider vertical />
                <Grid.Column>
                  {formatRelative(game!.startTime, new Date(), { locale: pt })}
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
                      <Button
                        color="olive"
                        fluid
                        onClick={() =>
                          openModal(<ModalAddToCart game={game} odd={odd} />)
                        }
                      >
                        {odd.name} <p /> {odd.oddValue}€
                      </Button>
                    </Grid.Column>
                  );
                })}
              </Grid.Row>
            </Grid>
          </Card>
          {/* <Card fluid>
        <BetTypeList betTypes={game!.bets} />
      </Card> */}
        </Grid.Column>
        <Grid.Column width={4}>
          <BetCart />
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default observer(GameDetails);
