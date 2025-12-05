using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace AdventOfCode2025
{
    public class Day3 : Day
    {
        public long Part1(string[] lines)
        {
            long sum = 0;
            foreach (var line in lines)
            {
                int[] numbersInLine = new int[line.Length];
                for (int i = 0; i < line.Length; i++)
                    numbersInLine[i] = int.Parse(line[i].ToString());

                int maxFirstNumber = numbersInLine[0..(numbersInLine.Length - 1)].Max();
                int posFirstNumber = GetIndexOfFirst(numbersInLine, maxFirstNumber) + 1;
                int maxSecondNumber = numbersInLine[posFirstNumber..numbersInLine.Length].Max();

                sum += maxFirstNumber * 10 + maxSecondNumber;
            }
            return sum;
        }

        public long Part2(string[] lines)
        {
            long sum = 0;
            foreach (var line in lines)
            {
                int[] numbersInLine = new int[line.Length];
                for (int i = 0; i < line.Length; i++)
                    numbersInLine[i] = int.Parse(line[i].ToString());

                int startingRange = 0;
                for (int i = 0; i < 12; i++)
                {
                    int maxNumber = numbersInLine[startingRange..(numbersInLine.Length - (11 - i))].Max();
                    startingRange = GetIndexOfFirst(numbersInLine, maxNumber, startingRange) + 1;

                    long value = maxNumber;

                    for (int k = i; k < 11; k++)
                        value *= 10;

                    sum += value;
                }
            }
            return sum;
        }

        private int GetIndexOfFirst(int[] array, int value, int startingRange = 0)
        {
            for (int i = startingRange; i < array.Length; i++)
                if (array[i] == value)
                    return i;
            return -1;
        }
    }
}