import { Fragment } from "react";
import { observer } from "mobx-react-lite";
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
import ChangePass from "../../features/ChangePass";
import GameDetails from "../../features/appUser/GameDetails/GameDetails";
import NavBar from "../../features/appUser/NavBar/NavBar";
import ModalContainer from "../common/ModalContainer";
import PrivateRoute from "./PrivateRoute";
import AppUserDashboard from "../../features/appUser/Homepage/AppUserDashboard";
import AppUserLogin from "../../features/appUser/AppUserLogin";

const App: React.FC<RouteComponentProps> = () => {
  // const rootStore = useContext(RootStoreContext);
  // const { setAppLoaded, appLoaded, token } = rootStore.commonStore;
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
      <ModalContainer />
      <ToastContainer position="bottom-right" />
      <Route exact path="/" component={AppUserLogin} />
      <Route
        path={"/(.+)"}
        render={() => (
          <Fragment>
            <NavBar />
            <Container fluid style={{ margin: "6em", paddingLeft: '10em' }}>
              <Switch>
                <Route exact path="/admin" component={AppUserLogin} />
                <Route exact path="/specialist" component={AppUserLogin} />
                <Route exact path="/register" component={RegisterForm} />
                <Route exact path="/changePass" component={ChangePass} />
                <PrivateRoute
                  exact
                  path="/user/homepage/:id"
                  component={AppUserDashboard}
                  roles={["AppUser"]}
                />
                <PrivateRoute
                  exact
                  path="/user/game/details/:id"
                  component={GameDetails}
                  roles={["AppUser"]}
                />
                <Route path="/*" component={NotFound} />
              </Switch>
            </Container>
          </Fragment>
        )}
      />
    </Fragment>
  );
};

export default withRouter(observer(App));
