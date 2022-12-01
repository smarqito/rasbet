import { observer } from "mobx-react-lite";
import { RouteComponentProps } from "react-router-dom";
import { Grid, Header, Segment, Tab } from "semantic-ui-react";
import { Form as FinalForm } from "react-final-form";
import { IAppUserRegister, IUserRegister } from "../../../app/models/user";
import { useContext, useEffect, useState } from "react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import { FORM_ERROR } from "final-form";
import CreateUser from "../../CreateUser";

interface DetailsParams {
  id: string;
}

const CreationMenu: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history,
}) => {
  useEffect(() => {}, [match.params.id]);

  const panes = [
    {
      menuItem: "Especialista",
      render: () => (
        <Tab.Pane attached={false}>
          <Segment padded secondary>
            <CreateUser userType="specialist" />{" "}
          </Segment>
        </Tab.Pane>
      ),
    },
    {
      menuItem: "Administrador",
      render: () => (
        <Tab.Pane attached={false}>
          <Segment padded secondary>
            <CreateUser userType="admin" />
          </Segment>
        </Tab.Pane>
      ),
    },
  ];

  return (
    <Tab menu={{ fluid: true, vertical: true, tabular: true }} panes={panes} />
  );
};

export default observer(CreationMenu);
