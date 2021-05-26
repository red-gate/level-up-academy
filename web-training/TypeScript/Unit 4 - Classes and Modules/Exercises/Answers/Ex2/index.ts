import game1 = require("./game1");
import game2 = require("./game2");

const gameChoice = (document.getElementById("input") as HTMLInputElement)
  .value as "farming_sim" | "scifi_rpg";

switch (gameChoice) {
  case "farming_sim":
    game1.playGame();
  case "scifi_rpg":
    game2.playGame();
}
