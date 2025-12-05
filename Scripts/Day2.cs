using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace AdventOfCode2025
{
    public class Day2 : Day
    {
        public long Part1(string[] lines)
        {
            var ranges = lines[0].Split(',');

            long counter = 0;

            foreach (var range in ranges)
            {
                var r = range.Split('-');
                string leftRange = r[0];
                string rightRange = r[1];

                long leftValue = long.Parse(leftRange);
                long rightValue = long.Parse(rightRange);

                for (long i = leftValue; i <= rightValue; i++)
                {
                    string text = i.ToString();

                    int lenght = (text.Length / 2);
                    var sequence = text[0..lenght];

                    if (text[lenght..text.Length].Equals(sequence))
                        counter += long.Parse(text);
                }
            }
            return counter;
        }

        public long Part2(string[] lines)
        {
            var ranges = lines[0].Split(',');

            long counter = 0;

            foreach (var range in ranges)
            {
                var r = range.Split('-');
                string leftRange = r[0];
                string rightRange = r[1];

                long leftValue = long.Parse(leftRange);
                long rightValue = long.Parse(rightRange);

                for (long i = leftValue; i <= rightValue; i++)
                {
                    string text = i.ToString();
                    int lenght = (text.Length / 2);

                    bool sequenceFound = false;

                    for (int l = 1; l <= lenght; l++)
                    {
                        var sequence = text[0..l];

                        sequenceFound = true;

                        for (int k = l; k < text.Length; k += l)
                        {
                            if (k + l > text.Length)
                            {
                                sequenceFound = false;
                                continue;
                            }

                            if (!sequence.Equals(text[k..(k + l)]))
                                sequenceFound = false;
                        }
                        if (sequenceFound)
                            break;
                    }

                    if (sequenceFound)
                        counter += long.Parse(text);
                }
            }
            return counter;
        }
    }
}