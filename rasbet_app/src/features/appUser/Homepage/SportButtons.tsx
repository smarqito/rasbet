import React from "react";
import { Button, Grid, Header, Segment } from "semantic-ui-react";
import { sports } from "../../../app/common/Sports";

const SportButtons = () => {
  return (
    <Segment>
      <Header as="h3">Desportos</Header>
      <Grid padded centered>
        {sports.map((x) => {
          return (
            <Grid.Row>
              <Button content={x.text} fluid type="button" />
            </Grid.Row>
          );
        })}
      </Grid>
    </Segment>
  );
};

export default SportButtons;
