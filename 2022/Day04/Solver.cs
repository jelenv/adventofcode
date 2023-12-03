using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day04 : ISolver {

    public void Run() {
        Console.WriteLine("Day04 solver");

        string[] lines = File.ReadAllLines("Day04/input.txt");

        Part1(lines);
        Part2(lines);
    }

    private static void Part1(string[] lines) {
        int assignmentContained = 0;

        foreach (var line in lines) {
            string[] elfPair = line.Split(',');
            if (IsPairContained(elfPair)) {
                assignmentContained++;
            }
        }

        Console.WriteLine($"Part1 assignments contained: {assignmentContained}");
    }

    private static void Part2(string[] lines) {
        int assignmentOverlapping = 0;

        foreach (var line in lines) {
            string[] elfPair = line.Split(',');
            if (IsPairOverlapping(elfPair)) {
                assignmentOverlapping++;
            }
        }

        Console.WriteLine($"Part2 assignments overlap: {assignmentOverlapping}");
    }

    private static bool IsPairContained(string[] elfPair) {
        int lowElf1 = int.Parse(elfPair[0].Split('-')[0]);
        int highElf1 = int.Parse(elfPair[0].Split('-')[1]);
        int lowElf2 = int.Parse(elfPair[1].Split('-')[0]);
        int highElf2 = int.Parse(elfPair[1].Split('-')[1]);

        if (lowElf1 >= lowElf2 && highElf1 <= highElf2) {
            // if first elf is in range of second elf
            return true;
        } else if (lowElf2 >= lowElf1 && highElf2 <= highElf1) {
            // if second elf is in range of first elf
            return true;
        }
        return false;
    }

    private static bool IsPairOverlapping(string[] elfPair) {
        int lowElf1 = int.Parse(elfPair[0].Split('-')[0]);
        int highElf1 = int.Parse(elfPair[0].Split('-')[1]);
        int lowElf2 = int.Parse(elfPair[1].Split('-')[0]);
        int highElf2 = int.Parse(elfPair[1].Split('-')[1]);

        if (lowElf1 >= lowElf2 && lowElf1 <= highElf2) {
            // if first elf overlaps second elf
            return true;
        }
        else if (lowElf2 >= lowElf1 && lowElf2 <= highElf1) {
            // if second elf overlaps first elf
            return true;
        }
        return false;
    }

}
