package main

import (
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
)

func main() {
	part1("input.txt")
}

func part1(inputFile string) {
	var sumOfPossibleIDs int = 0
	var maxRed, maxGreen, maxBlue int = 12, 13, 14

	data, err := os.ReadFile(inputFile)
	if err != nil {
		log.Fatal(err)
	}

	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		if line == "" {
			continue
		}
		parts := strings.Split(line, ":")
		gameID := extractGameID(parts[0])
		var gameIsPossible bool = true

		setsOfCubes := strings.Split(parts[1], ";")
		// check if all sets of cubes are do not exceed the max
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

		// fmt.Printf("GameID: %d, possible: %t\n", gameID, gameIsPossible)
	}

	fmt.Printf("Sum of possible IDs: %d\n", sumOfPossibleIDs)
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
