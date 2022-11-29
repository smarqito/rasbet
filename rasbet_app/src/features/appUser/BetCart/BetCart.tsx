import { Button, Card, Grid, Header, Segment } from "semantic-ui-react";
import { Fragment, useContext, useEffect, useState } from "react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import { observer } from "mobx-react-lite";
import BetCartSimpleElement from "./BetCartSimpleElement";
import BetCartMultipleElement from "./BetCartMultipleElement";

const BetCart: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    simpleBets,
    betMultiple,
    getNumMulti,
    getNumSimple,
    getSimpleAmount,
    getGanhosSimple,
    createBetMultiple,
    createBetSimple,
    getOddMultiple,
  } = rootStore.betStore;

  const [betType, setBetType] = useState("simple");

  function isMultipleValid() {
    return getOddMultiple() < 1.2;
  }

  function isSimpleValid() {
    var res = false;

    for (let index = 0; index < simpleBets.length; index++) {
      const element = simpleBets[index];
      if (element.selection.oddValue < 1.2) res = true;
    }

    return res;
  }

  useEffect(() => {}, [betType, createBetMultiple, createBetSimple]);

  return (
    <Card>
      <Segment.Group>
        <Segment>
          {betType === "simple" ? (
            <Header as="h4">A minha aposta ({getNumSimple()})</Header>
          ) : (
            <Header as="h4">A minha aposta ({getNumMulti()})</Header>
          )}
          <Grid>
            <Grid.Row columns={2}>
              <Grid.Column>
                <Button type="button" onClick={() => setBetType("simple")}>
                  Simples
                </Button>
              </Grid.Column>
              <Grid.Column>
                <Button type="button" onClick={() => setBetType("multiple")}>
                  Múltipla
                </Button>
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Segment>
        <Segment>
          {betType === "simple" ? (
            <Card.Group itemsPerRow={1}>
              {simpleBets.map((x) => {
                return <BetCartSimpleElement selection={x} />;
              })}
            </Card.Group>
          ) : (
            <Card.Group itemsPerRow={1}>
              {betMultiple.selections.map((x) => {
                return <BetCartMultipleElement selection={x} />;
              })}
            </Card.Group>
          )}
        </Segment>
        <Segment>
          {betType === "simple" ? (
            <Fragment>
              <Header as="h5">Montate total: {getSimpleAmount()}€</Header>
              <Header as="h4">Ganhos Possiveis: {getGanhosSimple()} </Header>
            </Fragment>
          ) : (
            <Fragment>
              <Header as="h5">Odd Total: {getOddMultiple()}€</Header>
            </Fragment>
          )}
        </Segment>
        <Segment>
          {betType === "simple" ? (
            <Button
              attached="bottom"
              positive
              type="submit"
              disabled={isSimpleValid()}
              onClick={createBetSimple}
            >
              Aposta já
            </Button>
          ) : (
            <Button
              attached="bottom"
              positive
              type="submit"
              disabled={isMultipleValid()}
              onClick={createBetMultiple}
            >
              Aposta já
            </Button>
          )}
        </Segment>
      </Segment.Group>
    </Card>
  );
};

export default observer(BetCart);
