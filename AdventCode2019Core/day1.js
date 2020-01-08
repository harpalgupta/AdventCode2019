var fs = require("fs");

var contents = fs
    .readFileSync("day1Input.txt", "utf8")
  .toString()
  .split("\n");

var massCalculate = mass => {
  return Math.floor(mass / 3) - 2;
};

const massCalculateWithFuel = mass => {
  let total = 0;
  var tmpMass = mass;
  while (tmpMass > 2) {
    tmpMass = massCalculate(tmpMass);
    if (tmpMass > 0) total += tmpMass;
  }
  return total;
};

let part1total = 0;
let part2total = 0;

contents.forEach(element => {
  part1total += massCalculate(element);
  part2total += massCalculateWithFuel(element);
});
console.log(part1total, part2total);
