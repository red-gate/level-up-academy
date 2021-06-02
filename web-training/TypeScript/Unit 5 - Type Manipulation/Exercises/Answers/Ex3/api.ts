interface CatFood {
  isTasty: boolean;
}

function createRealApi() {
  return {
    feedKitty: (food: CatFood) => {
      console.log(`kitty fed with ${food.isTasty ? "tasty" : "regular"} food`);
    },
    reticulateSplines: () => {
      console.log("splines reticulated");
    },
  };
}

type MyReturnType<T> = T extends (...args: any[]) => infer U ? U : never;
type Api = MyReturnType<typeof createRealApi>;

const MockApi: Api = {
  reticulateSplines: () => {},
  feedKitty: () => {},
};
