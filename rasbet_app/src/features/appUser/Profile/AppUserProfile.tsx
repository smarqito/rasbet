import { observer } from "mobx-react-lite";
import React, { Fragment, useContext, useEffect, useState } from "react";
import { RouteComponentProps } from "react-router-dom";
import {
  Button,
  Card,
  Divider,
  Dropdown,
  Grid,
  Header,
  Input,
  Segment,
  Tab,
} from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import ChangeProfile from "./ChangeProfile";
import ChangeSensitive from "./ChangeSensitive";
import Deposit from "./Deposit";
import Withdraw from "./Withdraw";
import DatePicker, { registerLocale } from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { pt } from "date-fns/locale";
import ListItemNotFound from "../../../app/common/ListItemNotFound";
registerLocale("pt", pt);

interface DetailsParams {
  id: string;
}

const AppUserProfile: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history,
}) => {
  const rootStore = useContext(RootStoreContext);
  const {
    appUserDetails,
    getAppUser,
    getUserBetsLost,
    getUserBetsOpen,
    getUserBetsWon,
    userBetsFiltered,
  } = rootStore.userStore;
  const { wallet, allTransactions, getWallet, getTransactions } =
    rootStore.walletStore;
  const { openModal } = rootStore.modalStore;

  const betFilterTypes = [
    { key: 1, text: "open", value: "open" },
    { key: 2, text: "won", value: "won" },
    { key: 3, text: "lost", value: "lost" },
  ];

  const [betFilter, setBetFilter] = useState<
    string | number | boolean | (string | number | boolean)[] | undefined
  >("open");

  const [DateValueEnd, setDateValueEnd] = useState(new Date());

  useEffect(() => {
    console.log(betFilter);
    if (betFilter == "open")
      getUserBetsOpen(match.params.id, new Date(2001, 1, 1), DateValueEnd);

    if (betFilter == "won")
      getUserBetsWon(match.params.id, new Date(2001, 1, 1), DateValueEnd);

    if (betFilter == "lost")
      getUserBetsLost(match.params.id, new Date(2001, 1, 1), DateValueEnd);

    getAppUser(match.params.id);
    getWallet(match.params.id);
    getTransactions(match.params.id, new Date(2001, 1, 1), DateValueEnd);
  }, [betFilter, setBetFilter, DateValueEnd, match.params.id]);

  const panes = [
    {
      menuItem: "Preferências de conta",
      render: () => (
        <Tab.Pane>
          <Grid padded>
            <Grid.Row columns={2} textAlign="center">
              <Grid.Column>
                <Header as="h4">Nome: </Header>
                <Segment tertiary>{appUserDetails?.name}</Segment>
              </Grid.Column>
              <Grid.Column>
                <Grid>
                  <Grid.Row columns={1} textAlign="center">
                    <Grid.Column>
                      <Header as="h4"> Language:</Header>
                      <Segment tertiary>{appUserDetails?.language}</Segment>
                    </Grid.Column>
                  </Grid.Row>
                  <Grid.Row columns={1} textAlign="center">
                    <Grid.Column>
                      <Header as="h4">Moeda:</Header>
                      <Segment tertiary>{appUserDetails?.coin}(€)</Segment>
                    </Grid.Column>
                  </Grid.Row>
                  <Grid.Row columns={1} textAlign="center">
                    <Grid.Column>
                      <Header as="h4">Notificações:</Header>
                      <Segment tertiary>
                        {appUserDetails?.notif == true ? (
                          <Fragment>A receber notificações por email.</Fragment>
                        ) : (
                          <Fragment>Não está a receber notificações.</Fragment>
                        )}
                      </Segment>
                    </Grid.Column>
                  </Grid.Row>
                </Grid>
              </Grid.Column>
            </Grid.Row>
            <Divider section />
            <Grid.Row columns={1}>
              <Grid.Column>
                <Button
                  fluid
                  content="Alterar o perfil"
                  color="twitter"
                  type="button"
                  onClick={() => openModal(<ChangeProfile />)}
                />
              </Grid.Column>
            </Grid.Row>
            <Grid.Row columns={1}>
              <Grid.Column>
                <Button
                  fluid
                  content="Alterar informações sensíveis (Palavra-Passe, IBAN, Número de Telemóvel)"
                  color="twitter"
                  type="button"
                  onClick={() => openModal(<ChangeSensitive />)}
                />
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Tab.Pane>
      ),
    },
    {
      menuItem: "Carteira",
      render: () => (
        <Tab.Pane>
          <Grid padded divided>
            <Grid.Row columns={3}>
              <Grid.Column verticalAlign="middle">
                <Segment compact inverted secondary>
                  <Header as="h4">
                    Valor em carteira:
                    {wallet?.balance}
                  </Header>
                </Segment>
              </Grid.Column>
              <Grid.Column>
                <Header as="h4">Depositar na carteira</Header>
                <Button
                  color="twitter"
                  type="button"
                  onClick={() => openModal(<Deposit />)}
                >
                  Depositar
                </Button>
              </Grid.Column>
              <Grid.Column>
                <Header as="h4">Levantar dinheiro</Header>
                <Button
                  color="twitter"
                  type="button"
                  onClick={() => openModal(<Withdraw />)}
                >
                  Levantar
                </Button>
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Tab.Pane>
      ),
    },
    {
      menuItem: "Histórico Apostas",
      render: () => (
        <Tab.Pane>
          <Grid celled>
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
            <Card.Group>
              {userBetsFiltered.map((x) => {
                return (
                  <Card>
                    <Card.Header>
                      {x.start.toString()} - {x.end?.toString()}
                    </Card.Header>
                    <Card.Meta>Montante apostado: {x.amount}</Card.Meta>
                    <Card.Description>Ganhos: {x.wonValue}</Card.Description>
                  </Card>
                );
              })}
            </Card.Group>
          )}
        </Tab.Pane>
      ),
    },
    {
      menuItem: "Histórico Transações",
      render: () => (
        <Tab.Pane>
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
        </Tab.Pane>
      ),
    },
  ];

  return (
    <Tab
      menu={{ fluid: true, vertical: true }}
      menuPosition="left"
      panes={panes}
    />
  );
};

export default observer(AppUserProfile);
