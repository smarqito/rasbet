import { Fragment } from "react";
import { observer } from "mobx-react-lite";
import { Route, RouteComponentProps, withRouter } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import LoginForm from "../../features/user/LoginForm";

const App: React.FC<RouteComponentProps> = () => {
  return (
    <Fragment>
      <ToastContainer position="bottom-right" />
      <Route exact path="/" component={LoginForm} />
    </Fragment>
  );
};

export default withRouter(observer(App));