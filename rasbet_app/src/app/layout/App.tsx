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
import "react-toastify/dist/ReactToastify.min.css";
import ChangePass from "../../features/ChangePass";
import GameDetails from "../../features/appUser/GameDetails/GameDetails";
import NavBar from "../../features/NavBar/NavBar";
import ModalContainer from "../common/ModalContainer";
import PrivateRoute from "./PrivateRoute";
import AppUserDashboard from "../../features/appUser/Homepage/AppUserDashboard";
import AppUserLogin from "../../features/appUser/AppUserLogin";
import AppUserProfile from "../../features/appUser/Profile/AppUserProfile";
import AdminLogin from "../../features/admin/AdminLogin";
import SpecialistLogin from "../../features/specialist/SpecialistLogin";
import CreationMenu from "../../features/admin/Homepage/CreationMenu";
import ASProfile from "../../features/ASProfile";
import SpecialistHomepage from "../../features/specialist/Homepage/SpecialistHomepage";

const App: React.FC<RouteComponentProps> = () => {
  return (
    <div>
      <ModalContainer />
      <ToastContainer position="bottom-right" />
      <Switch>
        <Route exact path="/" component={AppUserLogin} />
        <Route exact path="/admin" component={AdminLogin} />
        <Route exact path="/specialist" component={SpecialistLogin} />
        <Route exact path="/register" component={RegisterForm} />
        <Route exact path="/changePass" component={ChangePass} />
        <Route
          path={"/(user|admin|specialist)/(.+)"}
          render={() => (
            <Fragment>
              <NavBar />
              <Container
                fluid
                style={{
                  margin: "6em",
                  paddingLeft: "3em",
                  paddingRight: "3em",
                }}
              >
                <Switch>
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
                  <PrivateRoute
                    exact
                    path="/user/profile/:id"
                    component={AppUserProfile}
                    roles={["AppUser"]}
                  />
                  <PrivateRoute
                    exact
                    path="/admin/homepage/:id"
                    component={CreationMenu}
                    roles={["Admin"]}
                  />
                  <PrivateRoute
                    exact
                    path="/admin/profile/:id"
                    component={ASProfile}
                    roles={["Admin"]}
                  />
                  <PrivateRoute
                    exact
                    path="/specialist/profile/:id"
                    component={ASProfile}
                    roles={["Specialist"]}
                  />
                  <PrivateRoute
                    exact
                    path="/specialist/homepage/:id"
                    component={SpecialistHomepage}
                    roles={["Specialist"]}
                  />
                  <Route
                    path="/(user|admin|specialist)/*"
                    component={NotFound}
                  />
                </Switch>
              </Container>
            </Fragment>
          )}
        />
        <Route path="/*" component={NotFound} />
      </Switch>
    </div>
  );
};

export default withRouter(observer(App));
