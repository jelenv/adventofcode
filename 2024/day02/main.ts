#!/usr/bin/env -S deno run --allow-read

async function main() {
  const input = await Deno.readTextFile("../../aoc-inputs/2024/d02/input.txt");
  const lines = input.trim().split("\n");

  part1(lines);
  part2(lines);
}

function part1(reports: string[]) {
  let numberOfSafeReports = 0;
  for (const report of reports) {
    const numbers = report.split(" ");
    if (isSafe(numbers)) {
      numberOfSafeReports++;
    }
  }
  console.log(`Part 1 (number of safe reports):`, numberOfSafeReports);
}

// TODO: fix part2, skips are not working correctly for all inputs
function part2(reports: string[]) {
  let numberOfSafeReports = 0;
  for (const report of reports) {
    const numbers = report.split(" ");
    if (isSafe(numbers, true)) {
      numberOfSafeReports++;
    }
  }
  console.log(`Part 2 (number of safe reports):`, numberOfSafeReports);
}

function isSafe(numbers: string[], canSkipOneNum: boolean = false): boolean {
  const nums = numbers.map(Number);

  let expectedSign = undefined;
  let skippedNum = undefined;
  for (let i = 0; i < nums.length - 1; i++) {
    const diff = nums[i + 1] - nums[i];

    if (expectedSign === undefined) {
      expectedSign = Math.sign(diff);
    }
    if (Math.sign(diff) !== expectedSign) {
      if (canSkipOneNum && skippedNum === undefined) {
        skippedNum = nums[i];
        const numbersWithoutSkippedNum = numbers
          .slice(0, i)
          .concat(numbers.slice(i + 1));
        return isSafe(numbersWithoutSkippedNum);
      }
      return false; // array is not ordered
    }

    const absDiff = Math.abs(diff);
    if (absDiff === 0 || absDiff > 3) {
      if (canSkipOneNum && skippedNum === undefined) {
        skippedNum = nums[i];
        const numbersWithoutSkippedNum = numbers
          .slice(0, i)
          .concat(numbers.slice(i + 1));
        return isSafe(numbersWithoutSkippedNum);
      }
      return false; // diff out of allowed range
    }
  }

  return true;
}

main();
