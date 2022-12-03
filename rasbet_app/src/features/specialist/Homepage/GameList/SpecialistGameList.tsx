import { Card } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { IActiveGame } from "../../../../app/models/game";
import ListItemNotFound from "../../../../app/common/ListItemNotFound";
import { Fragment, useContext, useEffect } from "react";
import SpecialistGameItem from "./SpecialistGameItem";
import { RootStoreContext } from "../../../../app/stores/rootStore";

const GameList: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { getActiveAndSuspended, getAllGamesBySport, clearAllGames } =
    rootStore.gameStore;

  useEffect(() => {
    getActiveAndSuspended();
    return () => {
      clearAllGames();
    };
  }, [getActiveAndSuspended]);

  return (
    <Fragment>
      {getAllGamesBySport.length > 0 ? (
        <Card.Group>
          {getAllGamesBySport.map((x) => (
            <SpecialistGameItem key={x.id} game={x} />
          ))}
        </Card.Group>
      ) : (
        <ListItemNotFound content="Não existem jogos ativos na categoria!" />
      )}
    </Fragment>
  );
};

export default observer(GameList);
