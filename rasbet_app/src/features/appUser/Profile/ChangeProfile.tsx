import { observer } from "mobx-react-lite";
import React, { useContext, useState } from "react";
import {
  Button,
  Checkbox,
  Form,
  Grid,
  Header,
  Segment,
} from "semantic-ui-react";
import { Field, Form as FinalForm } from "react-final-form";
import { FORM_ERROR } from "final-form";
import { RootStoreContext } from "../../../app/stores/rootStore";
import TextInput from "../../../app/common/TextInput";
import SelectInput from "../../../app/common/SelectInput";
import { Languages } from "../../../app/common/Languages";
import { Coin } from "../../../app/common/Coin";
import {
  AppUserChangeProfile,
  IAppUserChangeProfile,
} from "../../../app/models/user";
import { required } from "../../../app/common/Validators";

const ChangeProfile: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { user, appUserDetails, updateAppUser } = rootStore.userStore;
  const { closeModal } = rootStore.modalStore;
  const [notif, setNotif] = useState(appUserDetails!.notif);

  const [initialValues] = useState(new AppUserChangeProfile());

  return (
    <Grid textAlign="center" verticalAlign="middle">
      <Grid.Row>
        <Header> Alterar Informações do perfil</Header>
      </Grid.Row>
      <Grid.Row>
        <Grid.Column>
          <FinalForm
            onSubmit={(values: IAppUserChangeProfile) => {
              updateAppUser(
                user!.email,
                values.name,
                values.lang,
                values.coin,
                notif
              ).catch((error) => ({
                [FORM_ERROR]: error,
              }));
              closeModal();
            }}
            initialValues={initialValues}
            render={({ handleSubmit }) => (
              <Form size="large" onSubmit={handleSubmit}>
                <Segment stacked>
                  <Field
                    fluid
                    name="name"
                    component={TextInput}
                    placeholder="Novo nome de utilizador"
                  />
                  <Field
                    name="lang"
                    component={SelectInput}
                    options={Languages}
                    placeholder={Languages[0].text}
                  />
                  <Field
                    name="coin"
                    component={SelectInput}
                    options={Coin}
                    placeholder={Coin[0].text}
                  />
                </Segment>
                <Segment>
                  <Checkbox
                    name="notif"
                    label="Deseja ser notificado (por email)?"
                    checked={notif === true}
                    onChange={() => {
                      setNotif(!notif);
                    }}
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
