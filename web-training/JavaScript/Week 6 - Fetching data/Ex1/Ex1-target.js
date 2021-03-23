// * Look into the `excercise()` function to explain what happened
// * What does the `runAsync()` helper function do?
//   - https://developer.mozilla.org/en-US/docs/Web/API/WindowOrWorkerGlobalScope/setTimeout
//   - https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise
// * Change `excercise()` to use `runAsync()` for `slowOperation()` using async/await
//   - https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Statements/async_function 
// * What does it all tell us about threading and asynchronous execution in JS?

function generateLongArray()
{
    let a = [];
    for (let i = 0; i < 1000000; i ++) {
        a.push(i);
    }
        
    return a;
}

function slowOperation() {
    return generateLongArray().reverse().sort();
}

async function excercise(log) {
    for (let i = 0; i < 1000; i ++) {        
        await runAsync(slowOperation);
        log(`Finished operation #${i}`);
    }
}

function runAsync(func) {
    return new Promise((resolve) => {
        setTimeout(() => { func(); resolve() }, 0);
    });
}
