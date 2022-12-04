import { observer } from "mobx-react-lite";
import React, { useContext, useEffect } from "react";
import { RouteComponentProps } from "react-router-dom";
import { Tab } from "semantic-ui-react";
import { RootStoreContext } from "../../../app/stores/rootStore";
import AccountPref from "./AccountPref";
import Wallet from "./Wallet";
import HistoryBets from "./HistoryBets";
import HistoryTrans from "./HistoryTrans";

interface DetailsParams {
  id: string;
}

const AppUserProfile: React.FC<RouteComponentProps<DetailsParams>> = ({
  match,
  history,
}) => {
  const rootStore = useContext(RootStoreContext);
  const { getAppUser, clearAppUserDetails, clearBets } = rootStore.userStore;
  const { clearTransaction } = rootStore.walletStore;

  useEffect(() => {
    getAppUser(match.params.id);
    return () => {
      clearAppUserDetails();
      clearBets();
      clearTransaction();
    };
  }, [match.params.id]);

  const panes = [
    {
      menuItem: "Preferências de conta",
      render: () => (
        <Tab.Pane>
          <AccountPref />
        </Tab.Pane>
      ),
    },
    {
      menuItem: "Carteira",
      render: () => (
        <Tab.Pane>
          <Wallet />
        </Tab.Pane>
      ),
    },
    {
      menuItem: "Histórico Apostas",
      render: () => (
        <Tab.Pane>
          <HistoryBets />
        </Tab.Pane>
      ),
    },
    {
      menuItem: "Histórico Transações",
      render: () => (
        <Tab.Pane>
          <HistoryTrans />
        </Tab.Pane>
      ),
    },
  ];

  return (
    <Tab
      menu={{ fluid: true, vertical: true }}
      menuPosition="left"
      panes={panes}
    />
  );
};

export default observer(AppUserProfile);
