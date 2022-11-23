import { observer } from "mobx-react-lite";
import { Button, Form, Grid, Header, Segment } from "semantic-ui-react";
import { Field, Form as FinalForm } from "react-final-form";
import { useContext } from "react";
import TextInput from "../../app/common/TextInput";
import {
  composeValidators,
  isEmail,
  minLength,
  required,
} from "../../app/common/Validators";
import ErrorMessage from "../../app/common/ErrorMessage";
import { RootStoreContext } from "../../app/stores/rootStore";
import { IAppUserRegister } from "../../app/models/user";
import { FORM_ERROR } from "final-form";

const RegisterForm: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { registerAppUser } = rootStore.userStore;

  return (
    <Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
      <Grid.Column style={{ maxWidth: 450 }}>
        <Header as="h1" color="teal" textAlign="center">
          Registo
        </Header>
        <FinalForm
          onSubmit={(values: IAppUserRegister) => {
            registerAppUser(values).catch((error) => ({
              [FORM_ERROR]: error,
            }));
          }}
          render={({
            handleSubmit,
            submitError,
            submitFailed,
            invalid,
            dirtySinceLastSubmit,
          }) => (
            <Form onSubmit={handleSubmit} size="large">
              <Segment stacked>
                <Field
                  name="username"
                  fluid
                  component={TextInput}
                  icon="user"
                  iconPosition="left"
                  placeholder="Nome de Utilizador"
                  validate={required}
                />
                <Field
                  name="email"
                  component={TextInput}
                  fluid
                  icon="user"
                  iconPosition="left"
                  placeholder="E-mail"
                  validate={composeValidators(required, isEmail)}
                />
                <Field
                  name="password"
                  component={TextInput}
                  fluid
                  icon="lock"
                  iconPosition="left"
                  placeholder="Password"
                  type="password"
                  validate={composeValidators(required, minLength(6))}
                />
                {submitError && !dirtySinceLastSubmit && (
                  <ErrorMessage
                    error={submitError}
                    text="Utilizador ou pass errados"
                  />
                )}
                <Button
                  color="teal"
                  fluid
                  size="large"
                  disabled={submitFailed || (invalid && !dirtySinceLastSubmit)}
                >
                  Registar-se
                </Button>
              </Segment>
            </Form>
          )}
        />
      </Grid.Column>
    </Grid>
  );
};

export default observer(RegisterForm);
