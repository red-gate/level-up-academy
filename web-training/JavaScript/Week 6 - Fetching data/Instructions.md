# Fetching Data

[Pre-reading](https://github.com/red-gate/level-up-academy/blob/master/web-training/JavaScript/Week%206%20-%20Fetching%20data/Pre-reading%2C%20Week%206.pdf) (sent out ahead of time).


**Alex**
# Excercise 1 - Going asynchronous

- Open Ex1.html and press the button
- Look into the `excercise()` function in Ex1.js to explain what happened
- What does the `runAsync()` helper function do?
- Change `excercise()` to use `runAsync()` for `slowOperation()` using async/await
- What does it all tell us about threading and asynchronous execution in JS?
Resources:
- https://developer.mozilla.org/en-US/docs/Web/API/WindowOrWorkerGlobalScope/setTimeout
- https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise
- https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Statements/async_function

# Excercise 2 - Promises, Prmoises
- Open Ex2.html and press the button. Check Ex2.js for the implementation of `excercise()`
- Write "First task succeeded {taskName}" after the fastest task aucceed
- Write "All tasks succeeded" after when all tasks succeeded
- Update runAsync() to fail after the timeout when a delay is longer than the timeout
- Write "Some tasks failed: {error}" after all tasks are completed, but at least one of them failed
- Write the total number of succeded tasks
- Run all tasks in order one after another (you can use await and/or array.reduce())

Resources:
- https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/all
- https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/any
- https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/then
- https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/allSettled

# Exercise 3 - FETCH and POST
- Pop over to https://ipinfo.io/developers to figure out how to call the ipinfo API from JavaScript
- Make a `fetch` request to ipinfo and ensure you get a response.
- Make a decision about where to POST the exercise data
  - I want to have my own request bucket to look into: Sign into [Pipedream (formerly requestbin)](https://pipedream.com/) to register an endpoint
  - I want to use whatever is provided for me in the exercise: Use the following address to post to "https://eno8r4ahvr1xg.x.pipedream.net/"  
- Author a new `fetch` request where
  - You request the address from your chosen request bucket.
  - You declare in the request `header` that you `Accept` `application/json` responses.
  - The `method` is `POST`.
  - The `mode` is `cors`.
  - The `body` is a `JSON.stringify` encoded string of your JSON object.
- Ensure that your Pipedream endpoint returns 200 OK with the following JSON `{"success":true}`.
- If you opted to creating your own request bucket, ensure that the bucket received what you sent.

For a complete example (if you're curious, or if you get stuck), see [fetch-and-post.html](https://github.com/red-gate/level-up-academy/blob/master/web-training/JavaScript/Week%206%20-%20Fetching%20data/fetch-and-post.html).



# Try it out at home - Web and JavaScript security
- Posting data from a browser with cookies can be exploited if the web service isn't secured properly:    
- Read about [cross-site request forgery](https://portswigger.net/web-security/csrf) and try out some examples
- Browser-side JavaScript can be exploited as well:
- Try [cross-site scripting](https://xss-game.appspot.com/) out yourself


options 

files

Mention:
Bidirectional communication, websockets.
