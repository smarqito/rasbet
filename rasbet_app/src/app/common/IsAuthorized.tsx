import React, { Fragment, useContext } from "react";
import { RootStoreContext } from "../stores/rootStore";

interface IProps {
  roles: string[];
  component: any;
}

const IsAuthorized: React.FC<IProps> = ({ roles, component: Component }) => {
  const rootStore = useContext(RootStoreContext);
  const { hasRole } = rootStore.userStore;

  return !hasRole(roles) ? null : <Fragment>{Component}</Fragment>;
};

export default IsAuthorized;
