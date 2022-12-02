import React from "react";
import { Card } from "semantic-ui-react";
import { IBetType } from "../../../app/models/betType";
import BetTypeListItem from "./BetTypeListItem";

interface IProps {
  betTypes: IBetType[];
}

const BetTypeList: React.FC<IProps> = ({ betTypes }) => {
  return (
    <Card.Group>
      {betTypes.map((x) => (
        <BetTypeListItem id={x.id} betType={x} />
      ))}
    </Card.Group>
  );
};

export default BetTypeList;
