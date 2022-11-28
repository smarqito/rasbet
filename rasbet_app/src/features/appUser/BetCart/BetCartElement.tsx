import React from "react";
import { Card } from "semantic-ui-react";
import { IBet, IBetSimple } from "../../../app/models/bet";

interface IProps {
  bet: IBetSimple;
}

const BetCartElement: React.FC<IProps> = ({ bet }) => {
  function ganhos(cota: number, montante: number) {
    return cota * montante;
  }
  return (
    // <Card>
    //   <Card.Content>
    //     <Card.Header>
    //       {bet.selection.game.name}: {bet.selection.odd.name}
    //     </Card.Header>
    //     <Card.Meta>{bet.selection.betType.type}</Card.Meta>
    //     <Card.Description>
    //       cota:{bet.selection.oddValue} Montante: {bet.amount} Ganhos:{" "}
    //       {ganhos(bet.amount, bet.selection.oddValue)}
    //     </Card.Description>
    //   </Card.Content>
    // </Card>
    <></>
  );
};

export default BetCartElement;
