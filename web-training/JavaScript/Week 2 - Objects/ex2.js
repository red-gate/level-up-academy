function greetAndRepeat(name, n) {
	for (i = 0; i < n; i++) {
		console.log(`Hello ${name}!`);
  }
}

let iterations = 0;
const console = {
	log: message => {
  	window.console.log(message + iterations);
    iterations++;
  }
}

const list = document.querySelector('div#test-result');

//assertZeroIterations
//assertEmptyName
//assertNotANumber
//assertEqEqEq /// object tostring

function assertRuns3Times() {
	greetAndRepeat("Banana", 3);
  if (iterations !== 3) {
  	return `Expected exactly 3 iterations; found ${iterations}`
  }
}

function test(fn) {
	iterations = 0;
	let error = window[fn]();
  if (error) {
  	reportError(fn, error);
  } else {
  	reportSuccess(fn);
  }
}

test('assertRuns3Times')

function assertZeroIterations() {
	greetAndRepeat("Banana", 0);
  if (console.iterations !== 0) {
  	return `Expected 0 iterations; found ${iterations}`;
  }
}

test('assertZeroIterations')


function reportError(test, error) {
	const header = document.createElement('h3');
  header.textContent = test;
  list.appendChild(header);

	const content = document.createElement('p');
  content.textContent = error;
  list.appendChild(content);
}

function reportSuccess(test) {
	const header = document.createElement('h3');
  header.textContent = test + " ✔";
  list.appendChild(header);
}
