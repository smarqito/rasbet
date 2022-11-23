import { AxiosResponse } from "axios";
import React from "react";
import { Message } from "semantic-ui-react";

interface IProps {
  error: AxiosResponse;
  text?: string;
}
const ErrorMessage: React.FC<IProps> = ({ error, text }) => {
  return (
    <Message error>
      <Message.Header>{error.statusText}</Message.Header>
      {error.data && Object.keys(error.data.errors).length > 0 && (
        <Message.List>
          {Object.values(error.data.errors)
            .flat()
            .map((err: any, i) => {
              return <Message.Item key={i}>{err}</Message.Item>;
            })}
        </Message.List>
      )}
      {text && <Message.Content content={text} />}
    </Message>
  );
};

export default ErrorMessage;
