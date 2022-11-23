import React, { Fragment, useContext } from "react";
import { RootStoreContext } from "../stores/rootStore";

interface IProps {
  role: string;
  component: any;
}

const IsAuthorized: React.FC<IProps> = ({ role, component: Component }) => {
  const rootStore = useContext(RootStoreContext);
  const { hasRole } = rootStore.userStore;

  return !hasRole(role) ? null : <Fragment>{Component}</Fragment>;
};

export default IsAuthorized;
