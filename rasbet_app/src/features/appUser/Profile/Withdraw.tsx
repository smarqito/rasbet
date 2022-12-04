import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Button, Form, Grid, Header, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import { Field, Form as FinalForm } from "react-final-form";
import TextInput from "../../../app/common/TextInput";
import {
  composeValidators,
  isNumber,
  required,
} from "../../../app/common/Validators";
import { ICreateTransaction } from "../../../app/models/transaction";

const Withdraw: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { user } = rootStore.userStore;
  const { withdrawFunds } = rootStore.walletStore;
  const { closeModal } = rootStore.modalStore;

  return (
    <Grid textAlign="center" verticalAlign="middle">
      <Grid.Row>
        <Header> Insira o valor levantar</Header>
      </Grid.Row>
      <Grid.Row>
        <Grid.Column>
          <FinalForm
            onSubmit={(value: { value: number }) => {
              let tran: ICreateTransaction = {
                userId: user!.id,
                value: value.value,
              };
              withdrawFunds(tran);
              closeModal();
            }}
            render={({ handleSubmit }) => (
              <Form size="large" onSubmit={handleSubmit}>
                <Segment>
                  <Field
                    name="value"
                    component={TextInput}
                    placeholder="Valor a levantar"
                    validate={composeValidators(required, isNumber)}
                  />
                </Segment>
                <Button color="twitter" fluid size="large" content="Concluir" />
              </Form>
            )}
          />
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default observer(Withdraw);
