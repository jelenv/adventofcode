using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day06 : ISolver {

    public void Run() {
        Console.WriteLine("Day06 solver");

        string input = File.ReadAllText("Day06/input.txt");

        Part1(input);
        Part2(input);
    }

    private void Part1(string input) {
        int currentChar = FindMarkerInMessage(4, input);
        Console.WriteLine($"Part1, start-of-packet marker found at: {currentChar}");
    }

    private void Part2(string input) {
        int currentChar = FindMarkerInMessage(14, input);
        Console.WriteLine($"Part2, start-of-message marker found at: {currentChar}");
    }

    private int FindMarkerInMessage(int markerLength, string message) {
        int currentChar = 0;
        LinkedList<char> buffer = new();

        foreach (char c in message) {
            if (buffer.Count == markerLength) {
                buffer.RemoveFirst();
            }
            buffer.AddLast(c);
            currentChar++;
            if (buffer.Distinct().Count() == markerLength) {
                return currentChar;
            }
        }
        return -1;
    }

}
