type Maths = `One plus one is: ${2}`;

type BorderWidth = "1" | "2" | "3";
type BorderStyle = "dotted" | "dashed" | "solid";
type BorderColor = "blue" | "red" | "coral" | "brown";
type CssBorderString = `border: ${BorderWidth}px ${BorderStyle} ${BorderColor}`;

type EventDefinitions = {
  geometryLoaded: { polygonsProcessed: number };
  splinesReticulated: { splineCount: number };
  aiCreated: { areWeAllDoomed: boolean };
};
type EventHandlers<T> = {
  [Key in keyof T as `on${Capitalize<string & Key>}`]: (event: T[Key]) => void;
};

// `keyof T` has type `string|number|symbol`, but `Capitalize<T>` only accepts `string`.
//
// This means we need to do a bit of filtering. In the above answer, `string & Key` expands to three possibilities:
//   `string & string | string & symbol | string & number`
// `string & string` is just `string`, while the other two options resolve to `never`.
// That leaves us with `string | never | never`, which is equivalent to `string`, the type we want:
// non-string keys will be skipped over in the resulting type.
//
// `Capitalize<Key extends string ? Key : never>` also works, and is a bit more explicit.

type LevelLoadedEvents = EventHandlers<EventDefinitions>;
