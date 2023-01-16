import { Card, Icon } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import GameListItem from "./GameListItem";
import ListItemNotFound from "../../../app/common/ListItemNotFound";
import { Fragment, useContext, useEffect, useState } from "react";
import { RootStoreContext } from "../../../app/stores/rootStore";

const GameList: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    getActiveGames,
    getActiveGamesBySport,
    getSubbedGames,
    isGameSubbed,
    clearActive,
    clearSubbed,
  } = rootStore.gameStore;

  const { user } = rootStore.userStore;

  useEffect(() => {
    getActiveGames();
    getSubbedGames(user!.id);
    return () => {
      clearActive();
      clearSubbed();
    };
  }, [getActiveGames, getSubbedGames]);

  return (
    <Fragment>
      {getActiveGamesBySport.length > 0 ? (
        <Card.Group>
          {getActiveGamesBySport.map((x) => (
            <GameListItem key={x.id} game={x} bell={isGameSubbed(x.id)} />
          ))}
        </Card.Group>
      ) : (
        <ListItemNotFound content="Não existem jogos ativos na categoria!" />
      )}
    </Fragment>
  );
};

export default observer(GameList);
