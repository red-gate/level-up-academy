const console = {
	iterations: 0,
	log: message => {
  	window.console.log(message);
    this.iterations++;
  }
}

const list = document.getElementsByTagName('ul')[0];

function greetAndRepeat(name, n) {
	for (i = 0; i < n; i++) {
		console.log(`Hello ${name}!`);
  }
}

//assertZeroIterations
//assertEmptyName
//assertNotANumber
//assertEqEqEq /// object tostring
assertRuns3Times()

function assertRuns3Times() {
	greetAndRepeat("Banana", 3);
  if (console.iterations !== 3) {
  	reportError("assertRuns3Times", "Expected exactly 3 iterations; found " + console.iterations)
  }
}

function initTests() {
	list.clear();
  console.iterations = 0;
}

function reportError(test, error) {
	const item = document.createElement('li');
  item.textContent = error;
  list.appendChild(item);
}

