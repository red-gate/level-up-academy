Pre-reading (sent out ahead of time): https://github.com/red-gate/level-up-academy/blob/master/web-training/JavaScript/Week%206%20-%20Fetching%20data/Pre-reading%2C%20Week%206.pdf


**Alex**
1. Promises, promises
- Handling success and errors
- Passing promises around
- await/async

https://www.learnwithjason.dev/blog/keep-async-await-from-blocking-execution

2. One coherent example 

- [Get IP Address and re-post it to request bin](https://github.com/red-gate/level-up-academy/blob/master/web-training/JavaScript/Week%206%20-%20Fetching%20data/fetch-and-post.html)

**Piers**
fetch ISS
response.json
stringify

# Exercise 3 - POST to an external endpoint
- Make a decision about where to POST the exercise data
  - I want to have my own request bucket to look into: Sign into [Pipedream (formerly requestbin)](https://pipedream.com/) to register an endpoint
  - I want to use whatever is provided for me in the exercise: Use the following address to post to "https://eno8r4ahvr1xg.x.pipedream.net/"  
- Author a JSON object with arbitrary data
- Author a `fetch` request where
  - You request the address from your chosen request bucket.
  - You declare in the request `header` that you `Accept` `application/json` responses.
  - The `method` is `POST`
  - The `mode` is `cors`

Make sure you use `JSON.stringify` to turn your JSON object into a string representation for wire transfer.

- Ensure that your Pipedream endpoint returns 200 OK with the following JSON `{"success":true}`.

For a complete example (if you're curious, or if you get stuck), see [fetch-and-post.html](https://github.com/red-gate/level-up-academy/blob/master/web-training/JavaScript/Week%206%20-%20Fetching%20data/fetch-and-post.html).


**Alex**
General JS Security awareness
For fun: Have an example for the atteendees to break!

options 

files

Mention:
Bidirectional communication, websockets.
