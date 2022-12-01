import { observer } from "mobx-react-lite";
import { useContext } from "react";
import { RootStoreContext } from "../../app/stores/rootStore";
import LoginForm from "../LoginForm";

const SpecialistLogin = () => {
  const rootStore = useContext(RootStoreContext);
  const { loginSpecialist, loading } = rootStore.userStore;

  return (
    <LoginForm
      loginFunc={loginSpecialist}
      loading={loading}
      isAppUser={false}
    />
  );
};

export default observer(SpecialistLogin);
