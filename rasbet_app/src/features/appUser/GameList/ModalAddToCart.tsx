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
import { FormApi, FORM_ERROR, SubmissionErrors } from "final-form";
import { error } from "console";
import TextInput from "../../../app/common/TextInput";
import { required } from "../../../app/common/Validators";

interface IProps {
  game: IActiveGame;
  odd: IOdd;
}

const ModalAddToCart: React.FC<IProps> = ({ game, odd }) => {
  const rootStore = useContext(RootStoreContext);
  const { user } = rootStore.userStore;
  const { addBetSimple, addMultipleSelection } = rootStore.betStore;

  const [checkbox, setCheckbox] = useState(false);

  useEffect(() => {}, [checkbox]);

  return (
    <Grid>
      <Grid.Row>
        <Grid.Column>
          <Header as="h3">Como deseja efetuar a sua aposta?</Header>
          <Header as="h5">
            (Por defeito é adicionada como parte de uma aopsta múltipla.)
          </Header>
        </Grid.Column>
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
            }}
            // validate={(montante) => {
            //   var error: string = "";

            //   if (montante < 0.5) error = "Montante inferior ao necessário";

            //   return error;
            // }}
            render={({ handleSubmit }) => (
              <Form onSubmit={handleSubmit}>
                <Field
                  name="montante"
                  fluid
                  component={TextInput}
                  placeholder="Montante desejado"
                  validate={required}
                />
                <Button type="submit" fluid>
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
            fluid
            onClick={() =>
              addMultipleSelection(
                game.game.mainBet,
                odd.id,
                odd,
                game.game
              )
            }
          >
            Confirmar aposta!
          </Button>
        </Grid.Row>
      )}
    </Grid>
  );
};

export default observer(ModalAddToCart);
