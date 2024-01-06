package main

import (
	"aoc23/utils"
	"fmt"
	"strings"
)

func main() {
	part1("input.txt")
	part2("input.txt")
}

func part1(inputFile string) {
	data := utils.ReadFileData(inputFile)
	sumOfPoints := 0

	lines := strings.Split(data, "\n")
	for _, line := range lines {
		parts := strings.Split(line, ":")
		numbers := parts[1]
		parts = strings.Split(numbers, "|")
		winningNumbers := strings.Split(strings.TrimSpace(parts[0]), " ")
		winningNumbers = removeEmpty(winningNumbers)
		myNumbers := strings.Split(strings.TrimSpace(parts[1]), " ")
		myNumbers = removeEmpty(myNumbers)

		commonNumbers := uniqueIntersection(winningNumbers, myNumbers)

		if len(commonNumbers) > 0 {
			cardPoints := calculatePoints(commonNumbers)
			sumOfPoints += cardPoints
		}
	}

	fmt.Printf("Sum of points (part1): %d\n", sumOfPoints)
}

func part2(inputFile string) {
	data := utils.ReadFileData(inputFile)
	numOfScratchCards := 0
	lines := strings.Split(data, "\n")

	// map index and number of scratch cards for this index
	scratchCards := make(map[int]int)
	for i := range lines {
		// init all scratch cards to 1
		scratchCards[i] = 1
	}

	for i, line := range lines {
		parts := strings.Split(line, ":")
		parts = strings.Split(parts[1], "|")

		winningNumbers := removeEmpty(strings.Split(strings.TrimSpace(parts[0]), " "))
		myNumbers := removeEmpty(strings.Split(strings.TrimSpace(parts[1]), " "))
		commonNumbers := uniqueIntersection(winningNumbers, myNumbers)

		if len(commonNumbers) > 0 {
			for x := 0; x < scratchCards[i]; x++ {
				// create copies of next num of scratch cards
				for j := 1; j <= len(commonNumbers); j++ {
					scratchCards[i+j]++
				}
			}
		}
	}

	// count all scratch cards in the map
	for _, v := range scratchCards {
		numOfScratchCards += v
	}

	fmt.Printf("Num of scratch cards (part2): %d\n", numOfScratchCards)
}

func calculatePoints(numbers []string) int {
	points := 0
	for i := range numbers {
		if i == 0 {
			points++
		} else {
			points *= 2
		}
	}
	return points
}

func removeEmpty(s []string) []string {
	var r []string
	for _, str := range s {
		if str != "" {
			r = append(r, str)
		}
	}
	return r
}

func uniqueIntersection(a, b []string) []string {
	m := make(map[string]bool)
	for _, item := range a {
		m[item] = true
	}

	var result []string
	seen := make(map[string]bool)
	for _, item := range b {
		if _, ok := m[item]; ok {
			if _, alreadySeen := seen[item]; !alreadySeen {
				result = append(result, item)
				seen[item] = true
			}
		}
	}
	return result
}
