import React from "react";
import { Button, Card, Grid, Header } from "semantic-ui-react";
import { IActiveGame, IGame } from "../../../app/models/game";
import { formatRelative } from "date-fns";
import { pt } from "date-fns/locale";
import { NavLink } from "react-router-dom";
interface IProps {
  id: number;
  game: IActiveGame;
}
const GameListItem: React.FC<IProps> = ({ id, game }) => {
  return (
    <Card fluid key={id} as={NavLink} to={`/user/game/details/${id}`}>
      <Grid padded>
        <Grid.Row columns={5} verticalAlign="middle">
          <Grid.Column stretched width={7}>
            <Header as={"h3"}>{game.name}</Header>
            {formatRelative(game.start, new Date(), { locale: pt })}
          </Grid.Column>
          {game.mainBet.odds.map((odd) => {
            return (
              <Grid.Column key={odd.id} width={3} textAlign="center">
                <Button color="olive" type="submit">
                  {odd.name} <p /> {odd.value}€
                </Button>
              </Grid.Column>
            );
          })}
        </Grid.Row>
      </Grid>
    </Card>
  );
};

export default GameListItem;
