namespace AdventOfCode2025
{
    public class Day7 : Day
    {
        public long Part1(string[] lines)
        {
            long spliterCounter = 0;

            List<int> currentBeamX = new();
            bool[,] splitersMap = new bool[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    var c = lines[i][j];

                    if (c == 'S')
                        currentBeamX.Add(j);
                    if (c == '^')
                        splitersMap[i, j] = true;
                }
            }
            for (int i = 0; i < lines.Length; i++)
            {
                List<int> newBeamsX = new();

                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (!currentBeamX.Contains(j))
                        continue;

                    if (splitersMap[i, j])
                    {
                        newBeamsX.Add(j - 1);
                        newBeamsX.Add(j + 1);
                        spliterCounter++;
                    }
                    else
                        newBeamsX.Add(j);

                }

                currentBeamX = newBeamsX;
            }

            return spliterCounter;
        }

        public long Part2(string[] lines)
        {
            long spliterCounter = 0;

            List<int> currentBeamX = new();
            bool[,] splitersMap = new bool[lines.Length, lines[0].Length];
            int[,] spliterCounterMap = new int[lines.Length, lines[0].Length];

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    var c = lines[i][j];

                    if (c == 'S')
                        currentBeamX.Add(j);
                    if (c == '^')
                        splitersMap[i, j] = true;

                    spliterCounterMap[i, j] = -1;
                }
            }

            if (currentBeamX.Count == 0)
                return 0;

            spliterCounter = SpliterTimeline(splitersMap, spliterCounterMap, currentBeamX[0], 0);

            return spliterCounter;
        }

        public int SpliterTimeline(bool[,] spliterMap, int[,] spliterCounterMap, int currentBeamX, int currentBeamY)
        {
            if (currentBeamY >= spliterMap.GetLength(0))
                return 1;

            if (spliterCounterMap[currentBeamY, currentBeamX] != -1)
                return spliterCounterMap[currentBeamY, currentBeamX];

            int splitersSum = 0;

            if (spliterMap[currentBeamY, currentBeamX])
            {
                splitersSum += SpliterTimeline(spliterMap, spliterCounterMap, currentBeamX - 1, currentBeamY + 1);
                splitersSum += SpliterTimeline(spliterMap, spliterCounterMap, currentBeamX + 1, currentBeamY + 1);
            }
            else
                splitersSum += SpliterTimeline(spliterMap, spliterCounterMap, currentBeamX, currentBeamY + 1);

            spliterCounterMap[currentBeamY, currentBeamX] = splitersSum;

            //7980502 - bad

            return splitersSum;
        }
    }
}