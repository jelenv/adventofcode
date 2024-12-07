#!/usr/bin/env -S deno run --allow-read

async function main() {
  const input = await Deno.readTextFile('../../aoc-inputs/2024/d01/input.txt');

  const lines = input.trim().split('\n');
  const leftList = [];
  const rightList = [];
  for (const line of lines) {
    const [a, b] = line.split(/\s+/);
    insertInAscOrder(leftList, a);
    insertInAscOrder(rightList, b);
  }

  part1(leftList, rightList);
  part2(leftList, rightList);
}

function part1(leftList, rightList) {
  const t = performance.now();
  let sumOfDistances = 0;
  for (let i = 0; i < leftList.length; i++) {
    sumOfDistances += Math.abs(leftList[i] - rightList[i]);
  }
  console.log(`Part 1 (distance between lists) done in ${t.toPrecision(4)} ms:`, sumOfDistances);
}

function part2(leftList, rightList) {
  const t = performance.now();
  let similarityScore = 0;
  for (let i = 0; i < leftList.length; i++) {
    similarityScore += leftList[i] * timesInList(rightList, leftList[i]);
  }
  console.log(`Part 2 (similarity score) done in ${t.toPrecision(4)} ms:`, similarityScore);
}

function insertInAscOrder(list, value) {
  const valNum = parseInt(value);
  const index = list.findIndex((item) => item > valNum);
  if (index === -1) {
    list.push(valNum);
  } else {
    list.splice(index, 0, valNum);
  }
}

/**
 * list.filter((item) => item === value).length;
 *
 * **but faster:** on this input, it's about 21.5% faster
 */
function timesInList(list, value) {
  let count = 0;
  let index = list.findIndex((item) => item === value);
  while (index !== -1 && list[index] === value) {
    count++;
    index++;
  }
  return count;
}

main();
