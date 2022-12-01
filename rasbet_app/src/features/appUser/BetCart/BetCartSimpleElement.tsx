import { observer } from "mobx-react-lite";
import React, { useContext } from "react";
import { Button, Card } from "semantic-ui-react";
import { ISimpleDetails } from "../../../app/models/bet";
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
        <Card.Meta>{selection.selection.odd.name}</Card.Meta>
        <Card.Description>
          <b>Cota</b>: {selection.selection.oddValue}<p />
          <b>Montante</b>: {selection.amount}€ <p />
          <b>Ganhos</b>: {getGanhosSimple()}
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
