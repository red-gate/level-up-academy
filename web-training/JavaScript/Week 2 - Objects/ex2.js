// -- Test harness
let iterations = 0;
let successRate = 0;
const console = {
  log: (message) => {
    window.console.log(message);
    iterations++;
  },
};

function report(test, errorDetails) {
  const header = document.createElement("h3");
  if (errorDetails) {
    header.textContent = test + " ❌";
    header.style.color = "red";
  } else {
    header.textContent = test + " ✔";
    header.style.color = "green";
  }
  list.appendChild(header);
  if (!errorDetails) return;

  const content = document.createElement("p");
  content.textContent = errorDetails;
  list.appendChild(content);
}

function test(fn) {
  iterations = 0;
  let error = fn();
  report(fn.prototype.constructor.name, error);
  if (!error) {
    successRate += 25;
    if (successRate === 100) {
      const img = document.createElement("img");
      img.alt = "Your skill in JavaScript has gone up! Well done!";
      img.title = "Your skill in JavaScript has gone up! Well done!";
      img.src =
        "https://images-na.ssl-images-amazon.com/images/I/51UrZPK+8vL.jpg";
      list.appendChild(img);
    }
  }
}

// -- UI
let list = document.querySelector("div#test-result");
if (!list) {
  list = document.createElement("div");
  list.id = "test-result";
}

// -- Tests

test(function assertRuns3Times() {
  greetAndRepeat("Banana", 3);
  if (iterations !== 3) {
    return `Expected exactly 3 iterations; found ${iterations}`;
  }
});

test(function assertZeroIterations() {
  greetAndRepeat("Banana", 0);
  if (iterations !== 0) {
    return `Expecting 0 iterations; found ${iterations}`;
  }
});

test(function assertEmptyName() {
  try {
    greetAndRepeat("", 3);
  } catch (ex) {
    if (ex.toLowerCase().indexOf("name") === -1) {
      return `Expecting exception to mention 'name' in some fashion`;
    }
    return;
  }
  return `Expecting exception to be thrown for empty string`;
});

test(function assertNotANumber() {
  try {
    greetAndRepeat("George", "three");
  } catch (ex) {
    if (ex.toLowerCase().indexOf("num") === -1) {
      return `Expecting exception to explain that the second parameter needs to be a number`;
    }
    return;
  }
  return `Expecting exception to be thrown for non-numeric 2nd parameter`;
});
