import { addDays } from "date-fns";
import { Button, Card, Grid, Header, Segment } from "semantic-ui-react";
import { IBetType } from "../../../app/models/betType";
import { IGame } from "../../../app/models/game";
import { IOdd } from "../../../app/models/odd";
import { IUser } from "../../../app/models/user";
import { ISelection, IBetSimple } from "../../../app/models/bet";
import BetCartElement from "./BetCartElement";
import { createContext, useContext, useState } from "react";

const BetCart = () => {
  // function montanteTotal() {
  //   var res = 0;
  //   [bet1, bet2, bet3].map((x) => {
  //     res += x.amount;
  //   });

  //   return res.toPrecision(4);
  // }
  // function ganhosPossiveis() {
  //   var res = 0;
  //   [bet1, bet2, bet3].map((x) => {
  //     res += x.amount * x.selection.oddValue;
  //   });

  //   return res.toPrecision(4);
  // }

  const [betType, setBetType] = useState("simple");

  return (
    // <Card>
    //   <Segment.Group>
    //     <Segment>
    //       <Header as="h4">A minha aposta (3 seleções)</Header>
    //       <Grid>
    //         <Grid.Row columns={2}>
    //           <Grid.Column>
    //             <Button onClick={() => setBetType("simple")}>Simples</Button>
    //           </Grid.Column>
    //           <Grid.Column>
    //             <Button onClick={() => setBetType("multiple")}>Múltipla</Button>
    //           </Grid.Column>
    //         </Grid.Row>
    //       </Grid>
    //     </Segment>
    //     <Segment>
    //       {betType === "simple" && (
    //         <Card.Group itemsPerRow={1}>
    //           {[bet1, bet2, bet3].map((x) => {
    //             return <BetCartElement bet={x} />;
    //           })}
    //         </Card.Group>
    //       )}
    //       {betType === "multiple" && (
    //         <Card.Group itemsPerRow={1}>
    //           {[bet1, bet2, bet3].map((x) => {
    //             return <BetCartElement bet={x} />;
    //           })}
    //         </Card.Group>
    //       )}
    //     </Segment>
    //     <Segment>
    //       <Header as="h5">Montate total: {montanteTotal()}€</Header>
    //       <Header as="h4">Ganhos Possiveis: {ganhosPossiveis()} </Header>
    //     </Segment>
    //     <Segment>
    //       <Button attached="bottom" positive>
    //         Aposta já
    //       </Button>
    //     </Segment>
    //   </Segment.Group>
    // </Card>
    <></>
  );
};

export default BetCart;
