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

interface CatFood {
    Name: string;
    ContainsMeat: boolean;
}

interface DogFood {
    Quality: string;
}

function Feed(cat: Cat, catFood: CatFood): void;
function Feed(dog: Dog, dogFood: DogFood): void;
function Feed(pet: Pet, food: CatFood | DogFood): void
{
    if(IsCat(pet)){
       console.log(`Fed my cat ${pet.Name} with ${(food as CatFood).Name}`);
    }
    if(IsDog(pet)){
            console.log(`Fed my dog ${pet.Name} with ${(food as DogFood).Quality} quality food`);
    }
}

function FeedCat(cat: Cat, catFood?: CatFood){
    if(catFood){
        console.log(`You fed your cat ${cat.Name} with food that ${catFood.ContainsMeat ? "wasn't" : "was"} vegetarian`);
    }else{
        console.log(`You forgot to provide any food to ${cat.Name}`)
    }
}

function FeedEither(pet: Pet, catFood?: CatFood, dogFood?: DogFood): void {
    console.log("Force fed my animal with some food");
}

function ForceFeed(pet: Pet, food: CatFood | DogFood): void {
    console.log("Force fed my animal with some food");
}

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

FeedCat(kitty, kittyFood);
FeedCat(kitty);

FeedEither(kitty, dogFood);

ForceFeed(kitty, dogFood);

Feed(fiddo, dogFood);
Feed(kitty, kittyFood);