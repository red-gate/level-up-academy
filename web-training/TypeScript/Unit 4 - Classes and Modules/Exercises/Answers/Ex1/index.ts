import { playGame as playFarmingSim } from "./game1";
import { playGame as playScifiRpg } from "./game2";

const gameChoice = (document.getElementById("input") as HTMLInputElement)
  .value as "farming_sim" | "scifi_rpg";

switch (gameChoice) {
  case "farming_sim":
    playFarmingSim();
    break;
  case "scifi_rpg":
    playScifiRpg();
    break;
}
