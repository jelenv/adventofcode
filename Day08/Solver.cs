using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day08 : ISolver {

    public void Run() {
        Console.WriteLine("Day08 solver");

        string[] input = File.ReadAllLines("Day08/input.txt");

        // Part1(input);
        Part2(input);
    }

    private void Part1(string[] input) {
        ParseIntoForest(input);
        CalcVisibilityFromOutside();
        Console.WriteLine($"Part1, trees visible from outside: {forest.Count(x => x.Visible)}");
    }

    private void Part2(string[] input) {
        ParseIntoForest(input);
        CalcScenicScores();
        Console.WriteLine($"Part2, best tree scenic score: {forest.Max(x => x.ScenicScore)}");
    }

    private record Tree {
        public Tree(int height, int col, int row, bool visible = false, int scenicScore = 0) {
            Height = height;
            Col = col;
            Row = row;
            Visible = visible;
            ScenicScore = scenicScore;
        }

        public int Height { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }
        public bool Visible { get; set; }
        public int ScenicScore { get; set; }
    }
    private readonly List<Tree> forest = new();

    // Parse input into a forest of trees
    private void ParseIntoForest(string[] input) {
        for (int row = 0; row < input.Length; row++) {
            for (int col = 0; col < input[row].Length; col++) {
                Tree currentTree = new(input[row][col], col, row, false);
                if (row == 0 || row == input.Length - 1) {
                    currentTree.Visible = true;
                }
                if (col == 0 || col == input[row].Length - 1) {
                    currentTree.Visible = true;
                }
                forest.Add(currentTree);
            }
        }
    }

    // Iterate over invisible trees and check if they are visible from any direction
    private void CalcVisibilityFromOutside() {
        foreach (var tree in forest.Where(x => !x.Visible)) {
            if (IsVisibleUp(tree)) {
                tree.Visible = true;
                continue;
            }

            if (IsVisibleLeft(tree)) {
                tree.Visible = true;
                continue;
            }

            if (IsVisibleRight(tree)) {
                tree.Visible = true;
                continue;
            }

            if (IsVisibleDown(tree)) {
                tree.Visible = true;
            }
        }
    }

    // Iterate over all trees and calculate their scenic score
    private void CalcScenicScores() {
        foreach (var tree in forest) {
            var takeWhileCondition = tree.Visible ? (Func<Tree, bool>)(x => x.Height < tree.Height) : _ => true;

            int treesUp = IsVisibleUp(tree) ? TreeCountUp(tree) : SmallerTreesUp(tree);
            int treesLeft = IsVisibleLeft(tree) ? TreeCountLeft(tree) : SmallerTreesLeft(tree);
            int treesRight = IsVisibleRight(tree) ? TreeCountRight(tree) : SmallerTreesRight(tree);
            int treesDown = IsVisibleDown(tree) ? TreeCountDown(tree) : SmallerTreesDown(tree);
            tree.ScenicScore = treesRight * treesLeft * treesUp * treesDown;
            if (tree.Col == 2 && tree.Row == 1) {
                Console.WriteLine($"isVis: {IsVisibleLeft(tree)}, TreeCount: {TreeCountLeft(tree)},"
                 + $" SmallerTrees: {SmallerTreesLeft(tree)}");
                Console.WriteLine(
                    $"treesUp: {treesUp}, treesLeft: {treesLeft}, "
                    + $"treesRight: {treesRight}, treesDown: {treesDown} = score {tree.ScenicScore}");

            }
        }
    }

    private bool IsVisibleUp(Tree tree) {
        return !forest.Any(x => x.Col == tree.Col && x.Row < tree.Row && x.Height >= tree.Height);
    }

    private bool IsVisibleLeft(Tree tree) {
        return !forest.Any(x => x.Col < tree.Col && x.Row == tree.Row && x.Height >= tree.Height);
    }

    private bool IsVisibleRight(Tree tree) {
        return !forest.Any(x => x.Col > tree.Col && x.Row == tree.Row && x.Height >= tree.Height);
    }

    private bool IsVisibleDown(Tree tree) {
        return !forest.Any(x => x.Col == tree.Col && x.Row > tree.Row && x.Height >= tree.Height);
    }

    private int SmallerTreesUp(Tree tree) {
        var count = forest.Where(x => x.Col == tree.Col && x.Row < tree.Row)
            .TakeWhile(x => x.Height < tree.Height).Count();
        if (!forest.Any(x => x.Col == tree.Col && x.Row + 1 == tree.Row && x.Height >= tree.Height)) {
            return count + 1;
        } else {
            return count;
        }
        //     return  ? count : count + 1;
        // return  ? count : count + 1;
    }

    private int SmallerTreesLeft(Tree tree) {
        var count = forest.Where(x => x.Col < tree.Col && x.Row == tree.Row)
            .TakeWhile(x => x.Height < tree.Height).Count();
        // return count == 1 ? count : count + 1;
        if (!forest.Any(x => x.Col+1 == tree.Col && x.Row == tree.Row && x.Height >= tree.Height)) {
            return count + 1;
        } else {
            return count;
        }
    }

    private int SmallerTreesRight(Tree tree) {
        var count = forest.Where(x => x.Col > tree.Col && x.Row == tree.Row)
            .TakeWhile(x => x.Height < tree.Height).Count();
        // return count == 1 ? count : count + 1;
        if (!forest.Any(x => x.Col == tree.Col+1 && x.Row == tree.Row && x.Height >= tree.Height)) {
            return count + 1;
        } else {
            return count;
        }
    }

    private int SmallerTreesDown(Tree tree) {
        var count = forest.Where(x => x.Col == tree.Col && x.Row > tree.Row)
            .TakeWhile(x => x.Height < tree.Height).Count();
        // return count == 1 ? count : count + 1;
        if (!forest.Any(x => x.Col == tree.Col && x.Row == tree.Row + 1 && x.Height >= tree.Height)) {
            return count + 1;
        } else {
            return count;
        }
    }

    private int TreeCountUp(Tree tree) {
        return forest.Count(x => x.Col == tree.Col && x.Row < tree.Row);
    }

    private int TreeCountLeft(Tree tree) {
        return  forest.Count(x => x.Col < tree.Col && x.Row == tree.Row);
    }

    private int TreeCountRight(Tree tree) {
        return forest.Count(x => x.Col > tree.Col && x.Row == tree.Row);
    }

    private int TreeCountDown(Tree tree) {
        return forest.Count(x => x.Col == tree.Col && x.Row > tree.Row);
    }

}
