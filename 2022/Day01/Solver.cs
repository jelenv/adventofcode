using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day01 : ISolver {

    public void Run() {
        Console.WriteLine("Day01 solver");

        string[] lines;
        var inputFile = "../aoc-inputs/2022/d01/input.txt";
        try {
            lines = File.ReadAllLines(inputFile);
        } catch (FileNotFoundException) {
            Console.WriteLine($"Input file not found: {inputFile}");
            return;
        }
        List<string> lineList = lines.ToList();
        lineList.Add(""); // ensure that we process the last elf

        Part1(lineList);
        Part2(lineList);
    }

    private void Part1(List<string> lineList) {
        int mostCaloricElf = 0;
        int currentElf = 0;

        foreach (string line in lineList) {
            string trLine = line.Trim();
            if (trLine.Length > 0) {
                currentElf += int.Parse(trLine);
            } else {
                if (currentElf > mostCaloricElf) {
                    mostCaloricElf = currentElf;
                }
                currentElf = 0;
            }
        }
        Console.WriteLine("Most caloric elf: " + mostCaloricElf);
    }

    private void Part2(List<string> lineList) {
        int[] topElves = { 0, 0, 0 };
        int currentElf = 0;

        foreach (string line in lineList) {
            string trLine = line.Trim();
            if (trLine.Length > 0) {
                currentElf += int.Parse(trLine);
            } else {
                if (currentElf > topElves[0]) {
                    topElves[0] = currentElf;
                    Array.Sort(topElves);
                }
                currentElf = 0;
            }
        }

        Console.WriteLine("Top elves: " + string.Join(", ", topElves));
        Console.WriteLine("Top3 caloric sum: " + topElves.Sum());
    }
}
