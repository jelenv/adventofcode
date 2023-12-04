package main

import (
	"aoc23/utils"
	"fmt"
	"log"
	"strconv"
	"strings"
)

func main() {
	part1("input.txt")
	part2("input.txt")
}

func part1(inputFile string) {
	data := utils.ReadFileData(inputFile)

	var sumOfPossibleIDs int = 0
	var maxRed, maxGreen, maxBlue int = 12, 13, 14

	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		if line == "" {
			continue
		}
		parts := strings.Split(line, ":")
		gameID := extractGameID(parts[0])
		var gameIsPossible bool = true

		setsOfCubes := strings.Split(parts[1], ";")
		for _, setOfCubes := range setsOfCubes {
			cubes := strings.Split(setOfCubes, ",")
			for _, cube := range cubes {
				cube = strings.TrimSpace(cube)
				cubeParts := strings.Split(cube, " ")
				value, _ := strconv.Atoi(strings.TrimSpace(cubeParts[0]))
				color := strings.TrimSpace(cubeParts[1])

				if (color == "red" && value > maxRed) || (color == "green" && value > maxGreen) || (color == "blue" && value > maxBlue) {
					gameIsPossible = false
					break
				}
			}
			if !gameIsPossible {
				break
			}
		}

		if gameIsPossible {
			sumOfPossibleIDs += gameID
		}
	}

	fmt.Printf("Sum of possible IDs: %d\n", sumOfPossibleIDs)
}

func part2(inputFile string) {
	data := utils.ReadFileData(inputFile)

	var sumOfPowers int = 0

	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		if line == "" {
			continue
		}
		parts := strings.Split(line, ":")
		var maxRed, maxGreen, maxBlue int = 0, 0, 0

		setsOfCubes := strings.Split(parts[1], ";")
		for _, setOfCubes := range setsOfCubes {
			cubes := strings.Split(setOfCubes, ",")
			for _, cube := range cubes {
				cube = strings.TrimSpace(cube)
				cubeParts := strings.Split(cube, " ")
				value, _ := strconv.Atoi(strings.TrimSpace(cubeParts[0]))
				color := strings.TrimSpace(cubeParts[1])

				if color == "red" && value > maxRed {
					maxRed = value
				}
				if color == "green" && value > maxGreen {
					maxGreen = value
				}
				if color == "blue" && value > maxBlue {
					maxBlue = value
				}
			}
		}
		sumOfPowers += maxRed * maxGreen * maxBlue
	}

	fmt.Printf("Sum of powers: %d\n", sumOfPowers)
}

func extractGameID(gameIDPart string) int {
	parts := strings.Split(gameIDPart, " ")
	if len(parts) == 2 {
		gameID := parts[1]
		result, _ := strconv.Atoi(gameID)
		return result
	}

	log.Fatal("Could not extract gameID from part: " + gameIDPart)
	return -1
}
