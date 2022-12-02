import { Card } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import { IActiveGame } from "../../../../app/models/game";
import ListItemNotFound from "../../../../app/common/ListItemNotFound";
import { Fragment } from "react";
import SpecialistGameItem from "./SpecialistGameItem";

interface IProps {
  games: IActiveGame[];
  loading: boolean;
}

const GameList: React.FC<IProps> = ({ games, loading }) => {
  function hasElements() {
    return games.length != 0;
  }

  return (
    <Fragment>
      {hasElements() ? (
        <Card.Group>
          {games.map((x) => (
            <SpecialistGameItem key={x.game.id} id={x.game.id} game={x} />
          ))}
        </Card.Group>
      ) : (
        <ListItemNotFound content="Não existem jogos ativos na categoria!" />
      )}
    </Fragment>
  );
};

export default observer(GameList);
