// See https://aka.ms/new-console-template for more information
using System;

namespace AOC;

static class SolverFactory {

    public static ISolver Create(string day) {
        return day switch {
            "day01" => new Day01(),
            "day02" => new Day02(),
            _ => throw new Exception($"Unknown day: {day}")
        };
    }
}

static class Program {

    static void Main(string[] args) {
        var solver = SolverFactory.Create("day02");
        solver.Run();
    }

}
