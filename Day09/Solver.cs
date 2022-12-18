using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day09 : ISolver {

    public void Run() {
        Console.WriteLine("Day09 solver");

        string[] input = File.ReadAllLines("Day09/input.txt");
        Part1(input);
        Part2(input);
    }

    private void Part1(string[] input) {
        var tails = TailPositions(input, 2).ToHashSet();
        Console.WriteLine($"Part1, tail positions number: {tails.Count}");
    }

    private void Part2(string[] input) {
        var tails = TailPositions(input, 10).ToHashSet();
        Console.WriteLine($"Part2, tail positions number: {tails.Count}");
    }

    private record Position(int X, int Y);

    private IEnumerable<Position> TailPositions(string[] input, int ropeLength) {
        var rope = Enumerable.Repeat(new Position(0, 0), ropeLength).ToArray();
        yield return rope.Last();

        foreach (string line in input) {
            var parts = line.Split(' ');
            var move = parts[0];
            var length = int.Parse(parts[1]);

            for (int i = 0; i < length; i++) {
                MoveRope(rope, move);
                yield return rope.Last();
            }
        }
    }

    private void MoveRope(Position[] rope, string move) {
        // move head of the rope
        switch (move) {
            case "U":
                rope[0] = new Position(rope[0].X, rope[0].Y - 1);
                break;
            case "D":
                rope[0] = new Position(rope[0].X, rope[0].Y + 1);
                break;
            case "L":
                rope[0] = new Position(rope[0].X - 1, rope[0].Y);
                break;
            case "R":
                rope[0] = new Position(rope[0].X + 1, rope[0].Y);
                break;
        }
        // move other parts of the rope to be adjacent to the head
        for (int i = 1; i < rope.Length; i++) {
            var xDiff = rope[i - 1].X - rope[i].X;
            var yDiff = rope[i - 1].Y - rope[i].Y;

            if (Math.Abs(xDiff) > 1 || Math.Abs(yDiff) > 1) {
                rope[i] = new Position(rope[i].X + Math.Sign(xDiff), rope[i].Y + Math.Sign(yDiff));
            }
        }
    }
}
