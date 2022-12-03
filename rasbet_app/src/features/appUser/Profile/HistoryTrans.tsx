import { observer } from "mobx-react-lite";
import { Fragment, useContext, useEffect, useState } from "react";
import { Card, Grid, Header } from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import ListItemNotFound from "../../../app/common/ListItemNotFound";
import DatePicker, { registerLocale } from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { pt } from "date-fns/locale";
registerLocale("pt", pt);

const HistoryTrans: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { allTransactions, getTransactions } = rootStore.walletStore;
  const { appUserDetails } = rootStore.userStore;

  const [DateValueEnd, setDateValueEnd] = useState(new Date());

  useEffect(() => {
    getTransactions(appUserDetails!.id, new Date(2001, 1, 1), DateValueEnd);
  }, [DateValueEnd, appUserDetails!.id]);

  return (
    <Fragment>
      <Grid padded celled>
        <Grid.Row columns={1} textAlign="center">
          <Grid.Column>
            <Header>Histórico de Transações!</Header>
          </Grid.Column>
        </Grid.Row>
        <Grid.Row columns={1} textAlign="left">
          <Grid.Column>
            <DatePicker
              selected={DateValueEnd}
              onChange={(date) => date && setDateValueEnd(date)}
              locale="pt"
              dateFormat={"P"}
            />
          </Grid.Column>
        </Grid.Row>
      </Grid>
      {allTransactions.length == 0 ? (
        <Fragment>
          <p />
          <ListItemNotFound content="Não existem transações até a data colocada!" />
        </Fragment>
      ) : (
        <Card.Group>
          {allTransactions.map((x) => {
            return (
              <Card>
                <Card.Header>{x.type}</Card.Header>
                <Card.Meta>{x.date.toString()}</Card.Meta>
                <Card.Description>{x.value}</Card.Description>
              </Card>
            );
          })}
        </Card.Group>
      )}
    </Fragment>
  );
};

export default observer(HistoryTrans);
