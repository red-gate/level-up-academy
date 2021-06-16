type Maths = `One plus one is: ${2}`;

type BorderWidth = "1" | "2" | "3";
type BorderStyle = "dotted" | "dashed" | "solid";
type BorderColor = "blue" | "red" | "coral" | "brown";
type CssBorderString = unknown;

//
// Exercise: Change CssBorderString so that it can represent strings like "border: 3px solid red;"
//

type EventDefinitions = {
  geometryLoaded: { polygonsProcessed: number };
  splinesReticulated: { splineCount: number };
  aiCreated: { areWeAllDoomed: boolean };
};
type EventHandlers<T> = {
  [Key in keyof T]: (event: T[Key]) => void;
};
type LevelLoadedEvents = EventHandlers<EventDefinitions>;

//
// Template literals can be particularly useful when combined with mapped types.
// Exercise: change EventHandlers<T> so that it creates an object like this:
//
// { onGeometryLoaded: (event: { polygonsProcessed }) => void, ... }
//
// Use template literal types to prepend the 'on' string and uppercase the initial letter.
// Hint: you can use the Capitalize<T> type for the uppercasing.
// Check that LevelLoadedEvents looks correct once you're done
//
