import { observer } from "mobx-react-lite";
import { useContext } from "react";
import { RootStoreContext } from "../../app/stores/rootStore";
import LoginForm from "../LoginForm";

const AppUserLogin = () => {
  const rootStore = useContext(RootStoreContext);
  const { loginAppUser, loading } = rootStore.userStore;

  return (
    <LoginForm
      loginFunc={loginAppUser}
      loading={loading}
      isAppUser={true}
    />
  );
};

export default observer(AppUserLogin);
