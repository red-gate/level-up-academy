function printId(id: number | string) {
  if (typeof id === "string") {
    // In this branch, id is of type 'string'
    console.log("Your ID is: " + id.toUpperCase());
  } else {
    // Here, id is of type 'number'
    console.log("Your ID is: " + id);
  }
}

printId(123);
printId("456");