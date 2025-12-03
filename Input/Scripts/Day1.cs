using System;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace AdventOfCode2025
{
    public class Day1 : Day
    {
        public long Part1(string[] lines)
        {
            int dialValue = 50;
            int zeroCounter = 0;

            foreach (var line in lines)
            {
                int value = int.Parse(line[1..]);
                if (line[0] == 'L')
                    dialValue -= value;
                if (line[0] == 'R')
                    dialValue += value;

                if (dialValue >= 100)
                    dialValue = dialValue % 100;

                while (dialValue < 0)
                    dialValue += 100;

                if (dialValue == 0)
                    zeroCounter++;
            }

            return zeroCounter;
        }

        public long Part2(string[] lines)
        {
            int dialValue = 50;
            int zeroCounter = 0;
            int dialLastValue = 50;

            foreach (var line in lines)
            {
                int value = int.Parse(line[1..]);
                bool preventOneClick = false;
                bool wasZeroCounted = false;

                if (line[0] == 'L')
                    dialValue -= value;
                if (line[0] == 'R')
                    dialValue += value;

                if (dialValue >= 100)
                {
                    zeroCounter += (int)(dialValue / 100f);
                    dialValue %= 100;
                    wasZeroCounted = true;
                }
                while (dialValue < 0)
                {
                    if (dialLastValue == 0 && !preventOneClick)
                    {
                        preventOneClick = true;
                        zeroCounter--;
                    }
                    dialValue += 100;
                    zeroCounter++;
                }

                if (dialValue == 0 && !wasZeroCounted)
                    zeroCounter++;

                dialLastValue = dialValue;
            }

            return zeroCounter;
        }
    }
}