using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day01 : ISolver {

    public void Run() {
        Console.WriteLine("Day01 solver");

        string[] lines = File.ReadAllLines("Day01/input.txt");
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
                foreach (int topElf in topElves) {
                    if (currentElf > topElf) {
                        topElves[0] = currentElf;
                        Array.Sort(topElves);
                        break;
                    }
                }
                currentElf = 0;
            }
        }

        Console.WriteLine("Top elves: " + string.Join(", ", topElves));
        Console.WriteLine("Top3 caloric sum: " + topElves.Sum());
    }
}
