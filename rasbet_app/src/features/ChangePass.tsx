import React, { useContext } from "react";
import { Button, Form, Grid, Header, Label, Segment } from "semantic-ui-react";
import { Field, Form as FinalForm } from "react-final-form";
import { IUserPwd, UserPwdFormValues } from "../app/models/user";
import TextInput from "../app/common/TextInput";
import {
  composeValidators,
  isEmail,
  minLength,
  required,
} from "../app/common/Validators";
import { FORM_ERROR } from "final-form";
import ErrorMessage from "../app/common/ErrorMessage";
import { observer } from "mobx-react-lite";
import { RootStoreContext } from "../app/stores/rootStore";

const ChangePass: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { changePassByEmail } = rootStore.userStore;

  return (
    <Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
      <Grid.Column style={{ maxWidth: 450 }}>
        <Header as="h1" color="black" textAlign="center">
          Alterar Palavra-Passe
        </Header>
        <FinalForm
          onSubmit={(email: string) =>
            changePassByEmail(email).catch((error: any) => {
              console.log(error);
              return {
                [FORM_ERROR]: error,
              };
            })
          }
          render={({
            handleSubmit,
            submitFailed,
            submitError,
            dirtySinceLastSubmit,
            invalid,
          }) => (
            <Form onSubmit={handleSubmit} size="large">
              <Segment stacked>
                <Field
                  name="email"
                  component={TextInput}
                  fluid
                  icon="user"
                  iconPosition="left"
                  placeholder="Email do utilizador"
                  validate={composeValidators(required, isEmail)}
                />
                {submitFailed && submitError && !dirtySinceLastSubmit && (
                  <ErrorMessage
                    error={submitError}
                    text="Utilizador ou Palavra-Passe inválidos!"
                  />
                )}
                <Button
                  color="green"
                  fluid
                  size="large"
                  disabled={invalid && !dirtySinceLastSubmit}
                >
                  Alterar Palavra-Passe
                </Button>
              </Segment>
            </Form>
          )}
        />
      </Grid.Column>
    </Grid>
  );
};

export default observer(ChangePass);
