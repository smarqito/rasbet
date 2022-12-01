import { Button, Form, Grid, Header, Segment } from "semantic-ui-react";
import { IUserChangeSensitive, UserChangeSensitive } from "../app/models/user";
import { RootStoreContext } from "../app/stores/rootStore";
import { Field, Form as FinalForm } from "react-final-form";
import { FORM_ERROR } from "final-form";
import TextInput from "../app/common/TextInput";
import { required } from "../app/common/Validators";
import SensitiveConfirm from "./SensitiveConfirm";
import { useContext, useState } from "react";

const UpdateSASensitive: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { user, updateAdminSensitive, updateSpecialistSensitive } =
    rootStore.userStore;
  const { closeModal, openModal } = rootStore.modalStore;

  const [initialValues] = useState(new UserChangeSensitive());

  return (
    <Grid textAlign="center" verticalAlign="middle">
      <Grid.Row>
        <Header> Alterar Informações do perfil</Header>
      </Grid.Row>
      <Grid.Row>
        <Grid.Column>
          <FinalForm
            onSubmit={(values: IUserChangeSensitive) => {
              if (user?.role == "Admin") {
                updateAdminSensitive(user!.email, values.pass).catch(
                  (error) => ({
                    [FORM_ERROR]: error,
                  })
                );
              } else {
                updateSpecialistSensitive(user!.email, values.pass).catch(
                  (error) => ({
                    [FORM_ERROR]: error,
                  })
                );
              }
              closeModal();
              openModal(<SensitiveConfirm />);
            }}
            initialValues={initialValues}
            render={({ handleSubmit }) => (
              <Form size="large" onSubmit={handleSubmit}>
                <Segment stacked>
                  <Field
                    fluid
                    name="pass"
                    component={TextInput}
                    placeholder="Nova Palavra-Passe"
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

export default UpdateSASensitive;
