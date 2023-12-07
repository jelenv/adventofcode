package main

import (
	"fmt"
	"log"
	"os"
	"strconv"
	"strings"
	"time"
	"unicode"
)

var digitWords = []string{"one", "two", "three", "four", "five", "six", "seven", "eight", "nine"}
var digits = []rune{'1', '2', '3', '4', '5', '6', '7', '8', '9'}

func main() {
	startTime := time.Now()

	part1Result, err := part1("input.txt")
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("part1 result: %d\n", part1Result)

	part2Result, err := part2("input.txt")
	if err != nil {
		log.Fatal(err)
	}
	fmt.Printf("part2 result: %d\n", part2Result)

	elapsed := time.Since(startTime)
	elapsedMilliseconds := float64(elapsed.Milliseconds()) + float64(elapsed.Nanoseconds()%1e6)/1e6
	fmt.Printf("Elapsed time: %fms\n", elapsedMilliseconds)
}

func part1(inputFile string) (int, error) {
	calibrationSum := 0

	data, err := os.ReadFile(inputFile)
	if err != nil {
		return 0, err
	}

	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		firstDigit := lfindDigit([]rune(line))
		lastDigit := rfindDigit([]rune(line))
		combined, _ := strconv.Atoi(string(firstDigit) + string(lastDigit))
		calibrationSum += combined
	}

	return calibrationSum, nil
}

func part2(inputFile string) (int, error) {
	calibrationSum := 0

	data, err := os.ReadFile(inputFile)
	if err != nil {
		return 0, err
	}

	lines := strings.Split(string(data), "\n")
	for _, line := range lines {
		firstDigit := lfindDigitPart2([]rune(line))
		lastDigit := rfindDigitPart2([]rune(line))
		combined, _ := strconv.Atoi(string(firstDigit) + string(lastDigit))
		calibrationSum += combined
	}

	return calibrationSum, nil
}

func lfindDigit(line []rune) rune {
	for _, char := range line {
		if unicode.IsDigit(char) {
			return char
		}
	}
	return 0
}

func rfindDigit(line []rune) rune {
	for i := len(line) - 1; i >= 0; i-- {
		if unicode.IsDigit(line[i]) {
			return line[i]
		}
	}
	return 0
}

func lfindDigitPart2(line []rune) rune {
	var currentWord []rune

	for _, char := range line {
		if unicode.IsDigit(char) {
			return char
		}
		if unicode.IsLetter(char) {
			currentWord = append(currentWord, char)
			found := getFirstNumberFromString(currentWord)
			if found != 0 {
				return found
			}
		}
	}
	return 0
}

func rfindDigitPart2(line []rune) rune {
	var currentWord []rune

	for i := len(line) - 1; i >= 0; i-- {
		char := line[i]
		if unicode.IsDigit(char) {
			return char
		}
		if unicode.IsLetter(char) {
			currentWord = append([]rune{char}, currentWord...)
			found := getFirstNumberFromString(currentWord)
			if found != 0 {
				return found
			}
		}
	}
	return 0
}

func getFirstNumberFromString(inputString []rune) rune {
	for idx, word := range digitWords {
		if strings.Contains(string(inputString), word) {
			return digits[idx]
		}
	}
	return 0
}
