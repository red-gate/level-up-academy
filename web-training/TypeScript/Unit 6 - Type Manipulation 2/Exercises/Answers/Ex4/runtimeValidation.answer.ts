// First, we'll generate a quick generic result type:
type ValidationResult<T> =
  | { type: "success"; value: T }
  | { type: "fail"; error: string };

// they are consumed correctly in the rest of the app.
type Validator<T> = (input: unknown) => ValidationResult<T>;
type ValidatorResult<T extends Validator<any>> = T extends Validator<infer U>
  ? U
  : never;

//
// This file contains a quick draft of how javascript part of the `validate*` methods might actually be implemented -
// totally untested though.
//

function validateBool(): Validator<boolean> {
  return (input) =>
    input === true || input === false
      ? { type: "success", value: input }
      : { type: "fail", error: `${input} is not a valid boolean` };
}

type LiteralValue = string | number | boolean | undefined | null;
function validateOneOf<TS extends LiteralValue[]>(
  ...args: TS
): Validator<TS[number]> {
  return (input) =>
    args.some((candidate) => candidate === input)
      ? { type: "success", value: input as TS[number] }
      : { type: "fail", error: `${input} is not one of ${args.join(" ")}` };
}

function validateObject<T extends object>(
  validators: { [Key in keyof T]: Validator<T[Key]> }
): Validator<T> {
  return (input) => {
    if (typeof input !== "object" || input === null) {
      return { type: "fail", error: `${input} is not an object` };
    }

    for (let key in validators) {
      if (!(key in input)) {
        return {
          type: "fail",
          error: `Expected ${input} to have key ${key}, but didn't`,
        };
      }
      // TODO: not sure why this cast is required given we're already checking for `key in input` above
      const innerResult = validators[key]((input as any)[key]);
      if (innerResult.type === "fail") {
        return {
          type: "fail",
          error: `Error in ${input} key ${key}: ${innerResult.error}`,
        };
      }
    }
    return { type: "success", value: input as T };
  };
}
