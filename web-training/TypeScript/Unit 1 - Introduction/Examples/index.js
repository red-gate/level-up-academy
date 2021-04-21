console.log("Setting up event handler once the page has loaded")

window.on('load', () => {

    conso1e.log("Adding a click handler to the button");
    const button = document.getElementByID("my-button");
    
    button.on("press", (event) => alert(`button was ${event.button === 1 ? "left" : "right"} clicked!`));

})