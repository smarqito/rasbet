import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Button, Form, Grid, Header, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../app/stores/rootStore";
import { Field, Form as FinalForm } from "react-final-form";
import { FORM_ERROR } from "final-form";
import { required } from "../app/common/Validators";
import TextInput from "../app/common/TextInput";

const SensitiveConfirm: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    user,
    updateAppUserSensitiveConfirm,
    updateAdminSensitiveConfirm,
    updateSpecialistSensitiveConfirm,
  } = rootStore.userStore;
  const { closeModal } = rootStore.modalStore;
  return (
    <Grid textAlign="center" verticalAlign="middle">
      <Grid.Row>
        <Header> Insira o código de confirmação. </Header> (enviado por e-mail)
      </Grid.Row>
      <Grid.Row>
        <Grid.Column>
          <FinalForm
            onSubmit={(code: string) => {
              if (user?.role == "AppUser") {
                updateAppUserSensitiveConfirm(code).catch((error) => ({
                  [FORM_ERROR]: error,
                }));
              } else if (user?.role == "Admin") {
                updateAdminSensitiveConfirm(code).catch((error) => ({
                  [FORM_ERROR]: error,
                }));
              } else {
                updateSpecialistSensitiveConfirm(code).catch((error) => ({
                  [FORM_ERROR]: error,
                }));
              }

              closeModal();
            }}
            render={({ handleSubmit }) => (
              <Form size="large" onSubmit={handleSubmit}>
                <Segment stacked>
                  <Field
                    fluid
                    name="pass"
                    component={TextInput}
                    placeholder="Código"
                    validate={required}
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

export default observer(SensitiveConfirm);
