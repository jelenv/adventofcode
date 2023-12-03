using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day03 : ISolver {

    public void Run() {
        Console.WriteLine("Day03 solver");

        string[] lines = File.ReadAllLines("Day03/input.txt");

        Part1(lines);
        Part2(lines);
    }

    private void Part1(string[] lines) {
        int totalPriority = 0;

        foreach (string line in lines) {
            string bag1 = line[..(line.Length / 2)];
            string bag2 = line[(line.Length / 2)..];

            char common = FindCommonChars(bag1, bag2)[0];
            int priority = common & 0x1F; // bitwise AND
            if (char.IsUpper(common)) {
                priority += 26;
            }

            totalPriority += priority;
            // Console.WriteLine($"{bag1} {bag2} {common} {priority}");
        }
        Console.WriteLine($"Part 1, sum of priorities: {totalPriority}");
    }

    private void Part2(string[] lines) {
        int totalPriority = 0;

        for (int i = 0; i < lines.Length; i += 3) {
            string group1 = lines[i];
            string group2 = lines[i + 1];
            string group3 = lines[i + 2];

            string commonString = FindCommonChars(group1, group2);
            char common = FindCommonChars(commonString, group3)[0];
            int priority = common & 0x1F; // bitwise AND with hex 1F
            if (char.IsUpper(common)) {
                priority += 26;
            }

            totalPriority += priority;
            // Console.WriteLine($"{common} {priority}");
        }
        Console.WriteLine($"Part 2, sum of priorities: {totalPriority}");
    }

    private static string FindCommonChars(string str1, string str2) {
        var commonChars = str1.GroupBy(c => c, EqualityComparer<char>.Default)
            .Join(
                str2.GroupBy(c => c, EqualityComparer<char>.Default),
                g => g.Key,
                g => g.Key,
                (lg, rg) => lg.Zip(rg, (l, _) => l),
                EqualityComparer<char>.Default)
            .SelectMany(g => g);
        return string.Concat(commonChars);
    }
}
