import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { Button, Grid, Header, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import Deposit from "./Deposit";
import Withdraw from "./Withdraw";

const Wallet: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { openModal } = rootStore.modalStore;
  const { wallet, getWallet } = rootStore.walletStore;
  const { appUserDetails } = rootStore.userStore;

  useEffect(() => {
    getWallet(appUserDetails!.id);
  }, [appUserDetails!.id]);

  return (
    <Grid padded divided relaxed>
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
  );
};

export default observer(Wallet);
