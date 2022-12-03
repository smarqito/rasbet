import { observer } from "mobx-react-lite";
import React, { useContext, useState } from "react";
import { Button, Card, Input } from "semantic-ui-react";
import { ISimpleDetails } from "../../../app/models/bet";
import { RootStoreContext } from "../../../app/stores/rootStore";

interface IProps {
  selection: ISimpleDetails;
}

const BetCartSimpleElement: React.FC<IProps> = ({ selection }) => {
  const rootStore = useContext(RootStoreContext);
  const { getGanhosSimple, removeSimpleSelection } = rootStore.betStore;
  const [amountV, setAmountV] = useState(selection.amount);

  const handleChange = (amount: number) => {
    setAmountV(amount);
    selection.amount = amount.valueOf();
  };
  return (
    <Card>
      <Card.Content>
        <Card.Header>
          {selection.selection.game.homeTeam} - {selection.selection.game.awayTeam}
        </Card.Header>
        <Card.Meta>{selection.selection.odd.name}</Card.Meta>
        <Card.Description>
          <b>Cota</b>: {selection.selection.oddValue.toFixed(2)}
          <p />
          <b>Montante</b>:{" "}
          <Input
            onChange={(_, data) =>
              handleChange(parseInt(data.value.length > 0 ? data.value : "0"))
            }
            value={amountV}
          />
          <p />
          <b>Ganhos</b>: {selection.amount} x {selection.selection.oddValue.toFixed(2)} ={" "}
          {(selection.amount * selection.selection.oddValue).toFixed(2)}
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
