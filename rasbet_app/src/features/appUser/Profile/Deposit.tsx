import { observer } from "mobx-react-lite";
import React, { useContext, useEffect, useState } from "react";
import { Button, Form, Grid, Header, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import { Field, Form as FinalForm } from "react-final-form";
import TextInput from "../../../app/common/TextInput";
import {
  composeValidators,
  isNumber,
  required,
} from "../../../app/common/Validators";
import {
  CreateDeposit,
  ICreateTransaction,
  ITransaction,
} from "../../../app/models/transaction";
import DatePicker, { registerLocale } from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { pt } from "date-fns/locale";
registerLocale("pt", pt);

const Deposit: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { user } = rootStore.userStore;
  const { depositFunds, getWallet } = rootStore.walletStore;
  const { closeModal } = rootStore.modalStore;

  return (
    <Grid textAlign="center" verticalAlign="middle">
      <Grid.Row>
        <Header> Insira o valor depositar</Header>
      </Grid.Row>
      <Grid.Row>
        <Grid.Column>
          <FinalForm
            onSubmit={(value: { value: number }) => {
              let tran: ICreateTransaction = {
                userId: user!.id,
                value: value.value,
              };
              depositFunds(tran);
              closeModal();
            }}
            render={({ handleSubmit }) => (
              <Form size="large" onSubmit={handleSubmit}>
                <Segment>
                  <Field
                    name="value"
                    component={TextInput}
                    placeholder="Valor a depositar"
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

export default observer(Deposit);
