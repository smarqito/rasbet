import { useContext } from "react";
import { Button, Form, Grid, Header, Label, Segment } from "semantic-ui-react";
import { RootStoreContext } from "../../app/stores/rootStore";
import { Field, Form as FinalForm } from "react-final-form";
import { UserPwdFormValues } from "../../app/models/user";
import {
  composeValidators,
  minLength,
  required,
} from "../../app/common/Validators";
import TextInput from "../../app/common/TextInput";
import { observer } from "mobx-react-lite";

const ChangePass = () => {
  const rootStore = useContext(RootStoreContext);

  return (
    <></>
    // <Grid
    //   textAlign="center"
    //   style={{ height: "100vh" }}
    //   verticalAlign="middle"
    // >
    //   <Grid.Column style={{ maxWidth: 450 }}>
    //     <Header as="h1" color="teal" textAlign="center">
    //       Alterar Password
    //     </Header>
    //     <FinalForm
    //       initialValues={new UserPwdFormValues()}
    //       validate={}
    //       onSubmit={}
    //       render={({
    //         handleSubmit,
    //         submitError,
    //         dirtySinceLastSubmit,
    //         values,
    //         submitting,
    //         submitFailed,
    //         invalid,
    //         errors,
    //       }) => (
    //         <Form onSubmit={handleSubmit} size="large">
    //           <Segment stacked>
    //             {console.log(errors)}
    //             <Field
    //               name="password"
    //               component={TextInput}
    //               fluid
    //               icon="lock"
    //               iconPosition="left"
    //               placeholder="Antiga palavra-passe"
    //               type="password"
    //               validate={composeValidators(required, minLength(6))}
    //             />
    //             <Field
    //               name="newPassword"
    //               component={TextInput}
    //               fluid
    //               icon="lock"
    //               iconPosition="left"
    //               placeholder="Nova palavra-passe"
    //               type="password"
    //             >
    //               {({
    //                 input,
    //                 meta: { touched, error },
    //                 placeholder,
    //                 type,
    //                 width,
    //               }) => (
    //                 <Form.Field
    //                   error={touched && error && !!error.newPassword}
    //                   type={type}
    //                   width={width}
    //                 >
    //                   <input {...input} placeholder={placeholder} />
    //                   {touched && error && error.newPassword !== "" && (
    //                     <Label basic color="red">
    //                       {error}
    //                     </Label>
    //                   )}
    //                 </Form.Field>
    //               )}
    //             </Field>
    //             <Field
    //               name="repetePass"
    //               component={TextInput}
    //               fluid
    //               icon="lock"
    //               iconPosition="left"
    //               placeholder="Confirmar palavra-passe"
    //               type="password"
    //               validate={composeValidators(minLength(6))}
    //             />
    //             <Button
    //               color="teal"
    //               fluid
    //               size="large"
    //               disabled={invalid && !dirtySinceLastSubmit}
    //             >
    //               Alterar Palavra-Passe
    //             </Button>
    //           </Segment>
    //         </Form>
    //       )}
    //     />
    //   </Grid.Column>
    // </Grid>
  );
};

export default observer(ChangePass);
