package main

import (
	"aoc23/utils"
	"fmt"
	"strconv"
	"strings"
	"unicode"
)

type Part struct {
	id     string
	startX int
	endX   int
	y      int
}

type Symbol struct {
	char string
	x    int
	y    int
}

func main() {
	part1("input.txt")
	part2("input.txt")
}

func part1(inputFile string) {
	data := utils.ReadFileData(inputFile)
	parts, symbols := scanTheSchematic(data)

	var sumOfParts int = 0
	for _, part := range parts {
		// check if there is a symbol near the part
		for _, symbol := range symbols {
			if symbol.x >= part.startX-1 && symbol.x <= part.endX+1 && symbol.y >= part.y-1 && symbol.y <= part.y+1 {
				idNumber, _ := strconv.Atoi(part.id)
				sumOfParts += idNumber
				break
			}
		}
	}
	fmt.Printf("Sum of parts: %d (should be 528819 for input.txt)\n", sumOfParts)
}

func part2(inputFile string) {
	data := utils.ReadFileData(inputFile)
	parts, symbols := scanTheSchematic(data)

	var sumOfGearRatios int = 0
	for _, symbol := range symbols {
		if symbol.char == "*" {
			var adjacentParts []Part = make([]Part, 0)
			for _, part := range parts {
				if symbol.x >= part.startX-1 && symbol.x <= part.endX+1 && symbol.y >= part.y-1 && symbol.y <= part.y+1 {
					adjacentParts = append(adjacentParts, part)
					if len(adjacentParts) > 2 {
						break
					}
				}
			}
			if len(adjacentParts) == 2 {
				part1, _ := strconv.Atoi(adjacentParts[0].id)
				part2, _ := strconv.Atoi(adjacentParts[1].id)
				sumOfGearRatios += part1 * part2
			}
		}
	}
	fmt.Printf("Sum of gear ratios: %d (should be 80403602 for input.txt)\n", sumOfGearRatios)
}

func scanTheSchematic(schematic string) ([]Part, []Symbol) {
	var parts []Part = make([]Part, 0)
	var symbols []Symbol = make([]Symbol, 0)

	lines := strings.Split(schematic, "\n")
	for y, line := range lines {
		var currentPart Part = initPart(y)
		for x, char := range line {
			if isSymbol(char) {
				symbols = append(symbols, Symbol{string(char), x, y})
				if currentPart.startX != -1 {
					parts = append(parts, currentPart)
					currentPart = initPart(y)
				}
			} else if unicode.IsDigit(char) {
				if currentPart.startX == -1 {
					currentPart.startX = x
				}
				currentPart.endX = x
				currentPart.id += string(char)
			} else {
				if currentPart.startX != -1 {
					parts = append(parts, currentPart)
					currentPart = initPart(y)
				}
			}
		}
	}
	return parts, symbols
}

func isSymbol(char rune) bool {
	return !unicode.IsDigit(char) && char != '.' && !unicode.IsControl(char)
}

func initPart(yy int) Part {
	return Part{id: "", startX: -1, endX: -1, y: yy}
}
