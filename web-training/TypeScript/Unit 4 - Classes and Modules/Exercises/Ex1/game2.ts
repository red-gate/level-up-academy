type Reaper = {};
type Galaxy = {};

function doReaping(reaper: Reaper, galaxy: Galaxy): void {}

function playGame() {
  const yearsInMs = 1000 * 60 * 60 * 24 * 365;

  const reaper = {};
  const galaxy = {};

  setInterval(() => doReaping(reaper, galaxy), 50_000 * yearsInMs);
}
