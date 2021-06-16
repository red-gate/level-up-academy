// First, we'll generate a quick generic result type:
type ValidationResult<T> =
  | { type: "success"; value: T }
  | { type: "fail"; error: string };

// The following two types are the most important:
// - A "validator" takes an arbitrary value and checks whether it is actually a T
// - ValidatorResult<T> takes a validator and extracts the target T value from it.
// By combining these two concepts, we can validate incoming values and also make sure
// they are consumed correctly in the rest of the app.
type Validator<T> = (input: unknown) => ValidationResult<T>;
type ValidatorResult<T extends Validator<any>> = T extends Validator<infer U>
  ? U
  : never;

// Returns a simple validator which just checks whether the input value is `true` or `false`
function validateBool(): Validator<boolean> {
  throw "placeholder implementation";
}

// A more complicated validator might check that the input is one of a few specific enum values.
type LiteralValue = string | number | boolean | undefined | null;
function validateOneOf<TS extends LiteralValue[]>(
  // Here we exploit the fact that typescript uses a tuple type rather than array to model rest parameters.
  // This means the individual literal values passed to this function are preserved in the resulting type.
  ...args: TS
): Validator<TS[number]> {
  throw "placeholder implementation";
}

// We can also compose validators together.
// Here we use a mapped type: for a given target object type T, a validator for T can be defined
// if we have validators for each of T's properties.
function validateObject<T>(
  validators: { [Key in keyof T]: Validator<T[Key]> }
): Validator<T> {
  throw "placeholder implementation";
}

// As an example, let's say we have some simple config JSON containing a couple of properties.
// We can define the runtime validator for the config using our validator factories...
const configValidator = validateObject({
  autoFeedPet: validateBool(),
  petType: validateOneOf("kitty", "doge"),
});

// ...and then the rest of the code can consume a `ConfigType` safely
type ConfigType = ValidatorResult<typeof configValidator>;

// And here's an example of how we might actually parse some config JSON:
// Note how the return type is correctly inferred to be `ConfigType`.
function safelyParseConfigFromJson(jsonStr: string) {
  const jsonObj: unknown = JSON.parse(jsonStr);
  const validatedResult = configValidator(jsonObj);
  if (validatedResult.type === "fail") {
    throw new Error("Failed to parse config: " + validatedResult.error);
  }
  return validatedResult.value;
}

// Of course, we might want a nicer API to interact with our config.
// Here we use some mapped types to automatically define an API based on the shape of our config:
type ConfigGetters = {
  [K in keyof ConfigType as `get${Capitalize<K>}`]: () => ConfigType[K];
};
type ConfigSetters = {
  [K in keyof ConfigType as `set${Capitalize<K>}`]: (
    newValue: ConfigType[K]
  ) => void;
};
type ConfigApi = ConfigGetters & ConfigSetters;

// ...and we can use our automatically-defined type to check a mock implementation!
const MockApi: ConfigApi = {
  getAutoFeedPet: () => true,
  setAutoFeedPet: (autoFeedPet) => {},
  getPetType: () => "kitty",
  setPetType: (petType) => {},
};
