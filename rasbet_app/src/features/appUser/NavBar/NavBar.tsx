import React, { useContext } from "react";
import { NavLink } from "react-router-dom";
import { Container, Dropdown, Icon, Menu } from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import IsAuthorized from "../../../app/common/IsAuthorized";
import { observer } from "mobx-react-lite";

const NavBar: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { user, logout } = rootStore.userStore;

  return (
    <div>
      {user && (
        <Menu fixed="top" inverted color="green" stackable>
          <Container>
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
              <Dropdown pointing="top left" text="username">
                <Dropdown.Menu>
                  <IsAuthorized
                    component={
                      <Dropdown.Item
                        as={NavLink}
                        to={`/perfil/${user.name}`}
                        text="Meu perfil"
                        icon="user"
                      />
                    }
                    roles={["Admin", "AppUser", "Specialist"]}
                  />
                  <Dropdown.Item onClick={logout} text="Logout" icon="power" />
                </Dropdown.Menu>
              </Dropdown>
            </Menu.Item>
          </Container>
        </Menu>
      )}
    </div>
  );
};

export default observer(NavBar);
