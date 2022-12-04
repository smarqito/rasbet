import { observer } from "mobx-react-lite";
import React, { Fragment, useContext, useEffect } from "react";
import {
  Button,
  Divider,
  Grid,
  GridColumn,
  Header,
  Segment,
} from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import ChangeProfile from "./ChangeProfile";
import ChangeSensitive from "./ChangeSensitive";

const AccountPref: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { openModal } = rootStore.modalStore;
  const { appUserDetails } = rootStore.userStore;

  return (
    <Grid padded>
      <Grid.Row columns={2} textAlign="center">
        <Grid.Column>
          <Grid>
            <Grid.Row columns={1}>
              <Grid.Column>
                <Header as="h4">Nome: </Header>
                <Segment tertiary>{appUserDetails?.name}</Segment>
              </Grid.Column>
            </Grid.Row>
            <Grid.Row columns={1}>
              <Grid.Column>
                <Header as="h4">IBAN: </Header>
                <Segment tertiary>
                  {appUserDetails?.iban ? appUserDetails?.iban : "Não definido"}
                </Segment>
              </Grid.Column>
            </Grid.Row>
            <Grid.Row columns={1}>
              <Grid.Column>
                <Header as="h4">Número de Telemóvel: </Header>
                <Segment tertiary>
                  {appUserDetails?.phoneNumber ? appUserDetails!.phoneNumber : "Não definido"}
                </Segment>
              </Grid.Column>
            </Grid.Row>
          </Grid>
        </Grid.Column>
        <Grid.Column>
          <Grid>
            <Grid.Row columns={1} textAlign="center">
              <Grid.Column>
                <Header as="h4"> Idioma:</Header>
                <Segment tertiary>{appUserDetails?.language}</Segment>
              </Grid.Column>
            </Grid.Row>
            <Grid.Row columns={1} textAlign="center">
              <Grid.Column>
                <Header as="h4">Moeda:</Header>
                <Segment tertiary>{appUserDetails?.coin}(€)</Segment>
              </Grid.Column>
            </Grid.Row>
            <Grid.Row columns={1} textAlign="center">
              <Grid.Column>
                <Header as="h4">Notificações:</Header>
                <Segment tertiary>
                  {appUserDetails?.notif == true ? (
                    <Fragment>A receber notificações por email.</Fragment>
                  ) : (
                    <Fragment>Não está a receber notificações.</Fragment>
                  )}
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
            onClick={() => openModal(<ChangeProfile />)}
          />
        </Grid.Column>
      </Grid.Row>
      <Grid.Row columns={1}>
        <Grid.Column>
          <Button
            fluid
            content="Alterar informações sensíveis (Palavra-Passe, IBAN, Número de Telemóvel)"
            color="twitter"
            type="button"
            onClick={() => openModal(<ChangeSensitive />)}
          />
        </Grid.Column>
      </Grid.Row>
    </Grid>
  );
};

export default observer(AccountPref);
