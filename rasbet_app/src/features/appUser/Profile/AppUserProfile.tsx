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
  Segment,
  Tab,
} from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import ChangeProfile from "./ChangeProfile";
import ChangeSensitive from "./ChangeSensitive";
import Deposit from "./Deposit";
import Withdraw from "./Withdraw";

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
  } = rootStore.userStore;
  const { wallet, allTransactions, getWallet } = rootStore.walletStore;
  const { openModal } = rootStore.modalStore;

  const [userHistory, setUserHistory] = useState("transactions");

  const betFilterTypes = [
    { key: 1, text: "open", value: 1 },
    { key: 2, text: "won", value: 2 },
    { key: 3, text: "lost", value: 3 },
  ];

  const [betFilter, setBetFilter] = useState("open");

  useEffect(() => {
    console.log(betFilter);
    if (betFilter == "open") getUserBetsOpen(match.params.id);

    if (betFilter == "won") getUserBetsWon(match.params.id);

    if (betFilter == "lost") getUserBetsLost(match.params.id);
    getAppUser(match.params.id);
    getWallet(match.params.id);
  }, [betFilter, userHistory, match.params.id]);

  const panes = [
    {
      menuItem: "Preferências de conta",
      render: () => (
        <Tab.Pane>
          <Grid padded>
            <Grid.Row columns={2} textAlign="center">
              <Grid.Column>
                <Header as="h4">Nome: </Header>
                <Segment tertiary>
                  {/* {appUserDetails?.name} */}
                  José Malheiro
                </Segment>
              </Grid.Column>
              <Grid.Column>
                <Grid>
                  <Grid.Row columns={1} textAlign="center">
                    <Grid.Column>
                      <Header as="h4"> Language:</Header>
                      <Segment tertiary>
                        {/* {appUserDetails?.language} */}
                        Português
                      </Segment>
                    </Grid.Column>
                  </Grid.Row>
                  <Grid.Row columns={1} textAlign="center">
                    <Grid.Column>
                      <Header as="h4">Moeda:</Header>
                      <Segment tertiary>
                        {/* {appUserDetails?.coin} */}
                        Euro(€)
                      </Segment>
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
            <Grid.Row columns={2}>
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
            <Grid.Row columns={2}>
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
                <Segment compact inverted>
                  <Header as="h4">
                    Valor em carteira:
                    {/* {wallet?.balance} */}
                    55€
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
      menuItem: "Histórico",
      render: () => (
        <Tab.Pane>
          <Grid celled>
            <Grid.Row columns={1} textAlign="center">
              <Grid.Column>
                <Header>Histórico</Header>
              </Grid.Column>
            </Grid.Row>
            <Grid.Row columns={2} textAlign="center">
              <Grid.Column>
                <Grid>
                  <Grid.Row columns={2}>
                    <Grid.Column width={10}>
                      <Button
                        type="button"
                        color="twitter"
                        fluid
                        onClick={() => setUserHistory("bets")}
                      >
                        Apostas
                      </Button>
                    </Grid.Column>
                    <Grid.Column width={6}>
                      <Dropdown
                        options={betFilterTypes}
                        selection
                        onChange={() => {
                          setBetFilter(betFilter);
                        }}
                      />
                    </Grid.Column>
                  </Grid.Row>
                </Grid>
              </Grid.Column>
              <Grid.Column>
                <Button
                  type="button"
                  color="twitter"
                  fluid
                  onClick={() => setUserHistory("transactions")}
                >
                  Transações
                </Button>
              </Grid.Column>
            </Grid.Row>
          </Grid>
          <Card.Group>
            {userHistory === "transactions" ? (
              <Fragment>
                {allTransactions.map((x) => {
                  return (
                    <Card>
                      <Card.Header>{x.type}</Card.Header>
                      <Card.Meta>{x.date.toString()}</Card.Meta>
                      <Card.Description>{x.value}</Card.Description>
                    </Card>
                  );
                })}
              </Fragment>
            ) : (
              <Fragment>
                {
                  <Card>
                    <Card.Header></Card.Header>
                    <Card.Meta></Card.Meta>
                    <Card.Description></Card.Description>
                  </Card>
                }
              </Fragment>
            )}
          </Card.Group>
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
