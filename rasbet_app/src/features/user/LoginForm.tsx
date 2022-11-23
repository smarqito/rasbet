import {
  Button,
  Form,
  Grid,
  Header,
  Message,
  Segment,
} from "semantic-ui-react";
import { Field, Form as FinalForm } from "react-final-form";
import { IUserLogin } from "../../app/models/user";
import TextInput from "../../app/common/TextInput";
import {
  composeValidators,
  isEmail,
  minLength,
  required,
} from "../../app/common/Validators";
import ErrorMessage from "../../app/common/ErrorMessage";
import { useContext, useState } from "react";
import { RootStoreContext } from "../../app/stores/rootStore";
import { FORM_ERROR } from "final-form";

const LoginForm = () => {
  const rootStore = useContext(RootStoreContext);
  const { login } = rootStore.userStore;
  const [error, setError] = useState();

  return (
    <Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
      <Grid.Column style={{ maxWidth: 450 }}>
        <Header as="h1" color="teal" textAlign="center">
          Autenticação
        </Header>
        <FinalForm
          onSubmit={(values: IUserLogin) =>
            login(values).catch((error) => {
              setError(error);
              return {
                [FORM_ERROR]: error,
              };
            })
          }
          render={({
            handleSubmit,
            submitError,
            invalid,
            submitFailed,
            dirtySinceLastSubmit,
          }) => (
            <Form size="large" onSubmit={handleSubmit}>
              <Segment stacked>
                <Field
                  fluid
                  name="email"
                  icon="user"
                  component={TextInput}
                  iconPosition="left"
                  placeholder="E-mail"
                  validate={composeValidators(required, isEmail)}
                />
                <Field
                  fluid
                  name="password"
                  icon="lock"
                  component={TextInput}
                  iconPosition="left"
                  placeholder="Password"
                  type="password"
                  validate={composeValidators(required, minLength(6))}
                />
                {submitFailed && submitError && !dirtySinceLastSubmit && (
                  <ErrorMessage
                    error={submitError}
                    text="Utilizador ou pass errados"
                  />
                )}
                <Button
                  color="teal"
                  fluid
                  disabled={invalid && !dirtySinceLastSubmit}
                  size="large"
                  content="Entrar"
                />
              </Segment>
            </Form>
          )}
        />
        <Message>
          Não tem conta ? <a href="registo">Regista-se</a>
        </Message>
      </Grid.Column>
    </Grid>
  );
};

export default LoginForm;
