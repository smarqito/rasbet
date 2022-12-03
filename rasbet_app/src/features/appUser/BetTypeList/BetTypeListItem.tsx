import React, { Fragment, useState } from "react";
import { Button, Card, Divider, Grid, Header } from "semantic-ui-react";
import { IBetType } from "../../../app/models/betType";

interface IProps {
  id: number;
  betType: IBetType;
}

const BetTypeListItem: React.FC<IProps> = ({ id, betType }) => {
  return (
    <Fragment>
      <Card fluid key={id} color="orange">
        <Grid padded centered>
          <Grid.Row>
            <Header as={"h3"}>{betType.type}</Header>
          </Grid.Row>
        </Grid>
        <Divider horizontal>-</Divider>
        <Grid padded textAlign="center" verticalAlign="middle">
          {betType.odds.map((odd, i) => {
            return (
              <Grid.Row key={odd.id} columns={3}>
                <Grid.Column>
                  <Header as={"h4"}>{odd.name}</Header>
                </Grid.Column>
                <Grid.Column>
                  <Divider section />
                </Grid.Column>
                <Grid.Column>
                  <Button size="large" color="orange">
                    {odd.oddValue}€
                  </Button>
                </Grid.Column>
              </Grid.Row>
            );
          })}
        </Grid>
      </Card>
    </Fragment>
  );
};

export default BetTypeListItem;
