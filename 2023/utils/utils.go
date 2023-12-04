package utils

import (
	"log"
	"os"
	"strings"
)

func ReadFileData(inputFile string) string {
	data, err := os.ReadFile(inputFile)
	if err != nil {
		log.Fatal(err)
	}
	return strings.TrimSpace(string(data))
}
