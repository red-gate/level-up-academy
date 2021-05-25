type FoodType = "drink" | "pizza" | "salad" | "fries";
type FoodState = "ordered" | "preparing" | "ready";

interface IFood {
    type: FoodType,
    name: string,
    state: FoodState,
}

interface IBeverage extends IFood {
    type: "drink",
    isCold: boolean
}

interface IPizza extends IFood {
    type: "pizza",
    hasPineaple: boolean,
    slices: number
}

const beer: IBeverage = {
    type: "drink",
    name: "Guiness",
    state: "ready",
    isCold: true
}

const margheritta: IPizza = {
    type: "pizza",
    name: "margheritta",
    state: "preparing",
    hasPineaple: false,
    slices: 8,
}

function writeState(food: IFood) {
    console.log(`${food.name} ${food.type} is ${food.state}`);
}

function takeSip(beverage: IBeverage) {
    if (beverage.isCold) {
        console.log(`nice`);
    }
    else {
        console.log(`nah`)
    }
}

function takeSlice(pizza: Pizza) {
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