using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day13
{
    internal class Day13 : Day
    {
        public override void SolvePart1()
        {
            string root = "C:\\Users\\ARMSTRONG\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Days\\Day13\\Day13Input.txt";

            var input = File.ReadAllText(root);
            int packetsInOrder = ParsePacketsToJSON(input).Chunk(2).Select((pair, index) => CompareNodes(pair[0], pair[1]) < 0 ? index + 1 : 0).Sum();
            Console.WriteLine($"The number of in order packets: {packetsInOrder}");
        }

        public override void SolvePart2()
        {
            string root = "C:\\Users\\ARMSTRONG\\source\\repos\\AdventOfCode2022\\AdventOfCode2022\\Days\\Day13\\Day13Input.txt";
            var input = File.ReadAllText(root);
            var divider = ParsePacketsToJSON("[[2]]\r\n[[6]]").ToList();
            var packets = ParsePacketsToJSON(input).Concat(divider).ToList();
            packets.Sort(CompareNodes);
            int decoderKey = (packets.IndexOf(divider[0]) + 1) * (packets.IndexOf(divider[1]) + 1);
            Console.WriteLine($"The decoder key {decoderKey}");

        }

        private IEnumerable<JsonNode> ParsePacketsToJSON(string input) =>
            from line in input.Split("\r\n")
            where !string.IsNullOrEmpty(line)
            select JsonNode.Parse(line.ToString());


        private int CompareNodes(JsonNode nodeA, JsonNode nodeB)
        {
            if(nodeA is JsonValue && nodeB is JsonValue) 
            { 
                return (int)nodeA - (int)nodeB;
            } else
            {
                var arrayA = nodeA as JsonArray ?? new JsonArray((int)nodeA);
                var arrayB = nodeB as JsonArray ?? new JsonArray((int)nodeB);
                return Enumerable.Zip(arrayA, arrayB)
                    .Select(p => CompareNodes(p.First, p.Second))
                    .FirstOrDefault(c => c != 0, arrayA.Count - arrayB.Count);
            }
        }
    }
}
