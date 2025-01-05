#!/usr/bin/env -S deno run --allow-read

const MUL_PTRN = ['m', 'u', 'l', '(', Number, ',', Number, ')'];
const DO_PTRN = ['d', 'o', '(', ')'];
const DONT_PTRN = ['d', 'o', 'n', "'", 't', '(', ')'];

interface ProgramState {
  resultSum: number;
  char: string;
  nextChar: string;
  mulState: MulState;
  doState: DoState;
}

interface DoState {
  enabled: boolean;
  processingDont: boolean;
  index: number;
}

interface MulState {
  numA: string;
  numB: string;
  index: number;
}

async function main() {
  const input = await Deno.readTextFile('../../aoc-inputs/2024/d03/input.txt');

  part1(input);
  part2(input);
}

function part1(input: string) {
  const pstate: ProgramState = {
    char: '',
    nextChar: '',
    resultSum: 0,
    mulState: {
      numA: '',
      numB: '',
      index: 0,
    },
    doState: {
      enabled: true,
      processingDont: false,
      index: 0,
    },
  };

  for (let i = 0; i < input.length; i++) {
    pstate.char = input[i];
    pstate.nextChar = i < input.length - 1 ? input[i + 1] : '';

    processMulExpr(pstate);
  }

  console.log(`Part 1 (sum of muls):`, pstate.resultSum);
}

function part2(input: string) {
  const pstate: ProgramState = {
    char: '',
    nextChar: '',
    resultSum: 0,
    mulState: {
      numA: '',
      numB: '',
      index: 0,
    },
    doState: {
      enabled: true,
      processingDont: false,
      index: 0,
    },
  };

  for (let i = 0; i < input.length; i++) {
    pstate.char = input[i];
    pstate.nextChar = i < input.length - 1 ? input[i + 1] : '';

    processControlExprs(pstate);
    processMulExpr(pstate);
  }

  console.log(`Part 2 (sum of muls, with do/dont):`, pstate.resultSum);
}

function processMulExpr(pstate: ProgramState): void {
  const mstate = pstate.mulState;
  if (
    pstate.char === MUL_PTRN[mstate.index] ||
    ((mstate.index === 4 || mstate.index === 6) &&
      Number(pstate.char) >= 0)
  ) {
    if (mstate.index === 4 && Number(pstate.char) >= 0) {
      mstate.numA += pstate.char;
      if (pstate.nextChar === ',') {
        mstate.index++;
      }
    } else if (mstate.index === 6 && Number(pstate.char) >= 0) {
      mstate.numB += pstate.char;
      if (pstate.nextChar === ')') {
        mstate.index++;
      }
    } else {
      mstate.index++;
    }

    if (
      mstate.index === MUL_PTRN.length &&
      mstate.numA !== '' &&
      mstate.numB !== ''
    ) {
      if (pstate.doState.enabled) {
        pstate.resultSum += Number(mstate.numA) * Number(mstate.numB);
      }
      resetMulState(mstate);
    }
  } else {
    resetMulState(mstate);
  }
}

function processControlExprs(pstate: ProgramState): void {
  const dstate = pstate.doState;
  const char = pstate.char;
  const nextChar = pstate.nextChar;

  if (dstate.processingDont) {
    if (char === DONT_PTRN[dstate.index]) {
      dstate.index++;
      if (dstate.index === DONT_PTRN.length) {
        dstate.enabled = false;
        dstate.index = 0;
        dstate.processingDont = false;
      }
    } else {
      dstate.index = 0;
      dstate.processingDont = false;
    }
  } else {
    if (char === DO_PTRN[dstate.index]) {
      dstate.index++;
      if (nextChar === 'n') {
        dstate.processingDont = true;
      }
      if (dstate.index === DO_PTRN.length) {
        dstate.enabled = true;
        dstate.index = 0;
        dstate.processingDont = false;
      }
    } else {
      dstate.index = 0;
    }
  }
}

function resetMulState(state: MulState): void {
  state.index = 0;
  state.numA = '';
  state.numB = '';
}

main();
