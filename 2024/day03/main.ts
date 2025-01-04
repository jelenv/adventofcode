#!/usr/bin/env -S deno run --allow-read

const MulPattern = ["m", "u", "l", "(", Number, ",", Number, ")"];

interface MulState {
  numberA: string;
  numberB: string;
  currentIndex: number;
}

async function main() {
  const input = await Deno.readTextFile("../../aoc-inputs/2024/d03/input.txt");

  part1(input);
  // part2(lines);
}

function part1(input: string) {
  let sumOfMulResults = 0;
  const state = {
    numberA: "",
    numberB: "",
    currentIndex: 0,
  };
  for (let i = 0; i < input.length; i++) {
    const char = input[i];
    const nextChar = i < input.length - 1 ? input[i + 1] : "";
    if (
      char === MulPattern[state.currentIndex] ||
      ((state.currentIndex === 4 || state.currentIndex === 6) &&
        Number(char) >= 0)
    ) {
      if (state.currentIndex === 4 && Number(char) >= 0) {
        state.numberA += char;
        if (nextChar === ",") {
          state.currentIndex++;
        }
      } else if (state.currentIndex === 6 && Number(char) >= 0) {
        state.numberB += char;
        if (nextChar === ")") {
          state.currentIndex++;
        }
      } else {
        state.currentIndex++;
      }

      if (
        state.currentIndex === MulPattern.length &&
        state.numberA !== "" &&
        state.numberB !== ""
      ) {
        sumOfMulResults += Number(state.numberA) * Number(state.numberB);
        resetState(state);
      }
    } else {
      resetState(state);
    }
  }

  console.log(`Part 1 (sum of mul results):`, sumOfMulResults);
}

function part2(input: string[]) {}

function resetState(state: MulState): void {
  state.currentIndex = 0;
  state.numberA = "";
  state.numberB = "";
}

main();
