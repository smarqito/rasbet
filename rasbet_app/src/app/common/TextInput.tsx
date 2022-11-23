import React from "react";
import { FieldRenderProps } from "react-final-form";
import { Form, FormFieldProps, Label } from "semantic-ui-react";

interface IProps
  extends FieldRenderProps<string | number, HTMLElement>,
    FormFieldProps {}

const TextInput: React.FC<IProps> = ({
  input,
  width,
  type,
  placeholder,
  disabled,
  maxLength,
  meta: { touched, error },
}) => {
  return (
    <Form.Field
      error={touched && !!error}
      type={type}
      width={width}
      disabled={disabled}
    >
      <input
        maxLength={maxLength}
        {...input}
        placeholder={placeholder}
        disabled={disabled}
      />
      {touched && error && !disabled && (
        <Label basic color="red">
          {error}
        </Label>
      )}
    </Form.Field>
  );
};

export default TextInput;
