import React from "react";
import { FieldRenderProps } from "react-final-form";
import {
  Dropdown,
  Form,
  FormFieldProps,
  Label,
  Select,
} from "semantic-ui-react";

interface IProps extends FieldRenderProps<any, HTMLElement>, FormFieldProps {}

const SelectInput: React.FC<IProps> = ({
  input,
  disabled,
  width,
  options,
  placeholder,
  meta: { touched, error },
}) => {
  return (
    <Form.Field error={touched && !!error} width={width}>
      <Dropdown
        search
        fluid
        selection
        disabled={disabled}
        value={input.value}
        onChange={(e, data) => input.onChange(data.value)}
        placeholder={placeholder}
        options={options}
      />
      {touched && error && (
        <Label basic color="red">
          {error}
        </Label>
      )}
    </Form.Field>
  );
};

export default SelectInput;
