using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml;

namespace AdventOfCode2025
{
    public class Day4 : Day
    {

        readonly (int, int)[] adjacentOffsets = new (int, int)[] { (-1, 1), (0, 1), (1, 1), (-1, 0), (1, 0), (-1, -1), (0, -1), (1, -1) };

        public long Part1(string[] lines)
        {
            bool[,] rollPapers = new bool[lines.Length, lines[0].Length];

            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < lines[i].Length; j++)
                    rollPapers[j, i] = lines[i][j] == '@';

            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < lines[i].Length; j++)
                    if (CountAllAdjacentIsRollPapper((j, i), rollPapers) < 4)
                        sum++;

            return sum;
        }

        public long Part2(string[] lines)
        {
            bool[,] rollPapers = new bool[lines.Length, lines[0].Length];

            int sum = 0;

            for (int i = 0; i < lines.Length; i++)
                for (int j = 0; j < lines[i].Length; j++)
                    rollPapers[j, i] = lines[i][j] == '@';


            List<(int, int)> adjacentToRemove = new();

            while (true)
            {
                for (int i = 0; i < lines.Length; i++)
                    for (int j = 0; j < lines[i].Length; j++)
                        if (CountAllAdjacentIsRollPapper((j, i), rollPapers) < 4)
                            adjacentToRemove.Add((j, i));

                if (adjacentToRemove.Count == 0)
                    break;

                sum += adjacentToRemove.Count;

                foreach (var adjacent in adjacentToRemove)
                    rollPapers[adjacent.Item1, adjacent.Item2] = false;

                adjacentToRemove = new();
            }

            return sum;
        }

        public int CountAllAdjacentIsRollPapper((int, int) index, bool[,] array)
        {
            if (!array[index.Item1, index.Item2])
                return int.MaxValue;

            int counter = 0;

            foreach (var adjacent in adjacentOffsets)
            {
                var x = index.Item1 + adjacent.Item1;
                var y = index.Item2 + adjacent.Item2;

                if (x < 0 || x >= MathF.Sqrt(array.Length))
                    continue;
                if (y < 0 || y >= MathF.Sqrt(array.Length))
                    continue;

                if (array[x, y])
                    counter++;
            }

            return counter;
        }
    }
}