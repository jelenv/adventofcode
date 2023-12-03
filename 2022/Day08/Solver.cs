using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day08 : ISolver {

    public void Run() {
        Console.WriteLine("Day08 solver");

        string[] input = File.ReadAllLines("Day08/input.txt");
        Part1(input);
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
        forest.Clear();
        for (int row = 0; row < input.Length; row++) {
            for (int col = 0; col < input[row].Length; col++) {
                int height = int.Parse(input[row][col].ToString());
                Tree currentTree = new(height, col, row, false);
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
            int treesUp = VisibleTreesUp(tree);
            int treesLeft = VisibleTreesLeft(tree);
            int treesRight = VisibleTreesRight(tree);
            int treesDown = VisibleTreesDown(tree);
            tree.ScenicScore = treesRight * treesLeft * treesUp * treesDown;
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

    private int VisibleTreesUp(Tree tree) {
        var trees = forest.Where(x => x.Col == tree.Col && x.Row < tree.Row).OrderByDescending(x => x.Row).ToList();
        return CountVisibleTrees(tree, trees);
    }

    private int VisibleTreesLeft(Tree tree) {
        var trees = forest.Where(x => x.Col < tree.Col && x.Row == tree.Row).OrderByDescending(x => x.Col).ToList();
        return CountVisibleTrees(tree, trees);
    }

    private int VisibleTreesRight(Tree tree) {
        var trees = forest.Where(x => x.Col > tree.Col && x.Row == tree.Row).OrderBy(x => x.Col).ToList();
        return CountVisibleTrees(tree, trees);
    }

    private int VisibleTreesDown(Tree tree) {
        var trees = forest.Where(x => x.Col == tree.Col && x.Row > tree.Row).OrderBy(x => x.Row).ToList();
        return CountVisibleTrees(tree, trees);
    }

    private static int CountVisibleTrees(Tree tree, List<Tree> trees) {
        int visibleTrees = 0;
        foreach (Tree tr in trees) {
            visibleTrees++;
            if (tr.Height >= tree.Height) {
                break;
            }
        }
        return visibleTrees;
    }
}
