import { Fragment, useContext, useEffect } from "react";
import { observer } from "mobx-react-lite";
import ModalContainer from "../common/ModalContainer";
import { ToastContainer } from "react-toastify";
import {
  Route,
  RouteComponentProps,
  Switch,
  withRouter,
} from "react-router-dom";
import NotFound from "./NotFound";
import { Container } from "semantic-ui-react";
import RegisterForm from "../../features/appUser/RegisterForm";
import LoginForm from "../../features/LoginForm";
import "react-toastify/dist/ReactToastify.min.css";
import { RootStoreContext } from "../stores/rootStore";
import LoadingComponent from "./LoadingComponent";
import ChangePass from "../../features/ChangePass";
import { BetDashboard } from "../../features/appUser/betDashboard/BetDashboard";

const App: React.FC<RouteComponentProps> = () => {
  const rootStore = useContext(RootStoreContext);
  const { setAppLoaded, appLoaded, token } = rootStore.commonStore;
  // const { } = rootStore.userStore;

  // useEffect(() => {
  //   if (token) {
  //     getUser().finally(() => {
  //       setAppLoaded();
  //     });
  //   } else {
  //     setAppLoaded();
  //   }
  // }, [getUser, setAppLoaded, token]);

  // if (!appLoaded) {
  //   return <LoadingComponent content="Loading app" />;
  // }
  return (
    <Fragment>
      <ToastContainer position="bottom-right" />
      <Route exact path="/" component={LoginForm} />
      <Route exact path="/registo" component={RegisterForm} />
      <Route
        path={"/(.+)"}
        render={() => (
          <Fragment>
            <Container style={{ marginTop: "7em" }}>
              <Switch>
                <Route exact path="/alterarPasse" component={ChangePass} />
                <Route exact path="/user/homepage" component={BetDashboard} />
                <Route component={NotFound} />
              </Switch>
            </Container>
          </Fragment>
        )}
      />
    </Fragment>
  );
};

export default withRouter(observer(App));
