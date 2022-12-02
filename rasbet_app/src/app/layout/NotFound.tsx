import { Segment, Button, Header, Icon } from 'semantic-ui-react';
import { Link } from 'react-router-dom';

const NotFound = () => {
    return (
        <Segment placeholder>
            <Header icon>
                <Icon name='search' />
                A página que está a tentar aceder não existe.
            </Header>
            <Segment.Inline>
                <Button as={Link} to='/' primary>
                    Voltar para a página inicial
                </Button>
            </Segment.Inline>
        </Segment>
    );
};

export default NotFound;