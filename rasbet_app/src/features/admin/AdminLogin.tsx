import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { RootStoreContext } from "../../app/stores/rootStore";
import LoginForm from "../LoginForm";

const AdminLogin = () => {
  const rootStore = useContext(RootStoreContext);
  const { login, submitting, loading } = rootStore.userStore;

  return (
    <LoginForm
      loginFunc={login}
      submitting={submitting}
      loading={loading}
      isAppUser={false}
    />
  );
};

export default observer(AdminLogin);
