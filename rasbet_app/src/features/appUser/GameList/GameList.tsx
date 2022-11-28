import { Card } from "semantic-ui-react";
import { observer } from "mobx-react-lite";
import GameListItem from "./GameListItem";
import { IActiveGame } from "../../../app/models/game";

interface IProps {
  games: IActiveGame[];
}

const GameList: React.FC<IProps> = ({games}) => {
  return (
    <Card.Group>
      {games.map((x) => (
        <GameListItem id={x.id} game={x} />
      ))}
    </Card.Group>
  );
};

export default observer(GameList);
