using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC;

class Day02 : ISolver {

    public void Run() {
        Console.WriteLine("Day02 solver");
        // player1 (opponent): A = rock, B = paper, C = scissors
        // player2: X = rock, Y = paper, Z = scissors
        // A beats Z/C, B beats X/A, C beats Y/B
        // score (selected): X = 1, Y = 2, Z = 3
        // score (outcome): 6 for win, 3 for draw, 0 for loss

        string[] lines = File.ReadAllLines("Day02/input.txt");
        List<char> player1 = new();
        List<char> player2 = new();
        foreach (string line in lines) {
            string[] strategy = line.Split(" ");
            if (strategy.Length == 2) {
                player1.Add(strategy[0][0]);
                player2.Add(strategy[1][0]);
            } else {
                throw new Exception("Invalid strategy guide: " + line);
            }
        }

        Part1(player1.ToArray(), player2.ToArray());
        Part2(player1.ToArray(), player2.ToArray());
    }

    private readonly Dictionary<char, int> selectionScoreMap = new() {
        { 'X', 1 },
        { 'Y', 2 },
        { 'Z', 3 },
        { 'A', 1 },
        { 'B', 2 },
        { 'C', 3 },
    };

    private void Part1(char[] player1, char[] player2) {
        int totalScore = 0;

        for (int i = 0; i < player1.Length; i++) {
            int player1Score = selectionScoreMap[player1[i]];
            int player2Score = selectionScoreMap[player2[i]];
            totalScore += player2Score;
            // 0 = draw, 1 or -2 = win, else = loss
            int diff = player2Score - player1Score;
            if (diff == 0) {
                totalScore += 3; // draw
            } else if (diff == -2 || diff == 1) {
                totalScore += 6; // win
            } else {
                totalScore += 0; // loss
            }
        }
        Console.WriteLine("My total score: " + totalScore);
    }

    private void Part2(char[] player1, char[] player2) {
        int totalScore = 0;
        // X = lose, Y = draw, Z = win
        var outcomeScoreMap = new Dictionary<char, int> {
            { 'X', 0 },
            { 'Y', 3 },
            { 'Z', 6 },
        };

        for (int i = 0; i < player1.Length; i++) {
            int player1Score = selectionScoreMap[player1[i]];
            int player2Score;

            if (player2[i] == 'Y') {
                player2Score = player1Score;
            } else if (player2[i] == 'Z') {
                var score = (player1Score + 1) % 3;
                player2Score = score == 0 ? 3 : score;
            } else {
                var score = (player1Score + 2) % 3;
                player2Score = score == 0 ? 3 : score;
            }

            totalScore += player2Score;
            totalScore += outcomeScoreMap[player2[i]];
        }
        Console.WriteLine("My total score: " + totalScore);
    }
}
