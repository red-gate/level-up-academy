# Bringing in libraries

**N**ode **P**ackage **M**anager is the default package manager for Node.js. It consists of a command line client, also called npm, and an online database of public and paid-for private packages, called the npm registry. The registry is accessed via the client, and the available packages can be browsed and searched via the npm website. [- Wikipedia](https://en.wikipedia.org/wiki/Npm_(software)#:~:text=npm%20(originally%20short%20for%20Node,for%20the%20JavaScript%20programming%20language.&text=It%20consists%20of%20a%20command,packages%2C%20called%20the%20npm%20registry.)

We'll use NPM to add libraries to our JavaScript application.

## Tasks

- Install Node.js (my `node --version` reveals that I have `v12.18.2` installed)
- Create a git branch for your breakout room.
- Create a project directory
- Use `npm init` inside your project directory to create a `package.json` document, which describes the application or library that you're building and is responsible for declaring its dependencies. Accept all default parameters.
- Create an `index.js` file wherein you `console.log('hello world')` or something like that
- Run your JavaScript application through `node index.js`
- Verify that you see your console message
- Add an entry to the `"scripts":` section in `package.json` called `"hello"`.
  - Have `hello` call `node index.js`
  - Verify that everything is kosher by running `npm run hello` from the command line, seeing your console message.
- Commit; switch drivers

We have successfully run some server-side JavaScript! But that's not the goal of this exercise ;) 

## Parcel

Parcel is a web application bundler among many. There's also Webpack, Gulp and many others. Common for them, is that they enable us to add other people's code to our application and then package it up into one coherent script. This has benefits of script management and application size.

- Add the Parcel bundler to your application through `npm install --save-dev parcel-bundler`
- Note that a new `node_modules` folder was created with a whole bunch of stuff! Those are the libraries that got included from the above command. 
- Also observe the new `package-lock.json` that was created. It contains the full dependency tree of your application (i.e. the parcel-bundler and all of its dependencies).
- Add a .gitignore that ignores `node_modules`. See [this link](https://github.com/github/gitignore/blob/master/Node.gitignore) for a full list of recommended ignores.
- Copy the provided `index.html` file into your application
- Add a new script to `package.json` called `start` that runs `parcel index.html`
- Run `npm start`
- Navigate to `http://localhost:1234` and verify that your web page produces console output
- Add the `.cache` and `dist` directories from parcel to your gitignore
- Commit; switch drivers

Your JavaScript now runs in the client!

## Add a library

Let's add lit-html from the Polymer project!

- Run `npm run start` and notice that it fails as we haven't installed the application dependencies yet.
- Run `npm install` to install the depencies declared in the application.
- Add `lit-html` as a part of your application through `npm install --save lit-html`.
- Copy the code example from [npmjs.com](https://www.npmjs.com/package/lit-html) into your `index.js` file
- Run `npm run start` and refresh your application
- Navigate to the `Network` tab of your browser's developer tools to see that only one JavaScript file was downloaded, containing both your application code and all of its dependencies.
- Commit; switch drivers

## More, more!

- Run `npm install` to install the depencies declared in the application.
- Add a material design button with `npm install --save @material/mwc-button @webcomponents/webcomponentsjs`
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
- Commit; switch drivers

## Package things up

Once we're done developing an application, we can ask our bundler for an optimized production build.

For Parcel, the command looks like this: `parcel build index.html`

- Add a new NPM script into `package.json` that calls the above script.
- Observe the generated files. They are a minified and bundled version of your application, ready to be distributed!

## NPX

npx is a npm package runner (x probably stands for eXecute). The typical use is to download and run a package temporarily or for trials. create-react-app is an npm package that is expected to be run only once in a project's lifecycle. Hence, it is preferred to use npx to install and run it in a single step.


