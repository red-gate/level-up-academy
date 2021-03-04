# Exercise 1 

Take a look in a browser at [Ex1/](https://github.com/red-gate/level-up-academy/tree/master/web-training/JavaScript/Week%205%20-%20Working%20with%20Arrays/Ex1)Ex1_target.html and observe its behaviour
Reproduce this behaviour in [Ex1/](https://github.com/red-gate/level-up-academy/tree/master/web-training/JavaScript/Week%205%20-%20Working%20with%20Arrays/Ex1)Ex1.html using JavaScript to:
* Create elements in the table that are:
	* Sorted based on the sort select option
	* filtered based on the class select option

The data values are already available for you.


# Exercise 2
'Alex'
* splice
* slice

# Exercise 3
'Alex'
* from
* reduce/reduceRight
* join - a refresher (you might've encountered this while finding palindromes in week 1!)

# Exercise 4 - `find` and friends!
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

# Exercise 5 - `includes` `indexOf`
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
[localeCompare](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/String/localeCompare)
