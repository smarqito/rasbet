import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Button, Card } from "semantic-ui-react";
import { ISelection } from "../../../app/models/bet";
import { RootStoreContext } from "../../../app/stores/rootStore";

interface IProps {
  selection: ISelection;
}

const BetCartMultipleElement: React.FC<IProps> = ({ selection }) => {
  const rootStore = useContext(RootStoreContext);
  const { removeMultipleSelection } = rootStore.betStore;

  return (
    <Card>
      <Card.Content>
        <Card.Header>
          {selection.game.homeTeam} - {selection.game.awayTeam}
        </Card.Header>
        <Card.Meta>{selection.odd.name}</Card.Meta>
        <Card.Description>
          <b>Cota</b>: {selection.oddValue.toFixed(2)}
        </Card.Description>
      </Card.Content>
      <Card.Content extra>
        <Button
          basic
          color="red"
          onClick={() => removeMultipleSelection(selection.odd.id)}
        >
          Remover
        </Button>
      </Card.Content>
    </Card>
  );
};

export default observer(BetCartMultipleElement);
