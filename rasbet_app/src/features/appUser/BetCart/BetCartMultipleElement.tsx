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
          {selection.game.home} - {selection.game.away}
        </Card.Header>
        <Card.Meta>{selection.betType.type}</Card.Meta>
        <Card.Description>cota:{selection.oddValue}</Card.Description>
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
