using System.Numerics;

namespace AdventOfCode2025
{
    public class Day8 : Day
    {
        public long Part1(string[] lines)
        {
            int howManyPairs = 1000;

            SortedList<long, (int, int)> shortestPair = new();
            List<Circuit> circuits = new();
            Vector3[] points = new Vector3[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var numbs = lines[i].Split(",");
                points[i] = new Vector3(int.Parse(numbs[0]), int.Parse(numbs[1]), int.Parse(numbs[2]));
            }
            for (int i = 0; i < points.Length; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    var dist = (long)Vector3.DistanceSquared(points[i], points[j]);
                    while (shortestPair.ContainsKey(dist))
                        dist++;
                    shortestPair.Add(dist, (i, j));
                }
            }

            for (int i = 0; i < howManyPairs; i++)
            {
                var pair = shortestPair.Values[i];

                var circuitA = circuits.FirstOrDefault(c => c.Contains(pair.Item1));
                var circuitB = circuits.FirstOrDefault(c => c.Contains(pair.Item2));

                if (circuitA != null && circuitA == circuitB)
                    continue;
                if (circuitA != null && circuitB != null)
                {
                    foreach (var pointID in circuitB.Points)
                        circuitA.AddPoint(pointID);
                    circuits.Remove(circuitB);
                }
                else if (circuitA == null && circuitB == null)
                {
                    Circuit newCircuit = new();
                    newCircuit.AddPoint(pair.Item1);
                    newCircuit.AddPoint(pair.Item2);
                    circuits.Add(newCircuit);
                }
                else if (circuitA != null)
                    circuitA.AddPoint(pair.Item2);
                else if (circuitB != null)
                    circuitB.AddPoint(pair.Item1);
            }

            circuits = circuits.OrderByDescending(c => c.Count).ToList();

            return circuits[0].Count * circuits[1].Count * circuits[2].Count;
        }

        public long Part2(string[] lines)
        {
            SortedList<long, (int, int)> shortestPair = new();
            List<Circuit> circuits = new();
            Vector3[] points = new Vector3[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                var numbs = lines[i].Split(",");
                points[i] = new Vector3(int.Parse(numbs[0]), int.Parse(numbs[1]), int.Parse(numbs[2]));
            }
            for (int i = 0; i < points.Length; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    var dist = (long)Vector3.DistanceSquared(points[i], points[j]);
                    while (shortestPair.ContainsKey(dist))
                        dist++;
                    shortestPair.Add(dist, (i, j));
                }
            }

            int c = -1;
            (int,int) lastJunctionBox = (-1, -1);
            while (!circuits.Any(x => x.Count == points.Length))
            {
                c++;
                var pair = shortestPair.Values[c];

                var circuitA = circuits.FirstOrDefault(c => c.Contains(pair.Item1));
                var circuitB = circuits.FirstOrDefault(c => c.Contains(pair.Item2));

                lastJunctionBox = pair;

                if (circuitA != null && circuitA == circuitB)
                    continue;
                if (circuitA != null && circuitB != null)
                {
                    foreach (var pointID in circuitB.Points)
                        circuitA.AddPoint(pointID);
                    circuits.Remove(circuitB);
                }
                else if (circuitA == null && circuitB == null)
                {
                    Circuit newCircuit = new();
                    newCircuit.AddPoint(pair.Item1);
                    newCircuit.AddPoint(pair.Item2);
                    circuits.Add(newCircuit);
                }
                else if (circuitA != null)
                    circuitA.AddPoint(pair.Item2);
                else if (circuitB != null)
                    circuitB.AddPoint(pair.Item1);
            }

            return (long)(points[lastJunctionBox.Item1].X * points[lastJunctionBox.Item2].X);
        }

        public class Circuit
        {
            private List<int> _pointsID = new();

            public IEnumerable<int> Points => _pointsID;

            public void AddPoint(int x) => _pointsID.Add(x);
            public bool Contains(int x) => _pointsID.Contains(x);
            public int Count => _pointsID.Count;
        }
    }
}