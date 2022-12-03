import { Card } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import GameListItem from "./GameListItem";
import { ISport } from "../../../app/models/game";
import ListItemNotFound from "../../../app/common/ListItemNotFound";
import { Fragment, useCallback, useContext, useEffect } from "react";
import { RootStoreContext } from "../../../app/stores/rootStore";

const GameList: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const {
    getActiveGames,
    getActiveGamesBySport,
    clearActive,
  } = rootStore.gameStore;

  useEffect(() => {
    getActiveGames();
    return () => {
      clearActive();
    };
  }, [getActiveGames]);

  return (
    <Fragment>
      {getActiveGamesBySport.length > 0 ? (
        <Card.Group>
          {getActiveGamesBySport.map((x) => (
            <GameListItem key={x.id} game={x} />
          ))}
        </Card.Group>
      ) : (
        <ListItemNotFound content="Não existem jogos ativos na categoria!" />
      )}
    </Fragment>
  );
};

export default observer(GameList);
