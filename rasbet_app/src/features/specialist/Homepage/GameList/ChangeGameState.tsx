import { observer } from "mobx-react-lite";
import React, { Fragment, useContext, useState } from "react";
import {
  Button,
  Checkbox,
  Dropdown,
  Grid,
  GridColumn,
  Header,
  Input,
} from "semantic-ui-react";
import { RootStoreContext } from "../../../../app/stores/rootStore";

const ChangeGameState: React.FC = () => {
  const rootStore = useContext(RootStoreContext);
  const { finishGame, suspendGame, activateGame, game } = rootStore.gameStore;
  const { user } = rootStore.userStore;
  const { closeModal } = rootStore.modalStore;

  const [state, setState] = useState<string | number | undefined>("activate");

  const [result, setResult] = useState<
    string | number | boolean | (string | number | boolean)[] | undefined
  >("home");

  const resultValues = [
    { key: 1, text: "home", value: "home" },
    { key: 2, text: "away", value: "away" },
    { key: 2, text: "Draw", value: "draw" },
  ];

  return (
    <Grid padded>
      <Grid.Row textAlign="center" columns={1}>
        <Grid.Column>
          <Header as="h3">Alterar estado do jogo!</Header>
        </Grid.Column>
      </Grid.Row>
      <Grid.Row columns={1}>
        <Grid.Column>
          <Checkbox
            label="Fechar"
            value="close"
            checked={state === "close"}
            onChange={(_, data) => setState(data.value)}
          />
        </Grid.Column>
      </Grid.Row>
      <Grid.Row columns={1}>
        <Grid.Column>
          <Checkbox
            label="Suspender"
            value="suspend"
            checked={state === "suspend"}
            onChange={(_, data) => setState(data.value)}
          />
        </Grid.Column>
      </Grid.Row>
      <Grid.Row columns={1}>
        <Grid.Column>
          <Checkbox
            label="Ativar"
            value="activate"
            checked={state === "activate"}
            onChange={(_, data) => setState(data.value)}
          />
        </Grid.Column>
      </Grid.Row>
      {state === "close" && (
        <Grid.Row>
          <Dropdown
            options={resultValues}
            selection
            value={result}
            onChange={(_, data) => {
              setResult(data.value);
            }}
          />
        </Grid.Row>
      )}
      <Grid.Row>
        <Button
          content="Concluir"
          fluid
          color="green"
          onClick={() => {
            if (state === "activate") activateGame(game!.id, user!.id);
            if (state === "suspend") suspendGame(game!.id, user!.id);
            // if (state === "close") finishGame(game!.id, resultValues['text' == result].value, user!.id);

            closeModal();
          }}
        />
      </Grid.Row>
    </Grid>
  );
};

export default observer(ChangeGameState);
