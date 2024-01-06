package main

import (
	"aoc23/utils"
	"fmt"
	"strings"
)

func main() {
	part1("test.txt")
	// part2("input.txt")
}

func part1(inputFile string) {
	data := utils.ReadFileData(inputFile)
	lowestLocation := 0

	lines := strings.Split(data, "\n")
	for _, line := range lines {
		// TODO: implement
	}

	fmt.Printf("Lowest location number (part1): %d\n", lowestLocation)
}

// func part2(inputFile string) {
// 	// data := utils.ReadFileData(inputFile)

// 	// fmt.Printf("Num of scratch cards (part2): %d\n", numOfScratchCards)
// }
