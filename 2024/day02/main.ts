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

function part2(reports: string[]) {
  let numberOfSafeReports = 0;
  for (const report of reports) {
    const numbers = report.split(" ");
    if (isSafe(numbers)) {
      numberOfSafeReports++;
    } else {
      if (isSafeWithSkip(numbers)) {
        numberOfSafeReports++;
      }
    }
  }
  console.log(`Part 2 (number of safe reports):`, numberOfSafeReports);
}

function isSafe(numbers: string[]): boolean {
  const nums = numbers.map(Number);

  let expectedSign = undefined;
  for (let i = 0; i < nums.length - 1; i++) {
    const diff = nums[i + 1] - nums[i];

    if (expectedSign === undefined) {
      expectedSign = Math.sign(diff);
    }
    if (Math.sign(diff) !== expectedSign) {
      return false; // array is not ordered
    }

    const absDiff = Math.abs(diff);
    if (absDiff === 0 || absDiff > 3) {
      return false; // diff out of allowed range
    }
  }

  return true;
}

function isSafeWithSkip(numbers: string[]): boolean {
  for (let i = 0; i < numbers.length; i++) {
    const numbersWithoutSkippedNum = [
      ...numbers.slice(0, i),
      ...numbers.slice(i + 1),
    ];
    if (isSafe(numbersWithoutSkippedNum)) {
      return true;
    }
  }
  return false;
}

main();
