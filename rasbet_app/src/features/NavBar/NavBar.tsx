import React, { Fragment, useContext, useEffect } from "react";
import { NavLink } from "react-router-dom";
import {
  Container,
  Dropdown,
  Grid,
  Image,
  Menu,
} from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import IsAuthorized from "../../app/common/IsAuthorized";
import { observer } from "mobx-react-lite";

const NavBar: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { user, logout } = rootStore.userStore;
  const { wallet, getWallet, depositFunds, withdrawFunds } =
    rootStore.walletStore;

  useEffect(() => {
    if (user?.role === "AppUser") {
      getWallet(user.id);
    }
  }, [depositFunds, withdrawFunds]);

  return (
    <div>
      {user && (
        <Menu fixed="top" inverted color="green" stackable>
          <Container>
            <Menu.Item>
              <Grid>
                <Grid.Row columns={1}>
                  <Grid.Column>
                    <Image src="/assets/rasbet_logo.png" size="tiny" />
                  </Grid.Column>
                </Grid.Row>
              </Grid>
            </Menu.Item>
            <IsAuthorized
              component={
                <Menu.Item
                  header
                  as={NavLink}
                  to={`/user/homepage/${user.name}`}
                >
                  Desporto
                </Menu.Item>
              }
              roles={["AppUser"]}
            />
            <IsAuthorized
              component={
                <Menu.Item
                  header
                  as={NavLink}
                  to={`/specialist/homepage/${user.name}`}
                >
                  Desporto
                </Menu.Item>
              }
              roles={["Specialist"]}
            />
            <IsAuthorized
              component={
                <Menu.Item
                  header
                  as={NavLink}
                  to={`/admin/homepage/${user.name}`}
                >
                  Criação
                </Menu.Item>
              }
              roles={["Admin"]}
            />
            <Menu.Item position="right">
              {user.role === "AppUser" ? (
                <Fragment>
                  <b>({wallet?.balance} €)</b>
                  <Dropdown
                    pointing="top left"
                    text={`Bem Vindo, ${user.name}`}
                  >
                    <Dropdown.Menu>
                      <IsAuthorized
                        component={
                          <Dropdown.Item
                            as={NavLink}
                            to={`/user/profile/${user.id}`}
                            text="Meu perfil"
                            icon="user"
                          />
                        }
                        roles={["AppUser"]}
                      />
                      <Dropdown.Item
                        onClick={logout}
                        text="Logout"
                        icon="power"
                      />
                    </Dropdown.Menu>
                  </Dropdown>
                </Fragment>
              ) : (
                <Dropdown pointing="top left" text={`Bem Vindo, ${user.name}`}>
                  <Dropdown.Menu>
                    <IsAuthorized
                      component={
                        <Dropdown.Item
                          as={NavLink}
                          to={`/admin/profile/${user.id}`}
                          text="Meu perfil"
                          icon="user"
                        />
                      }
                      roles={["Admin"]}
                    />
                    <IsAuthorized
                      component={
                        <Dropdown.Item
                          as={NavLink}
                          to={`/specialist/profile/${user.id}`}
                          text="Meu perfil"
                          icon="user"
                        />
                      }
                      roles={["Specialist"]}
                    />
                    <Dropdown.Item
                      onClick={logout}
                      text="Logout"
                      icon="power"
                    />
                  </Dropdown.Menu>
                </Dropdown>
              )}
            </Menu.Item>
          </Container>
        </Menu>
      )}
    </div>
  );
};

export default observer(NavBar);
