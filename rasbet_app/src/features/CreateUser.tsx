import { observer } from "mobx-react-lite";
import React, { useContext, useState } from "react";
import { Button, Form, Grid, Header, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../app/stores/rootStore";
import { Field, Form as FinalForm } from "react-final-form";
import { IUserRegister, UserRegisterFormValues } from "../app/models/user";
import { FORM_ERROR } from "final-form";
import TextInput from "../app/common/TextInput";
import {
  composeValidators,
  isEmail,
  minLength,
  required,
} from "../app/common/Validators";
import SelectInput from "../app/common/SelectInput";
import { Languages } from "../app/common/Languages";
import ErrorMessage from "../app/common/ErrorMessage";

interface IErrors {
  repetePass?: string;
}

interface IProps {
  userType: string;
}

const CreateUser: React.FC<IProps> = ({ userType }) => {
  const rootStore = useContext(RootStoreContext);
  const { createAdmin, createSpecialist } = rootStore.userStore;
  const { closeModal } = rootStore.modalStore;

  const [initialValues] = useState(new UserRegisterFormValues());

  return (
    <Grid textAlign="center" verticalAlign="middle">
      <Grid.Column>
        <Header as="h2" color="black" textAlign="center">
          Registo
        </Header>
        <FinalForm
          onSubmit={(values: IUserRegister) => {
            if (userType == "specialist") {
              createSpecialist(values).catch((error) => ({
                [FORM_ERROR]: error,
              }));
            } else {
              createAdmin(values).catch((error) => ({
                [FORM_ERROR]: error,
              }));
            }
            closeModal();
          }}
          // validate={(values) => {
          //   const errors: IErrors = {};
          //   if (values.password !== values.repetePass)
          //     errors.repetePass = "Não coincidem!";

          //   return errors;
          // }}
          initialValues={initialValues}
          render={({
            handleSubmit,
            submitError,
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
                {submitError && !dirtySinceLastSubmit && (
                  <ErrorMessage
                    error={submitError}
                    text="Parâmetros incorretos!"
                  />
                )}
                <Button color="green" fluid size="large" type="submit">
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

export default observer(CreateUser);
