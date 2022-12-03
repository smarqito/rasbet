import {
  Button,
  Card,
  Grid,
  Header,
  Input,
  Label,
  Segment,
} from "semantic-ui-react";
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
    getGanhosMultiple,
    createBetMultiple,
    createBetSimple,
    getOddMultiple,
  } = rootStore.betStore;

  const [betType, setBetType] = useState("simple");

  function isMultipleValid() {
    var disabled = false;

    if (getOddMultiple < 1.2 && betMultiple.amount < 0.01) disabled = true;

    if (!disabled && betMultiple.selections.length < 2) disabled = true;

    if (!disabled) {
      for (let index1 = 0; index1 < betMultiple.selections.length; index1++) {
        const element1 = betMultiple.selections[index1];

        for (let index2 = 0; index2 < betMultiple.selections.length; index2++) {
          const element2 = betMultiple.selections[index2];

          if (
            element1.odd.id !== element2.odd.id &&
            element1.game.id == element2.game.id
          )
            disabled = true;
        }
      }
    }

    return disabled;
  }

  function isSimpleValid() {
    var res = false;

    for (let index = 0; index < simpleBets.length; index++) {
      const element = simpleBets[index];
      if (
        element.selection.oddValue < 1.2 ||
        element.selection.oddValue < 0.01
      ) {
        res = true;
      }
    }
    if (!res && simpleBets.length < 1) res = true;

    return res;
  }

  const [amountV, setAmountV] = useState(0);

  const handleChange = (amount: number) => {
    setAmountV(amount);
    betMultiple.amount = amount.valueOf();
  };

  return (
    <div>
      <Segment.Group>
        <Segment>
          <Header as="h4">A sua aposta.</Header>
        </Segment>
        <Segment>
          <Grid>
            <Grid.Row columns={2} textAlign="center">
              <Grid.Column>
                <Button as="div" labelPosition="right">
                  <Button
                    type="button"
                    onClick={() => setBetType("simple")}
                    color="twitter"
                    floated="left"
                  >
                    Simples
                  </Button>
                  <Label as="a" basic pointing="left" color="blue">
                    {getNumSimple()}
                  </Label>
                </Button>
              </Grid.Column>
              <Grid.Column>
                <Button as="div" labelPosition="right">
                  <Button
                    type="button"
                    onClick={() => setBetType("multiple")}
                    color="twitter"
                  >
                    Múltipla
                  </Button>
                  <Label as="a" basic pointing="left" color="blue">
                    {getNumMulti()}
                  </Label>
                </Button>
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Segment>
      </Segment.Group>
      <Segment style={{ overflow: "auto", maxHeight: "55vh" }}>
        {betType === "simple" ? (
          <Card.Group itemsPerRow={1}>
            {simpleBets.map((x, i) => {
              return <BetCartSimpleElement key={i} selection={x} />;
            })}
          </Card.Group>
        ) : (
          <Card.Group itemsPerRow={1}>
            {betMultiple.selections.map((x, i) => {
              return <BetCartMultipleElement key={i} selection={x} />;
            })}
          </Card.Group>
        )}
      </Segment>
      <Segment.Group>
        <Segment>
          {betType === "simple" ? (
            <Fragment>
              <div style={{ fontSize: "15px", paddingBottom: "7px" }}>
                <b>Montate total</b>: {getSimpleAmount}€
              </div>
              <div style={{ fontSize: "16px" }}>
                <b>Ganhos Possiveis</b>: {getGanhosSimple().toFixed(2)}{" "}
              </div>
            </Fragment>
          ) : (
            <div style={{ fontSize: "16px" }}>
              <b>Odd Multípla</b>: {getOddMultiple.toFixed(2)}{" "}
            </div>
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
            <Fragment>
              <Input
                onChange={(_, data) =>
                  handleChange(
                    parseInt(data.value.length > 0 ? data.value : "0")
                  )
                }
                value={amountV}
              />
              <b> Ganhos</b>: {amountV} x {getOddMultiple.toFixed(2)} ={" "}
              {getGanhosMultiple().toFixed(2)}
              <p />
              <Button
                attached="bottom"
                positive
                type="submit"
                disabled={isMultipleValid()}
                onClick={createBetMultiple}
              >
                Aposta já
              </Button>
            </Fragment>
          )}
        </Segment>
      </Segment.Group>
    </div>
  );
};

export default observer(BetCart);
