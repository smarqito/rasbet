import { observer } from "mobx-react-lite";
import React, { useContext, useState } from "react";
import { Button, Form, Grid, Header, Segment } from "semantic-ui-react";
import { Field, Form as FinalForm } from "react-final-form";
import { FORM_ERROR } from "final-form";
import { RootStoreContext } from "../../../app/stores/rootStore";
import TextInput from "../../../app/common/TextInput";
import {
  AppUserChangeSensitive,
  IAppUserChangeSensitive,
} from "../../../app/models/user";
import { required } from "../../../app/common/Validators";
import SensitiveConfirm from "../../SensitiveConfirm";

const ChangeProfile: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { user, updateAppUserSensitive } = rootStore.userStore;
  const { closeModal, openModal } = rootStore.modalStore;

  const [initialValues] = useState(new AppUserChangeSensitive());

  return (
    <Grid textAlign="center" verticalAlign="middle">
      <Grid.Row>
        <Header> Alterar Informações do perfil</Header>
      </Grid.Row>
      <Grid.Row>
        <Grid.Column>
          <FinalForm
            onSubmit={(values: IAppUserChangeSensitive) => {
              updateAppUserSensitive(
                user!.email,
                values.password,
                values.IBAN,
                values.phoneNumber
              ).catch((error) => ({
                [FORM_ERROR]: error,
              }));
              closeModal();
              openModal(<SensitiveConfirm />);
            }}
            initialValues={initialValues}
            render={({ handleSubmit }) => (
              <Form size="large" onSubmit={handleSubmit}>
                <Segment stacked>
                  <Field
                    fluid
                    name="password"
                    component={TextInput}
                    placeholder="Nova Palavra-Passe"
                    type="password"
                  />
                  <Field
                    name="IBAN"
                    component={TextInput}
                    placeholder="Novo IBAN"
                  />
                  <Field
                    name="phoneNumber"
                    component={TextInput}
                    placeholder="Novo número de telemóvel"
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

export default observer(ChangeProfile);
