#!/usr/bin/env -S deno run --allow-read

const XMAS_SQ = ['X', 'M', 'A', 'S'];

type Direction = [number, number];
const DIRECTIONS: Direction[] = [
  [-1, 0], // left
  [0, -1], // up
  [1, 0], // right
  [0, 1], // down
  [-1, -1], // up-left
  [-1, 1], // up-right
  [1, -1], // down-left
  [1, 1], // down-right
];

async function main() {
  const input = await Deno.readTextFile('../../aoc-inputs/2024/d04/input.txt');
  const grid = input.split('\n').map((line) => line.split(''));

  part1(grid);
  // part2(input);
}

function part1(grid: string[][]) {
  let xmasCount = 0;
  for (let x = 0; x < grid.length; x++) {
    for (let y = 0; y < grid[x].length; y++) {
      if (grid[x][y] === XMAS_SQ[0]) {
        xmasCount += DIRECTIONS.reduce((count, dir) => {
          return count + (checkSequence(grid, x, y, dir) ? 1 : 0);
        }, 0);
      }
    }
  }
  console.log(`Part 1 (number of XMASes):`, xmasCount);
}

function part2(input: string) {
}

function checkSequence(grid: string[][], x: number, y: number, dir: Direction): boolean {
  const [xDelta, yDelta] = dir;
  for (let i = 0; i < XMAS_SQ.length; i++) {
    const posX = x + xDelta * i;
    const posY = y + yDelta * i;

    if (
      posX < 0 || posX >= grid.length ||
      posY < 0 || posY >= grid[y].length ||
      grid[posX][posY] !== XMAS_SQ[i]
    ) {
      return false;
    }
  }
  return true;
}

main();
