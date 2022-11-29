import React from "react";
import { Segment, Header, Icon, Grid } from "semantic-ui-react";

const ListItemNotFound: React.FC<{ content: string }> = ({ content }) => {
  return (
    <div style={{textAlign: 'center'}}>
      <Segment placeholder>
        <Header icon>
          <Icon name="search" />
          {content}
        </Header>
      </Segment>
    </div>
  );
};

export default ListItemNotFound;
