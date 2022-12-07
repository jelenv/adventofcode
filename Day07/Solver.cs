using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day07 : ISolver {

    public void Run() {
        Console.WriteLine("Day07 solver");

        string[] input = File.ReadAllLines("Day07/input.txt");

        Part1(input);
        Part2(input);
    }

    private void Part1(string[] input) {
        int result = GetDirectorySizes(input).Where(x => x < 100000).Sum();
        Console.WriteLine($"Part1, sum of directories under 100k: {result}");
    }

    private void Part2(string[] input) {
        List<int> dirSizes = GetDirectorySizes(input);
        int currentFreeSpace = 70000000 - dirSizes[0]; // filesystem size - root dir size
        int spaceToBeFreed = 30000000 - currentFreeSpace;
        int result = dirSizes.Where(x => x >= spaceToBeFreed).OrderBy(x => x).First();
        Console.WriteLine($"Part2, size of dir to delete: {result}");
    }

    private List<int> GetDirectorySizes(string[] input) {
        Stack<string> cwd = new();
        Dictionary<string, int> dirSizes = new();

        foreach (string line in input) {
            if (line.StartsWith("$ cd")) {
                if (line.EndsWith("..")) {
                    cwd.Pop();
                } else {
                    cwd.Push(string.Concat(cwd) + line.Split(" ").Last());
                }
            } else if (!line.StartsWith("$ ls") && !line.StartsWith("dir")) {
                int size = int.Parse(line.Split(" ")[0]);
                foreach (string dir in cwd) {
                    if (!dirSizes.ContainsKey(dir)) {
                        dirSizes.Add(dir, 0);
                    }
                    dirSizes[dir] += size;
                }
            }
        }
        return dirSizes.Values.ToList();
    }

}
