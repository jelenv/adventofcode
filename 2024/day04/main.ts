#!/usr/bin/env -S deno run --allow-read

const XMAS_SQ = ['X', 'M', 'A', 'S'];

type Direction = [number, number];
const DIRS_P1: Direction[] = [
  [-1, 0], // left
  [0, -1], // up
  [1, 0], // right
  [0, 1], // down
  [-1, -1], // up-left
  [-1, 1], // up-right
  [1, -1], // down-left
  [1, 1], // down-right
];

const DIRS_P2_First: Direction[] = [
  [-1, -1], // up-left
  [1, 1], // down-right
];

const DIRS_P2_Second: Direction[] = [
  [-1, 1], // up-right
  [1, -1], // down-left
];

async function main() {
  const input = await Deno.readTextFile('../../aoc-inputs/2024/d04/input.txt');
  const grid = input.split('\n').map((line) => line.split(''));

  part1(grid);
  part2(grid);
}

function part1(grid: string[][]) {
  let xmasCount = 0;
  for (let x = 0; x < grid.length; x++) {
    for (let y = 0; y < grid[x].length; y++) {
      if (grid[x][y] === XMAS_SQ[0]) {
        xmasCount += DIRS_P1.reduce((count, dir) => {
          return count + (checkP1Sequence(grid, x, y, dir) ? 1 : 0);
        }, 0);
      }
    }
  }
  console.log(`Part 1 (number of XMASes):`, xmasCount);
}

function part2(grid: string[][]) {
  let xmasCount = 0;
  for (let x = 0; x < grid.length; x++) {
    for (let y = 0; y < grid[x].length; y++) {
      if (grid[x][y] === 'A') {
        xmasCount += checkP2Sequence(grid, x, y) ? 1 : 0;
      }
    }
  }
  console.log(`Part 2 (number of XMASes):`, xmasCount);
}

function checkP1Sequence(grid: string[][], x: number, y: number, dir: Direction): boolean {
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

function checkP2Sequence(grid: string[][], x: number, y: number): boolean {
  return checkP2Diagonal(grid, x, y, DIRS_P2_First) && checkP2Diagonal(grid, x, y, DIRS_P2_Second);
}

function checkP2Diagonal(grid: string[][], x: number, y: number, dirs: Direction[]): boolean {
  let mCount = 0;
  let sCount = 0;
  for (const dir of dirs) {
    const [xDelta, yDelta] = dir;
    const posX = x + xDelta;
    const posY = y + yDelta;

    if (
      posX < 0 || posX >= grid.length ||
      posY < 0 || posY >= grid[y].length
    ) {
      return false;
    }
    if (grid[posX][posY] === 'M') {
      mCount++;
    } else if (grid[posX][posY] === 'S') {
      sCount++;
    }
  }
  return mCount === 1 && sCount === 1;
}

main();
