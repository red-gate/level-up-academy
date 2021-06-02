interface IDog {
    name: string;
    goodBoy: boolean;
}

function PrintDog(dog: IDog){
    console.log(`${dog.name} ${dog.goodBoy ? "is" : "is not"} a good boy`);
}

const fiddo = {
    name: "Fiddo",
    goodBoy: true 
}

PrintDog(fiddo);