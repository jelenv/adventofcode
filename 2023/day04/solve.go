package main

import (
	"aoc23/utils"
	"fmt"
)

func main() {
	part1("test.txt")
	part2("test.txt")
}

func part1(inputFile string) {
	data := utils.ReadFileData(inputFile)
	fmt.Printf("Data part1: %s\n", data)
}

func part2(inputFile string) {
	data := utils.ReadFileData(inputFile)
	fmt.Printf("Data part1: %s\n", data)
}
