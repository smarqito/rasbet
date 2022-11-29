import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Button, Card } from "semantic-ui-react";
import { IBet, ISelection, ISimpleDetails } from "../../../app/models/bet";
import { RootStoreContext } from "../../../app/stores/rootStore";

interface IProps {
  selection: ISimpleDetails;
}

const BetCartSimpleElement: React.FC<IProps> = ({ selection }) => {
  const rootStore = useContext(RootStoreContext);
  const { getGanhosSimple, removeSimpleSelection } = rootStore.betStore;

  return (
    <Card>
      <Card.Content>
        <Card.Header>
          {selection.selection.game.home} - {selection.selection.game.away}
        </Card.Header>
        <Card.Meta>{selection.selection.betType.type}</Card.Meta>
        <Card.Description>
          cota:{selection.selection.oddValue} Montante:{selection.amount}€{" "}
          Ganhos:{getGanhosSimple()}
        </Card.Description>
      </Card.Content>
      <Card.Content extra>
        <Button
          basic
          color="red"
          onClick={() => removeSimpleSelection(selection.selection.odd.id)}
        >
          Remover
        </Button>
      </Card.Content>
    </Card>
  );
};

export default observer(BetCartSimpleElement);
