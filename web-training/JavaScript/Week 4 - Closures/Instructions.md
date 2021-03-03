# Focus

## Ex1

* https://jsfiddle.net/d3o7xhm9/
* Run the code and analyze it's behaviour
* Fix the code using different approaches
  * apply
  * bind
  * lamdba expresison
  * closure

## Ex2

### Working with local sources
*Note:* This is tested with Chrome and Firefox. If you are using another browser, your miles may vary.
* Create a local folder, wherein you'll create a set of local files with the contents of the finished exercise above.
* Create a CSS file and include that file as a source in your HTML file
  * Tip! If your are using Visual Studio Code, cut all existing code in the HTML file, type an exclamation mark at the start of the file and hit `TAB` to expand a HTML boilerplate. Then paste the previous code inside the body tag.
* Include the JavaScript file in the HTML file.
* Add the JavaScript statement `debugger;` just before the call to `counter.next` in your JavaScript file.
* Add an empty rule for `button` elements in the CSS file.
* Direct your web browser to the HTML file you just created
* Open the Developer Tools with `F12`
* Reload the page.
* Note how the script execution paused on your `debugger;` statement, allowing you to investigate the application state.
* Remove the `debugger;` statement and reload the page.

#### For Chrome
* Click on the `Sources` tab in the Developer Tools
* Drag your source folder into the `Sources` pane
* Accept the request for full access to the folder in your browser
* If you cannot see a file listing on the left hand side of the `Sources` panel, click the `Navigator` right arrow on he left hand side of the navigator
* Using the Element selector in the top-left corner of the developer tools, select a button
* Switch over to the `Styles` tab and notice that your CSS file is listed as a source
* Edit the styles of the button using the developer tools and notice how they are persisted to disk.
* Navigate back to the `Sources` tab and set a breakpoint inside the function `counter.next` by clicking in the margin of the editor. Reload the page and ensure that your breakpoint is hit.
* Practice the different debugger options: Resume (`F8`), Step over (`F10`), Step into (`F11`), Step out of (`Shift`+`F11`) and Step (`F9`)
* Make an edit to the `counter.next` function inside the browser, press `CTRL`+`s` and note how the change is persisted to disk
* Pay attention to the available values in the `Scope` tab

#### For Firefox
* Go to the `Styles Editor` tab in the Developer Tools
* Start typing `b` and notice how you get code completion to the specific button elements with ID. Edit the style of the `button-display` button. Press `CTRL`+`s` to save.
* Navigate to the `Debugger` tab and set a breakpoint inside the function `counter.next` by clicking in the margin of the editor. Reload the page and ensure that your breakpoint is hit.
* Practice the different debugger options: Resume (`F8`), Step over (`F10`), Step into (`F11`) and Step out of (`Shift`+`F11`)
* Pay attention to the available values in the `Scopes` tab


## Ex3

In Ex3/Ex3Target.html there are three boxes on screen. You can single or double click the boxes and observe the output in the console
In Ex3/Ex3.html there are the same three div elements that overlap.

* Write a double click event handler that outputs to the console all the box ids that are clicked
* Write a click event handler that outputs to the console only the box id that was clicked


## Ex4
`Sami`
private members: hide functions by not exporting them - https://developer.mozilla.org/en-US/docs/Web/JavaScript/Closures#emulating_private_methods_with_closures
- iife (module style syntax).

## Ex5

- https://jsfiddle.net/5ebosx1r/
- Run the code and analyze it's behaviour
- Fix the code
- Go back to the initial version and replace `var i` with `let i`. Try it out.


## Resources
- https://developer.mozilla.org/en-US/docs/Web/JavaScript/Closures
- https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Function/apply
- https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_objects/Function/bind
- [stopPropagation](https://developer.mozilla.org/en-US/docs/Web/API/Event/stopPropagation)
