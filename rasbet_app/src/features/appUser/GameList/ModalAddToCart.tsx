import { observer } from "mobx-react-lite";
import { useContext, useEffect, useState } from "react";
import {
  Button,
  Checkbox,
  Form,
  Grid,
  GridColumn,
  Header,
} from "semantic-ui-react";
import { IActiveGame } from "../../../app/models/game";
import { IOdd } from "../../../app/models/odd";
import { RootStoreContext } from "../../../app/stores/rootStore";
import { Field, Form as FinalForm } from "react-final-form";
import TextInput from "../../../app/common/TextInput";
import {
  composeValidators,
  isNumber,
  required,
} from "../../../app/common/Validators";
import { ValidationErrors } from "final-form";

interface IProps {
  game: IActiveGame;
  odd: IOdd;
}

const ModalAddToCart: React.FC<IProps> = ({ game, odd }) => {
  const rootStore = useContext(RootStoreContext);
  const { user } = rootStore.userStore;
  const { addBetSimple, addMultipleSelection } = rootStore.betStore;
  const { closeModal } = rootStore.modalStore;

  const [checkbox, setCheckbox] = useState(false);

  useEffect(() => {}, [checkbox]);

  return (
    <Grid padded>
      <Grid.Row columns={1}>
        <Grid.Column>
          <Header as="h3">Como deseja efetuar a sua aposta?</Header>
          (Por defeito é adicionada como parte de uma aopsta múltipla.)
        </Grid.Column>
      </Grid.Row>
      <Grid.Row>
        <GridColumn>
          <Checkbox
            label="Adicionar como aposta simples"
            checked={checkbox === true}
            onChange={() => {
              setCheckbox(!checkbox);
            }}
          />
        </GridColumn>
      </Grid.Row>
      {checkbox === true ? (
        <Grid.Row>
          <FinalForm
            onSubmit={(montante: number) => {
              addBetSimple(
                montante,
                user!.id,
                game.game.mainBet,
                odd.value,
                odd,
                game.game
              );
              closeModal();
            }}
            render={({ handleSubmit }) => (
              <Form onSubmit={handleSubmit}>
                <Field
                  name="montante"
                  fluid
                  component={TextInput}
                  placeholder="Montante desejado"
                  validate={composeValidators(required, isNumber)}
                />
                <Button type="submit" positive>
                  Confirmar aposta!
                </Button>
              </Form>
            )}
          />
        </Grid.Row>
      ) : (
        <Grid.Row>
          <Button
            type="submit"
            positive
            onClick={() => {
              addMultipleSelection(game.game.mainBet, odd.id, odd, game.game);
              closeModal();
            }}
          >
            Confirmar aposta!
          </Button>
        </Grid.Row>
      )}
    </Grid>
  );
};

export default observer(ModalAddToCart);
