package main

import (
	"aoc23/utils"
	"fmt"
	"strings"
)

func main() {
	part1("test.txt")
	part2("test.txt")
}

func part1(inputFile string) {
	data := utils.ReadFileData(inputFile)

	lines := strings.Split(data, "\n")
	for _, line := range lines {
		// split line by : this gets us card and numbers
		parts := strings.Split(line, ":")
		card := parts[0]
		numbers := parts[1]
		parts = strings.Split(numbers, "|")

	}

	fmt.Printf("Data part1: %s\n", data)
}

func part2(inputFile string) {
	data := utils.ReadFileData(inputFile)
	fmt.Printf("Data part1: %s\n", data)
}
