import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import {
  Redirect,
  Route,
  RouteComponentProps,
  RouteProps,
} from "react-router-dom";
import { Role } from "../models/user";
import { RootStoreContext } from "../stores/rootStore";

interface IProps extends RouteProps {
  role: Role;
  component: React.ComponentType<RouteComponentProps<any>>;
}

const PrivateRoute: React.FC<IProps> = ({
  component: Component,
  role,
  ...rest
}) => {
  const rootStore = useContext(RootStoreContext);
  const { isLoggedIn, hasRole } = rootStore.userStore;

  return (
    <Route
      {...rest}
      render={(props) => {
        if (!isLoggedIn) return <Redirect to={"/"} />;

        if (!hasRole(role)) return <Redirect to={"/"} />;

        return <Component {...props} />;
      }}
    />
  );
};

export default observer(PrivateRoute);
