using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day05 : ISolver {

    public void Run() {
        Console.WriteLine("Day05 solver");

        string[] lines = File.ReadAllLines("Day05/input.txt");

        Part1(lines);
        Part2(lines);
    }

    private List<List<char>> stacks = new() {
        new List<char>()
    };
    private bool stackListComplete;
    private int quantity;
    private int fromIndex;
    private int toIndex;

    private void Part1(string[] lines) {
        stacks = new List<List<char>> {
            new List<char>()
        };

        foreach (string line in lines) {
            if (!stackListComplete) {
                ParseCratesToStacks(line);
            } else if (line.Length > 0) {
                ParseMoveCommand(line);
                // move one at a time
                for (int i = 0; i < quantity; i++) {
                    stacks[toIndex].Add(stacks[fromIndex].Last());
                    stacks[fromIndex].RemoveAt(stacks[fromIndex].Count - 1);
                }
            }
        }

        string result = string.Concat(stacks.Select(s => s.Last()));
        Console.WriteLine($"Part1, Crates on top of stacks: {result}");
    }

    private void Part2(string[] lines) {
        stacks = new List<List<char>> {
            new List<char>()
        };

        foreach (string line in lines) {
            if (!stackListComplete) {
                ParseCratesToStacks(line);
            } else if (line.Length > 0) {
                ParseMoveCommand(line);
                // move all at once
                stacks[toIndex].AddRange(stacks[fromIndex].Skip(stacks[fromIndex].Count - quantity));
                stacks[fromIndex].RemoveRange(stacks[fromIndex].Count - quantity, quantity);
            }
        }

        string result = string.Concat(stacks.Select(s => s.Last()));
        Console.WriteLine($"Part2, Crates on top of stacks: {result}");
    }

    private void ParseCratesToStacks(string line) {
        int j = 0;
        int stackIndex = 0;
        for (int i = 0; i < line.Length; i++) {
            char c = line[i];

            if (char.IsNumber(c)) {
                stackListComplete = true;
                Console.WriteLine($"Stacks parsed: {stacks.Count}");
                PrintStacks();
                break;
            }

            if (c != ' ' && c != '[' && c != ']') {
                stacks[stackIndex].Insert(0, c);
            }

            if (++j == 4) {
                j = 0;
                stackIndex++;
                if (stackIndex >= stacks.Count) {
                    stacks.Add(new List<char>());
                }
            }
        }
    }

    private void ParseMoveCommand(string line) {
        string[] cmd = line.Split(' ');
        if (cmd.Length != 6) {
            throw new Exception("Invalid move command");
        }
        quantity = int.Parse(cmd[1]);
        fromIndex = int.Parse(cmd[3]) - 1;
        toIndex = int.Parse(cmd[5]) - 1;
    }

    private void PrintStacks(string cmd = "") {
        Console.WriteLine($"Stacks{(cmd.Length > 0 ? " (after " + cmd + ")" : "")}:");
        for (int i = 0; i < stacks.Count; i++) {
            Console.WriteLine($"{i + 1}: {string.Join(", ", stacks[i])}");
        }
    }

}
