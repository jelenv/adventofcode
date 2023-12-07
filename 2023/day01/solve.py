# !/usr/bin/env python3

import time

digit_words = ['one', 'two', 'three', 'four', 'five', 'six', 'seven', 'eight', 'nine']
digits = ['1', '2', '3', '4', '5', '6', '7', '8', '9']

def main():
    start_time = time.time()
    part1('input.txt')
    part2('input.txt')
    end_time = time.time()

    elapsed_time = end_time - start_time
    elapsed_time_ms = (end_time - start_time) * 1000
    print(f"Elapsed time: {round(elapsed_time_ms, 4)}ms")

def part1(input_file: str):
    calibration_sum = 0

    with open(input_file, 'r') as file:
        for line in file:
            first_digit = lfind_digit(line)
            last_digit = rfind_digit(line)
            combined = str(first_digit) + str(last_digit)
            calibration_sum += int(combined)

    print(f'part1 result: {calibration_sum}')

def part2(input_file: str):
    calibration_sum = 0

    with open(input_file, 'r') as file:
        for line in file:
            first_digit = lfind_digit_part2(line)
            last_digit = rfind_digit_part2(line)
            combined = str(first_digit) + str(last_digit)
            calibration_sum += int(combined)

    print(f'part2 result: {calibration_sum}')

def lfind_digit(line):
    for char in line:
        if char.isdigit():
            return char

def rfind_digit(line):
    for char in line[::-1]:
        if char.isdigit():
            return char

def lfind_digit_part2(line):
    current_word = ''

    for char in line:
        if char.isdigit():
            return char
        if char.isalpha():
            current_word += char
            found = get_first_number_from_string(current_word)
            if found:
                return found

def rfind_digit_part2(line):
    current_word = ''

    for char in line[::-1]:
        if char.isdigit():
            return char
        if char.isalpha():
            current_word = char + current_word
            found = get_first_number_from_string(current_word)
            if found:
                return found

def get_first_number_from_string(input_string: str):
    result = ''

    for word in digit_words:
        if word in input_string:
            return digits[digit_words.index(word)]
    return result

if __name__ == '__main__':
    main()
