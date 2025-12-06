using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace AdventOfCode2025
{
    public class Day5 : Day
    {
        public long Part1(string[] lines)
        {
            int freshFood = 0;

            List<(long, long)> ranges = new();
            List<long> foodID = new();

            foreach (var line in lines)
            {
                if (line.Contains('-'))
                {
                    var left_right = line.Split('-');
                    ranges.Add((long.Parse(left_right[0]), long.Parse(left_right[1])));
                }
                else if (line != string.Empty)
                    foodID.Add(long.Parse(line));
            }

            foreach (var food in foodID)
            {
                if (ranges.Any(x => food >= x.Item1 && food <= x.Item2))
                    freshFood++;
            }
            return freshFood;
        }

        public long Part2(string[] lines)
        {
            long freshFoodCount = 0;

            List<(long, long)> ranges = new();

            foreach (var line in lines)
            {
                if (line.Contains('-'))
                {
                    var left_right = line.Split('-');
                    ranges.Add((long.Parse(left_right[0]), long.Parse(left_right[1])));
                }
            }

            for (int i = 0; i < ranges.Count; i++)
            {
                for (int j = 0; j < ranges.Count; j++)
                {
                    if (i == j)
                        continue;

                    bool anyMod = false;
                    bool toDelete = true;

                    if (ranges[i].Item1 >= ranges[j].Item1 && ranges[i].Item1 <= ranges[j].Item2)
                    {
                        if (ranges[i].Item2 > ranges[j].Item2)
                        {
                            ranges[i] = (ranges[j].Item1, ranges[i].Item2);
                            toDelete = false;
                        }
                        anyMod = true;
                    }
                    if (ranges[i].Item2 >= ranges[j].Item1 && ranges[i].Item2 <= ranges[j].Item2)
                    {
                        if (ranges[i].Item1 < ranges[j].Item1)
                        {
                            ranges[i] = (ranges[i].Item1, ranges[j].Item2);
                            toDelete = false;
                        }
                        anyMod = true;
                    }
                    if (anyMod)
                    {
                        if (anyMod && toDelete)
                            ranges.RemoveAt(i);
                        else if (anyMod)
                            ranges.RemoveAt(j);
                        i = -1;
                        j = -1;
                        break;
                    }
                }
            }
            ranges = ranges.OrderBy(x => x.Item1).ToList();
            foreach (var range in ranges)
                freshFoodCount += (range.Item2 - range.Item1) + 1;

            return freshFoodCount;
        }
    }
}