import { observer } from "mobx-react-lite";
import React, { useContext, useState } from "react";
import { Button, Grid, Header, Input } from "semantic-ui-react";
import { IBetType } from "../../../../app/models/betType";
import { ChangeOddValues, IOdd } from "../../../../app/models/odd";
import { RootStoreContext } from "../../../../app/stores/rootStore";

interface IProps {
  odd: IOdd;
  bet: IBetType;
}

const ChangeOdd: React.FC<IProps> = ({ odd, bet }) => {
  const rootStore = useContext(RootStoreContext);
  const { closeModal } = rootStore.modalStore;
  const { changeOdds } = rootStore.gameStore;
  const { user } = rootStore.userStore;

  const handleChange = (oddValue: number) => {
    setOddValue(oddValue);
  };

  const [initialValues] = useState(new ChangeOddValues());
  const [oddValue, setOddValue] = useState(odd.oddValue);

  return (
    <Grid padded centered>
      <Grid.Row columns={1}>
        <Header as="h3">Insira o valor desejado da odd!</Header>
      </Grid.Row>
      <Grid.Row columns={1}>
        <Input
          onChange={(_, data) =>
            handleChange(parseFloat(data.value.length > 0 ? data.value : "0"))
          }
          value={oddValue}
        />
      </Grid.Row>
      <p />
      <Grid.Row columns={1}>
        <Button
          color="green"
          fluid
          content="Concluir"
          onClick={() => {
            initialValues.specialistId = user!.id;
            initialValues.betTypeId = bet.id;
            initialValues.newOdds.set(odd.id, odd.oddValue);
            changeOdds(initialValues);
            closeModal();
          }}
        />
      </Grid.Row>
    </Grid>
  );
};

export default observer(ChangeOdd);
