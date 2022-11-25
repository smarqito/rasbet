import {
  Button,
  Form,
  Grid,
  Header,
  Message,
  Segment,
} from "semantic-ui-react";
import { Field, Form as FinalForm } from "react-final-form";
import { IUserLogin } from "../app/models/user";
import TextInput from "../app/common/TextInput";
import {
  composeValidators,
  isEmail,
  minLength,
  required,
} from "../app/common/Validators";
import ErrorMessage from "../app/common/ErrorMessage";
import { FORM_ERROR } from "final-form";

interface IProps {
  loginFunc: (values: IUserLogin) => any;
  submitting: boolean;
  loading: boolean;
}

const LoginForm: React.FC<IProps> = ({ loginFunc, loading, submitting }) => {
  return (
    <Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
      <Grid.Column style={{ maxWidth: 450 }}>
        <Header as="h1" color="black" textAlign="center">
          Bem Vindo
        </Header>
        <FinalForm
          onSubmit={(values: IUserLogin) =>
            loginFunc(values).catch((error: any) => ({
              [FORM_ERROR]: error,
            }))
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
                <div
                  style={{
                    paddingBottom: 10,
                    paddingRight: 10,
                    textAlign: "right",
                  }}
                >
                  <a href="alterarPasse">Esqueci-me da palavra-passe</a>
                </div>
                {submitFailed && submitError && !dirtySinceLastSubmit && (
                  <ErrorMessage
                    error={submitError}
                    text="Utilizador ou Palavra-Passe errados"
                  />
                )}
                <Button
                  color="green"
                  fluid
                  loading={loading}
                  disabled={(invalid && !dirtySinceLastSubmit) || submitting}
                  size="large"
                  content="Aceder"
                />
              </Segment>
            </Form>
          )}
        />
        <Message>
          Não tem conta? <a href="registo">Registe-se já!</a>
        </Message>
      </Grid.Column>
    </Grid>
  );
};

export default LoginForm;
