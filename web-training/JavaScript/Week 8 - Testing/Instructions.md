## Excercise 1 - Simple test

- open a terminal in the Ex1 folder
- run `npm run` to see the list of available package scripts
- run `npm install` to get the package dependencies
- run `npm run test` to run the tests

The tests are written using Jest (https://jestjs.io/) - a common test framework and runner which runs the tests inside Node.

- Look at the example code in `ex1.js`, and the example test in `ex1.test.js`  
  See how the tests are laid out with a describe block for the whole file and individual test blocks.
- Try writing the other tests suggested by the comments
  - Write a new `test` block for each, and see how the test results are displayed in the console.
  - Make sure you can see a test fail
  - Try using `expect(...).toMatchObject()` and `expect(...).toThrow()`

Useful docs:

- https://jestjs.io/docs/expect - A list of all the default available assertions
- https://jestjs.io/docs/api#describename-fn - The top-level describe() function
- https://jestjs.io/docs/api#testname-fn-timeout - The test() function
- https://jestjs.io/docs/api#testtodoname - If you want to make a list of required tests

## Excercise 2 - Set of test cases

## Example 2 - Fetch

Mock and check function calls
Async

## Example 3 - API layer

Module mocking (?)
Setup and teardown (?)

jest configuration ?
