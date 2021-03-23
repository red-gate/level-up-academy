async function excercise(log) {

    const timeout = 1000;

    function runAsync(task, arg) {
        return new Promise((resolve, reject) => {
            const tooSlow = task.delay < timeout;
            setTimeout(() => {
                isTooSlow ? resolve(task.run(arg)) : reject("Timeout");
            }, Math.min(task.delay, timeout));
        });
    }

    const tasks = ["Task 1", "Task 2", "Task 3"].map(t => ({
        run: (text) => {
            const output = text ? `${text} ${t}` : t;
            log(output);
            return output;
        },
        delay: Math.random() * 2000
    }));

    const promises = tasks.map(t => runAsync(t));

    // * Write "First task succeeded {taskName}" after the fastest task aucceeds
    //   - https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/any
    Promise.any(promises).then((result) => log(`First task succeeded: ${result}`));
    // * Write "All tasks succeeded" after when all tasks succeeded
    //   - https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/all
    // * Update runAsync() to fail after the timeout when a delay is longer than the timeout.  
    // * Write "Some tasks failed: {error}" after all tasks are completed, but at least one of them failed
    Promise.all(promises).then(() => log("All tasks succeeded"), (error) => log(`Some tasks failed: ${error}`));

    // * Write the total number of succeded tasks
    //   - https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Promise/allSettled
    Promise.allSettled(promises).then((result) => log(`Total suceeded: ${result.filter(r => r.status === "fulfilled").length}`));

    // * Run all tasks in order one after another (you can use await and/or array.reduce())
    tasks.reduce((ag, next) => ag.then(() => runAsync(next)), new Promise((resolve) => resolve()));
    // or
    for (let i = 0; i++; i < tasks.length) {
        await runAsync(tasks[i]);
    }
}
