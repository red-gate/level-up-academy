interface CatFood {}
interface Api {
  reticulateSplines(): void;
  feedKitty(food: CatFood): void;
}

function createRealApi(): Api {
  return {
    feedKitty: () => {
      console.log("kitty fed");
    },
    reticulateSplines: () => {
      console.log("splines reticulated");
    },
  };
}

const MockApi: Api = {
  reticulateSplines: () => {},
  feedKitty: () => {},
};
