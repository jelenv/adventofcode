package main

import (
	"aoc23/utils"
	"fmt"
	"strings"
)

func main() {
	inputFilePath := "../../aoc-inputs/2023/d05/input.txt"
	part1(inputFilePath)
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
