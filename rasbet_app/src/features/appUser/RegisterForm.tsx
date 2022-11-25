import { observer } from "mobx-react-lite";
import {
  Button,
  Checkbox,
  Form,
  Grid,
  Header,
  Segment,
} from "semantic-ui-react";
import { Field, Form as FinalForm } from "react-final-form";
import { useContext, useState } from "react";
import TextInput from "../../app/common/TextInput";
import {
  composeValidators,
  exactLength,
  isEmail,
  minLength,
  required,
} from "../../app/common/Validators";
import ErrorMessage from "../../app/common/ErrorMessage";
import { RootStoreContext } from "../../app/stores/rootStore";
import {
  AppUserRegisterFormValues,
  IAppUserRegister,
} from "../../app/models/user";
import { FORM_ERROR } from "final-form";
import SelectInput from "../../app/common/SelectInput";
import { Languages } from "../../app/common/Languages";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";

interface IErrors {
  repetePass?: string;
  dob?: string;
}

const RegisterForm: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { registerAppUser, submitting } = rootStore.userStore;
  const [checkbox, setCheckbox] = useState(false);
  const [DateValue, setDateValue] = useState(new Date());

  const [initialValues] = useState(new AppUserRegisterFormValues());

  function getAge(birthDate: Date) {
    var today = new Date();
    var age = today.getFullYear() - birthDate.getFullYear();
    var m = today.getMonth() - birthDate.getMonth();
    if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
      age--;
    }
    return age;
  }

  return (
    <Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
      <Grid.Column style={{ maxWidth: 450 }}>
        <Header as="h1" color="black" textAlign="center">
          Registo
        </Header>
        <FinalForm
          onSubmit={(values: IAppUserRegister) => {
            values.notif = checkbox;
            values.DOB = DateValue;
            registerAppUser(values).catch((error) => ({
              [FORM_ERROR]: error,
            }));
          }}
          validate={(values) => {
            const errors: IErrors = {};
            if (values.password !== values.repetePass)
              errors.repetePass = "Não coincidem!";

            if (getAge(values.DOB) < 18) errors.dob = "Idade não é válida";
            return errors;
          }}
          initialValues={initialValues}
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
                  name="name"
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
                <Field
                  name="repetePass"
                  component={TextInput}
                  fluid
                  icon="lock"
                  iconPosition="left"
                  placeholder="Repita a Password"
                  type="password"
                  validate={composeValidators(required, minLength(6))}
                />
                <Field
                  fluid
                  name="language"
                  component={SelectInput}
                  options={Languages}
                  placeholder={Languages[0].text}
                  validate={composeValidators(required)}
                />
                <Field
                  name="NIF"
                  component={TextInput}
                  fluid
                  icon="lock"
                  iconPosition="left"
                  placeholder="NIF"
                  validate={composeValidators(required, exactLength(9))}
                />
                <Segment>
                  <DatePicker
                    selected={DateValue}
                    onChange={(date) => date && setDateValue(date)}
                  />
                </Segment>
                <Segment>
                  <Checkbox
                    name="notif"
                    label="Deseja ser notificado (por email)?"
                    checked={checkbox === true}
                    onChange={() => {
                      if (checkbox === true) {
                        setCheckbox(false);
                      } else {
                        setCheckbox(true);
                      }
                    }}
                  />
                </Segment>
                {submitError && !dirtySinceLastSubmit && (
                  <ErrorMessage
                    error={submitError}
                    text="Parâmetros incorretos!"
                  />
                )}
                <Button
                  color="green"
                  fluid
                  size="large"
                  loading={submitting}
                  disabled={invalid && !dirtySinceLastSubmit}
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
