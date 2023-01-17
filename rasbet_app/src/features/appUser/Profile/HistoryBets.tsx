import { observer } from "mobx-react-lite";
import React, { Fragment, useContext, useEffect, useState } from "react";
import { Card, Dropdown, Grid, Header } from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import ListItemNotFound from "../../../app/common/ListItemNotFound";
import DatePicker, { registerLocale } from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { pt } from "date-fns/locale";
registerLocale("pt", pt);

const HistoryBets: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    getUserBetsOpen,
    getUserBetsWon,
    getUserBetsClosed,
    userBetsFiltered,
    appUserDetails,
  } = rootStore.userStore;

  const betFilterTypes = [
    { key: 1, text: "open", value: "open" },
    { key: 2, text: "won", value: "won" },
    { key: 3, text: "close", value: "close" },
  ];

  const [betFilter, setBetFilter] = useState<
    string | number | boolean | (string | number | boolean)[] | undefined
  >("open");

  const [DateValueEnd, setDateValueEnd] = useState(new Date());

  return (
    <Fragment>
      <Grid padded celled>
        <Grid.Row columns={1} textAlign="center">
          <Grid.Column>
            <Header>Histórico de Apostas</Header>
          </Grid.Column>
        </Grid.Row>
        <Grid.Row columns={1} textAlign="center">
          <Grid.Column>
            <Grid centered>
              <Grid.Row columns={2} textAlign="left">
                <Grid.Column>
                  <Dropdown
                    options={betFilterTypes}
                    selection
                    value={betFilter}
                    onChange={(_, data) => {
                      setBetFilter(data.value);
                      if (betFilter == "open") {
                        getUserBetsOpen(
                          appUserDetails!.id,
                          new Date(2001, 1, 1),
                          DateValueEnd
                        );
                      } else if (betFilter == "won") {
                        getUserBetsWon(
                          appUserDetails!.id,
                          new Date(2001, 1, 1),
                          DateValueEnd
                        );
                      } else if (betFilter == "close") {
                        getUserBetsClosed(
                          appUserDetails!.id,
                          new Date(2001, 1, 1),
                          DateValueEnd
                        );
                      }
                    }}
                  />
                </Grid.Column>
                <Grid.Row>
                  <DatePicker
                    selected={DateValueEnd}
                    onChange={(date) => date && setDateValueEnd(date)}
                    locale="pt"
                    dateFormat={"P"}
                  />
                </Grid.Row>
              </Grid.Row>
            </Grid>
          </Grid.Column>
        </Grid.Row>
      </Grid>
      {userBetsFiltered.length == 0 ? (
        <ListItemNotFound content="Não existem apostas realizadas!" />
      ) : (
        <Grid celled style={{ overflow: "scroll", maxHeight: "90vh" }}>
          {userBetsFiltered.map((x) => {
            console.log(x.selections.length);
            return (
              <Grid.Row
                columns={1}
                textAlign="center"
                key={x.start + "" + x.odd}
              >
                <Grid.Column>
                  <Card fluid>
                    <Card.Content>
                      <Card.Header>
                        Aposta{" "}
                        {x.selections.length == 1 ? "Simples" : "Múltipla"}
                      </Card.Header>
                      <Card.Meta>
                        {x.start.toString()} - {x.end?.toString()}
                      </Card.Meta>
                      <Card.Description>
                        <b>Montante apostado</b> : {x.amount.toFixed(2)}
                      </Card.Description>
                      <Card.Description>
                        <b>Cota escolhida </b>: {x.odd.toFixed(2)}
                      </Card.Description>
                      <Card.Description>
                        Ganhos: {x.wonValue.toFixed(2)}
                      </Card.Description>
                    </Card.Content>
                  </Card>
                </Grid.Column>
              </Grid.Row>
            );
          })}
        </Grid>
      )}
    </Fragment>
  );
};

export default observer(HistoryBets);
