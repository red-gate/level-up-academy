type ValidationResult<T> =
  | { type: "success"; value: T }
  | { type: "fail"; error: string };

type Validator<T> = (input: unknown) => ValidationResult<T>;

type LiteralValue = string | number | boolean | undefined | null;

function validateOneOf<TS extends LiteralValue[]>(
  // here we exploit the fact that typescript uses a tuple type rather than array to model rest parameters.
  // this means the individual literal values passed to this function are preserved in the resulting type.
  ...args: TS
): Validator<TS[number]> {
  throw "placeholder implementation";
}

function validateBool(): Validator<boolean> {
  throw "placeholder implementation";
}

function validateObject<T>(
  validators: { [Key in keyof T]: Validator<T[Key]> }
): Validator<T> {
  throw "placeholder implementation";
}

const configValidator = validateObject({
  autoFeedPet: validateBool(),
  petType: validateOneOf("kitty", "doge"),
});

type ValidatorResult<T extends Validator<any>> = T extends Validator<infer U>
  ? U
  : never;
type ConfigType = ValidatorResult<typeof configValidator>;

function safelyParseConfigFromJson(jsonStr: string) {
  const jsonObj: unknown = JSON.parse(jsonStr);
  const validatedResult = configValidator(jsonObj);
  if (validatedResult.type === "fail") {
    throw new Error("Failed to parse config: " + validatedResult.error);
  }
  return validatedResult.value;
}

type ConfigGetters = {
  [K in keyof ConfigType as `get${Capitalize<K>}`]: () => ConfigType[K];
};

type ConfigSetters = {
  [K in keyof ConfigType as `set${Capitalize<K>}`]: (
    newValue: ConfigType[K]
  ) => void;
};

type ConfigApi = ConfigGetters & ConfigSetters;

const MockApi: ConfigApi = {
  getAutoFeedPet: () => true,
  setAutoFeedPet: (autoFeedPet) => {},
  getPetType: () => "kitty",
  setPetType: (petType) => {},
};
