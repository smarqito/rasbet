import React, { useContext, useEffect } from "react";
import { observer } from "mobx-react-lite";
import { RouteComponentProps } from "react-router-dom";
import { Button, Divider, Grid, Header, Segment, Tab } from "semantic-ui-react";
import { RootStoreContext } from "../app/stores/rootStore";
import UpdateSpecialistAdmin from "./UpdateSpecialistAdmin";
import UpdateSASensitive from "./UpdateSASensitive";

interface DetailsParams {
  id: string;
}

const ASProfile: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history,
}) => {
  const rootStore = useContext(RootStoreContext);
  const { user, getAdmin } = rootStore.userStore;
  const { openModal } = rootStore.modalStore;

  useEffect(() => {
    getAdmin(match.params.id);
  }, [match.params.id]);

  const panes = [
    {
      menuItem: "Preferências de Utilizador",
      render: () => (
        <Tab.Pane>
          <Grid padded>
            <Grid.Row columns={2} textAlign="center">
              <Grid.Column>
                <Header as="h4">Nome: </Header>
                <Segment tertiary>
                  {/* {user.name} */}
                  Marco António
                </Segment>
              </Grid.Column>
              <Grid.Column>
                <Grid>
                  <Grid.Row columns={1} textAlign="center">
                    <Grid.Column>
                      <Header as="h4"> Language:</Header>
                      <Segment tertiary>
                        {/* {user.language} */}
                        Português
                      </Segment>
                    </Grid.Column>
                  </Grid.Row>
                </Grid>
              </Grid.Column>
            </Grid.Row>
            <Divider section />
            <Grid.Row columns={1}>
              <Grid.Column>
                <Button
                  fluid
                  content="Alterar o perfil"
                  color="twitter"
                  type="button"
                  onClick={() => openModal(<UpdateSpecialistAdmin />)}
                />
              </Grid.Column>
            </Grid.Row>
            <Grid.Row columns={1}>
              <Grid.Column>
                <Button
                  fluid
                  content="Alterar informações sensíveis (Palavra-Passe)"
                  color="twitter"
                  type="button"
                  onClick={() => openModal(<UpdateSASensitive />)}
                />
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Tab.Pane>
      ),
    },
  ];

  return (
    <Tab
      menu={{ fluid: true, vertical: true }}
      menuPosition="left"
      panes={panes}
    />
  );
};

export default observer(ASProfile);
