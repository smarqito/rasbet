const emailRegexValidation =
  /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

export const required = (value: any) => (value ? undefined : "Obrigatório");

export const maxLength = (max: number) => (value: string) =>
  !value || (value && value.length) <= max
    ? undefined
    : `No máximo ${max} carateres`;

export const minLength = (min: number) => (value: string) =>
  value && value.length >= min ? undefined : `No mínimo ${min} carateres`;

export const exactLength = (length: number) => (value: string) =>
  value && value.length === length
    ? undefined
    : `Deve ter exatamente ${length} carateres`;

export const isNumber = (value: string) =>
  !value.match("[a-zA-Z]+") ? undefined : "Apenas números";

export const greaterThanOrEqual = (min: number) => (value: number) =>
  value >= min ? undefined : `O valor mínimo é ${min}.`;

export const isBetweenInclusive =
  (min: number, max: number) => (value: number) =>
    value >= min && value <= max
      ? undefined
      : `O valor mínimo é ${min} e max é ${max}.`;

export const greaterThan = (min: number) => (value: number) =>
  value > min ? undefined : `O valor mínimo é ${min}.`;

export const lesserThanOrEqual = (max: number) => (value: number) =>
  value <= max ? undefined : `O valor máximo é ${max}.`;

export const isEmail = (value: string) =>
  value && emailRegexValidation.test(value) ? undefined : "Email inválido";

export const isNumberLength = (length: number) =>
  composeValidators(required, isNumber, exactLength(length));

// verify if the vatnumber is valid
export const isVatNumber = (vatNumber: string) => {
  let checkDigit: number = 0;

  for (let i = 0; i < 8; i++) {
    checkDigit += parseInt(vatNumber[i]) * (9 - i);
  }

  checkDigit = 11 - (checkDigit % 11);

  if (checkDigit >= 10) checkDigit = 0;
  return checkDigit === parseInt(vatNumber[8])
    ? undefined
    : "Contribuinte inválido";
};

export const composeValidators =
  (...validators: any) =>
  (value: any) =>
    validators.reduce(
      (error: any, validator: any) => error || validator(value),
      undefined
    );
