# Bringing in libraries

**N**ode **P**ackage **M**anager is the default package manager for Node.js. It consists of a command line client, also called npm, and an online database of public and paid-for private packages, called the npm registry. The registry is accessed via the client, and the available packages can be browsed and searched via the npm website. [- Wikipedia](https://en.wikipedia.org/wiki/Npm_(software)#:~:text=npm%20(originally%20short%20for%20Node,for%20the%20JavaScript%20programming%20language.&text=It%20consists%20of%20a%20command,packages%2C%20called%20the%20npm%20registry.)

We'll use NPM to add libraries to our JavaScript application.

## Tasks

- Install Node.js (my `node --version` reveals that I have `v12.18.2` installed)
- Create a project directory
- Use `npm init` to create a `package.json` document, which describes the application or library that you're building and is responsible for declaring its dependencies. Accept all default parameters.
- Create an `index.js` file wherein you `console.log('hello world')` or something like that
- Run your JavaScript application through `node index.js`
- Verify that you see your console message
- Add an entry to the `"scripts":` section in `package.json` called `"hello"`.
  - Have `hello` call `node index.js`
  - Verify that everything is kosher by running `npm run hello` from the command line, seeing your console message.

We have successfully run some server-side JavaScript! But that's not the goal of this exercise ;) 

## Parcel

Parcel is a web application bundler among many. There's also Webpack, Gulp and many others. Common for them, is that they enable us to add other people's code to our application and then package it up into one coherent script. This has benefits of script management and application size.

- Add the Parcel bundler to your application through `npm install --save-dev parcel-bundler`
- Copy the provided `index.html` file into your application
- Add a new script to `package.json` called `start` that runs `parcel index.html`
- Run `npm start`
- Navigate to `http://localhost:1234` and verify that your web page produces console output

Your JavaScript now runs in the client!

## Add a library

Let's add lit-html from the Polymer project!

- Terminate your application with `CTRL+c`
- Add `lit-html` as a part of your application through `npm install --save lit-html`.
- Copy the code example from [npmjs.com](https://www.npmjs.com/package/lit-html) into your `index.js` file
- Run `npm start` and refresh your application
- Navigate to the `Network` tab of your browser's developer tools to see that only one JavaScript file was downloaded, containing both your application code and all of its dependencies.

## More, more!

- Terminate your application again
- Add a material design button with `npm install @material/mwc-button @webcomponents/webcomponentsjs`
- Add `<mwc-button id="myButton" label="Click Me!" raised></mwc-button>` before the `script` tag in your HTML document.
- Replace the contents of your JavaScript file with the following:
```js
import '@material/mwc-button';
const button = document.querySelector('#myButton');
button.addEventListener('click', () => {
  alert('You clicked!');
});
```
- Run your application again and bask in its glory! Ignore any warning about missing `.ts` files as they are irrellevant for our example.

## Package things up

Once we're done developing an application, we can ask our bundler for an optimized production build.

For Parcel, the command looks like this: `parcel build index.html`

- Add a new NPM script into `package.json` that calls the above script.
- Observe the generated files. They are a minified and bundled version of your application, ready to be distributed!

- 

export/import vs require ?

Client focus

Ecosystem

webpack ... ? 

Bundling ... 

webpack config

clashes with typescript course? 

This guide may be adapted: https://www.typescriptlang.org/docs/handbook/gulp.html
Or just https://webpack.js.org/

https://parceljs.org/
