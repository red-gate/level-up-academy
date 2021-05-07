console.log("Setting up event handler once the page has loaded")

window.addEventListener('load', () => {

    console.log("Adding a click handler to the button");
    const button = document.getElementById("my-button");
    
    button?.addEventListener("click", (event) => alert(`button was ${event.button === 1 ? "left" : "right"} clicked!`));
})