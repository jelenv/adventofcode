using System;

namespace AOC;

static class SolverFactory {

    public static ISolver Create(string day) {
        return day switch {
            "01" => new Day01(),
            "02" => new Day02(),
            "03" => new Day03(),
            "04" => new Day04(),
            "05" => new Day05(),
            "06" => new Day06(),
            "07" => new Day07(),
            "08" => new Day08(),
            "09" => new Day09(),
            _ => throw new Exception($"Unknown day: {day}")
        };
    }
}

static class Program {

    static void Main(string[] args) {
        if (args.Length < 1) {
            throw new ArgumentException("Please provide a day number (1-25)");
        }
        var dayNumber = int.Parse(args[0]);
        var solver = SolverFactory.Create($"{dayNumber:D2}");
        solver.Run();
    }

}
