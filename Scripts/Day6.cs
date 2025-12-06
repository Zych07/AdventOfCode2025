namespace AdventOfCode2025
{
    public class Day6 : Day
    {
        public long Part1(string[] lines)
        {
            long sum = 0;
            int[,] numbers = new int[lines.Length - 1, 1000];
            List<string> signs = new();

            for (int i = 0; i < lines.Length - 1; i++)
            {
                var line = lines[i].Split(' ');

                int idx = 0;
                foreach (var number in line)
                {
                    if (number.Length == 0)
                        continue;

                    numbers[i, idx] = int.Parse(number);
                    idx++;
                }
            }
            var signLine = lines[lines.Length - 1].Split(' ');
            foreach (var sign in signLine)
                if (sign.Length > 0)
                    signs.Add(sign);

            for (int i = 0; i < signs.Count; i++)
            {
                long sumRow = 0;

                for (int j = 0; j < lines.Length - 1; j++)
                {
                    if (signs[i] == "*")
                    {
                        if (sumRow == 0)
                            sumRow = 1;

                        sumRow *= numbers[j, i];
                    }
                    if (signs[i] == "+")
                        sumRow += numbers[j, i];
                }

                sum += sumRow;
            }

            return sum;
        }

        public long Part2(string[] lines)
        {
            long sum = 0;
            List<string> signs = new();

            float maxInLine = lines.Max(x => x.Length);

            var signLine = lines[lines.Length - 1].Split(' ');
            foreach (var sign in signLine)
                if (sign.Length > 0)
                    signs.Add(sign);

            int signCounter = 0;
            long sumRow = 0;

            for (int i = 0; i < maxInLine; i++)
            {
                string num = string.Empty;
                for (int j = 0; j < lines.Length - 1; j++)
                    num += lines[j][i];

                long number = 0;

                if (!long.TryParse(num, out number))
                {
                    sum += sumRow;
                    sumRow = 0;
                    signCounter++;
                }
                else if (sumRow == 0)
                    sumRow = number;
                else if (signs[signCounter] == "*")
                    sumRow *= number;
                else if (signs[signCounter] == "+")
                    sumRow += number;
            }
            sum += sumRow;

            return sum;
        }
    }
}