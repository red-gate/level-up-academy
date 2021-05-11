// Animals
interface Cat {
    Name: string;
    Vegetarian: boolean;
}
interface Dog {
    Name: string;
    FavouriteToy: string;
}
type Pet =  Cat | Dog;

function IsCat(pet: Pet): pet is Cat {
    return (pet as Cat).Vegetarian !== undefined;
}

function IsDog(pet: Pet): pet is Dog {
    return (pet as Dog).FavouriteToy !== undefined;
}

// Food
interface CatFood {
    Name: string;
    ContainsMeat: boolean;
}

interface DogFood {
    Quality: string;
}

// Instances
const kitty = {
    Name: "Kitty",
    Vegetarian: true,
}

const kittyFood = {
    Name: "Extra good cat food",
    ContainsMeat: false
}


const fiddo = {
    Name: "Fiddo",
    FavouriteToy: "Ball"
}

const dogFood = {
    Quality: "Cheap"
}