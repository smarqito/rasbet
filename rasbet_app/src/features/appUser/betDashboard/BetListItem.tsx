import React from 'react'
import { Button, Card, Grid, Header } from 'semantic-ui-react'
import { IGame } from '../../../app/models/game';
import { formatRelative } from 'date-fns'
import { pt } from 'date-fns/locale'
interface IProps {
    id: number;
    game: IGame;
}
const BetListItem: React.FC<IProps> = ({ id, game }) => {
    return (
        <Card fluid key={id}>
            <Grid padded>
                <Grid.Row columns={5} verticalAlign="middle">
                    <Grid.Column stretched width={7} >
                        <Header as={'h3'}>
                            {game.sport.name}
                        </Header>
                        {formatRelative(game.start, new Date(), {locale: pt})}
                    </Grid.Column>
                    {
                        game.mainBet.odds.map(odd => {
                            return (
                                <Grid.Column width={3} textAlign="center">
                                    <Button>{odd.name} <p /> {odd.value}</Button>

                                </Grid.Column>
                            )
                        })
                    }
                </Grid.Row>
            </Grid>
        </Card>
    )
}

export default BetListItem