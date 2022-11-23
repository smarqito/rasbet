import { Fragment } from "react";
import { observer } from "mobx-react-lite";
import {
  Route,
  RouteComponentProps,
  Switch,
  withRouter,
} from "react-router-dom";
import { ToastContainer } from "react-toastify";
import LoginForm from "../../features/user/LoginForm";
import RegisterForm from "../../features/user/RegisterForm";
import ModalContainer from "../common/ModalContainer";
import NotFound from "./NotFound";
import { Container } from "semantic-ui-react";
import PrivateRoute from "./PrivateRoute";

const App: React.FC<RouteComponentProps> = () => {
  return (
    <Fragment>
      <ModalContainer />
      <ToastContainer position="bottom-right" />
      <Route exact path="/" component={LoginForm} />
      <Route exact path="/registo" component={RegisterForm} />
      <Route
        path={"/(.+)"}
        render={() => (
          <Fragment>
            <Container style={{ marginTop: "7em" }}>
              <Switch>
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
