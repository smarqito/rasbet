import React from 'react'
import { Card } from 'semantic-ui-react'
import { IBetType } from '../../../app/models/betType'
import { IGame } from '../../../app/models/game'
import { IUser } from '../../../app/models/user'
import BetListItem from './BetListItem'
import {addDays} from 'date-fns'

let user: IUser = {
    name: '',
    email: '',
    language: '',
    token: '',
    role: 'AppUser'
}

let bettype: IBetType = {
    lastUpdate: addDays(new Date(), 2),
    state: 'FINISHED',
    specialist: user,
    gameId: 1,
    odds: [{ name: 'Sporting', value: 1.19, win: false },
    { name: 'Empate', value: 3.19, win: false },
    { name: 'Varzim', value: 7.19, win: false }]
}
let game1: IGame = {
    start: addDays(new Date(), 1),
    sport: { name: 'SPORTING - VARZIM' },
    state: 'Open',
    bets: [],
    specialist: user,
    mainBet: bettype
}

const BetList = () => {
    return (
        <Card.Group>
            {[1, 2, 3].map(x => <BetListItem id={x} game={game1} />)}
        </Card.Group>
    )
}

export default BetList