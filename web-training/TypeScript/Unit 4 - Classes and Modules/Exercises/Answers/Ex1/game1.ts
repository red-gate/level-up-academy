type Field = { stalks: any[] };
type Corn = { cornCount: number };

function doReaping(field: Field): Corn {
  return { cornCount: field.stalks.length };
}

function playGame() {
  const farm = {
    field: {
      stalks: new Array(999),
    },
  };

  const corn = doReaping(farm.field);
}

export { playGame };
