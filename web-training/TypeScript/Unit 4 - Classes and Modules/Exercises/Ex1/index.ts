const gameChoice = (document.getElementById("input") as HTMLInputElement)
  .value as "farming_sim" | "scifi_rpg";

switch (gameChoice) {
  case "farming_sim":
    playGame();
  case "scifi_rpg":
    playGame();
}
