type FoodType = "drink" | "pizza" | "salad" | "fries";
type FoodState = "ordered" | "preparing" | "ready";

const beer = {
    type: "drink",
    name: "Guiness",
    state: "ready",
    isCold: true
}

const margheritta = {
    type: "pizza",
    name: "margheritta",
    state: "preparing",
    hasPineaple: false,
    slices: 8,
}

function writeState(food: any) {
    console.log(`${food.name} ${food.type} is ${food.state}`);
}

function takeSip(beverage: any) {
    if (beverage.isCold) {
        console.log(`nice`);
    }
    else {
        console.log(`nah`)
    }
}

function takeSlice(pizza: any) {
    if (pizza.slices === 0) {
        console.log(`No ${pizza.name} left`);
    } else {
        pizza.slices -= 1;
        console.log(`Took a slice of ${pizza.name}`);
    }
}

writeState(beer);
writeState(margheritta);
takeSip(beer);
takeSlice(margheritta);