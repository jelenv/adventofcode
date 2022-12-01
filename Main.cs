// See https://aka.ms/new-console-template for more information
using System;

namespace AOC;

static class SolverFactory {

    public static ISolver Create(string day) {
        return day switch {
            "day01" => new Day01(),
            _ => throw new Exception($"Unknown day: {day}")
        };
    }
}

static class Program {

    static void Main(string[] args) {
        var solver = SolverFactory.Create("day01");
        solver.Run();
    }

}
