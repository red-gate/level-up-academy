# Exercise 1 

Take a look in a browser at [Ex1/](https://github.com/red-gate/level-up-academy/tree/master/web-training/JavaScript/Week%205%20-%20Working%20with%20Arrays/Ex1)Ex1_target.html and observe its behaviour
Reproduce this behaviour in [Ex1/](https://github.com/red-gate/level-up-academy/tree/master/web-training/JavaScript/Week%205%20-%20Working%20with%20Arrays/Ex1)Ex1.html using JavaScript to:
* Create elements in the table that are:
	* Sorted based on the sort select option
	* filtered based on the class select option

The data values are already available for you.

* Keep the implementaiton for the next excercise

# Exercise 2
* Read about [splice](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/splice) and [slice](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/slice) 
* Add radio buttons or other UI elements to allow selecting a single record
* Add a button and inputs to replace the selected record with a new one instead
* Add a filtering option to display only top 3 or only bottom 3 records
* Keep the implementaiton for the next excercise

# Exercise 3
* Read about [reduce](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/reduce) and [join](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/join) 
* Make a report button that displays a report in a text field that includes:
  * Average score
  * A line containing names and scores for all records in CSV format

# Exercise 4 - The Return of the Palindrome
* Read about [from](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/from) and [reverse](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/reverse) 
* Reiplement a function checking whether a string is a [palindrome](https://en.wikipedia.org/wiki/Palindrome) using only array functions and string comparison. (No loops allowed!)

# Exercise 5 - `find` and friends!
We've already worked with `filter` which returns the matching elements in an array. Find works similarly, but returns the first match or `undefined`.
## Part 1
* Pop over to https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/find
* Extract the lambda into its own `const`.
* Underneath `find`, add a call to `findIndex`, passing the lambda to both.
* Add a log statement to display the result of `findIndex`.
* Change the lambda to ask for an element greater than 12. Run and try to explain the results. 
* Change the lambda to ask for an element of value 42. Run and try to explain the results. 
* Change the lambda to ask for an element of value "8", using double-equals. Run and try to explain the results. 
* Replace all the numbers in `array1` to literals with all of the given names and favourite colours of the colleagues in your breakout room.
* Change the lambda to find elements with one of the given names.
* Add a favourite food field to one of the object literals in the array.
* Update the lambda to find that one entry.

# Exercise 6 - `includes` `indexOf`
* Pop over to https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/includes
* For each `console.log`, add a copy where you replace `includes` with `indexOf` and add a descriptive text, e.g.
```js
console.log("array1 includes 2", array1.includes(2));
console.log("indexOf 2 is", array1.indexOf(2));
```

## Resources:
* Array methods:
	* [map](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/map)
	* [filter](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/filter)
	* [sort](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/sort)
	* [forEach](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/forEach)
	* [splice](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/splice) 
	* [slice](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/slice)
	* [reduce](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/reduce)
	* [join](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/join)
	* [from](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/from)
	* [slice](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/slice) 
[localeCompare](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/localeCompare)
