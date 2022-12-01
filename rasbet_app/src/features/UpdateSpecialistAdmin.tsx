import { observer } from "mobx-react-lite";
import { Button, Form, Grid, Header, Segment } from "semantic-ui-react";
import { Field, Form as FinalForm } from "react-final-form";
import { IUserChangeProfile, UserChangeProfile } from "../app/models/user";
import { FORM_ERROR } from "final-form";
import { useContext, useState } from "react";
import { RootStoreContext } from "../app/stores/rootStore";
import TextInput from "../app/common/TextInput";
import { required } from "../app/common/Validators";
import SelectInput from "../app/common/SelectInput";
import { Languages } from "../app/common/Languages";

const UpdateSpecialistAdmin: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { closeModal } = rootStore.modalStore;
  const { user, updateAdmin, updateSpecialist } = rootStore.userStore;

  const [initialValues] = useState(new UserChangeProfile());

  return (
    <Grid textAlign="center" verticalAlign="middle">
      <Grid.Row>
        <Header> Alterar Informações do perfil</Header>
      </Grid.Row>
      <Grid.Row>
        <Grid.Column>
          <FinalForm
            onSubmit={(values: IUserChangeProfile) => {
              if (user?.role === "Admin") {
                updateAdmin(user!.email, values.name, values.lang).catch(
                  (error) => ({
                    [FORM_ERROR]: error,
                  })
                );
              } else {
                updateSpecialist(user!.email, values.name, values.lang).catch(
                  (error) => ({
                    [FORM_ERROR]: error,
                  })
                );
              }

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
                    validate={required}
                  />
                  <Field
                    name="lang"
                    component={SelectInput}
                    options={Languages}
                    placeholder={Languages[0].text}
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

export default observer(UpdateSpecialistAdmin);
