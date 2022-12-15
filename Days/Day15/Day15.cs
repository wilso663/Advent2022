using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Days.Day15
{
    internal class Day15 : Day
    {

        public HashSet<(int sx,int sy,int distance)> sensorSet = new HashSet<(int sensor_x,int sensor_y,int distance)>();
        public ISet<(int bx, int by)> beaconSet = new HashSet<(int beacon_x, int beacon_y)>();
        private int distanceSum = 0;
        public override void SolvePart1()
        {
            var input = this.ReadLines();
            foreach(var line in input)
            {
                //mega lazy parse. gotta go fast.
                var words = line.Split(' ');
                var sensorXString = words[2];
                var sensorYString = words[3];
                var beaconXString = words[8];
                var beaconYString = words[9];
                var sensorX = int.Parse(sensorXString[2..^1]);
                var sensorY = int.Parse(sensorYString[2..^1]);
                var beaconX = int.Parse(beaconXString[2..^1]);
                var beaconY = int.Parse(beaconYString[2..]);
                var distance = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);
                //Console.WriteLine($"SensorX:{sensorX} SensorY:{sensorY} beaconX:{beaconX} beaconY:{beaconY} ");
                distanceSum += distance;
                sensorSet.Add((sensorX,sensorY,distance));
                beaconSet.Add((beaconX, beaconY));
            }
            var positions = 0;
            //Arbitrary big numbers again. It just works.
            for(int i = -10000000; i <= 10000000; i++)
            {
                var y = 2000000;
                if (!isValid(i, y, sensorSet) && !beaconSet.Contains((i, y)))
                    positions += 1;
            }
            Console.WriteLine($"Positions without beacon on y:2e6 {positions}");
        }

        public override void SolvePart2()
        {
            var numberChecked = 0;
            var foundBeacon = false;
            var input = this.ReadLines();
            foreach (var line in input)
            {
                //mega lazy parse. gotta go fast.
                var words = line.Split(' ');
                var sensorXString = words[2];
                var sensorYString = words[3];
                var beaconXString = words[8];
                var beaconYString = words[9];
                var sensorX = int.Parse(sensorXString[2..^1]);
                var sensorY = int.Parse(sensorYString[2..^1]);
                var beaconX = int.Parse(beaconXString[2..^1]);
                var beaconY = int.Parse(beaconYString[2..]);
                var distance = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);
                distanceSum += distance;
                sensorSet.Add((sensorX, sensorY, distance));
                beaconSet.Add((beaconX, beaconY));
            }
            var signPos = new[]
            {
                (-1,-1),
                (-1,1),
                (1,-1),
                (1,1)
            };
            foreach(var t in sensorSet)
            {
                //all points at least d+1 away from the sensor (sx,sy)
                for (var distanceX = 0; distanceX < t.distance + 2; distanceX++)
                {
                    var distanceY = (t.distance + 1) - distanceX;
                    foreach (var p in signPos)
                    {
                        numberChecked++;
                        var x = t.sx + (distanceX * p.Item2);
                        var y = t.sy + (distanceY * p.Item1);
                        if (!(x >= 0 && x <= 4000000) || !(y >= 0 && y <= 4000000))
                            continue;
                        if (Math.Abs(x - t.sx) + Math.Abs(y - t.sy) == t.distance + 1) { 
                            if(isValid(x,y,sensorSet) && !foundBeacon)
                            {
                                Console.WriteLine($"x:{x} y:{y}");
                                //Number so big, gotta cast it to stop truncating.
                                long l = (long)x * (long)4000000 + (long)y;
                                Console.WriteLine($"Tuning frequency {l}");
                                foundBeacon = true;
                            }
                        }
                    }
                }
            }
        }

        private bool isValid(int x, int y, HashSet<(int sx,int sy,int sd)> s)
        {
            foreach(var t in s)
            {
                var distance = Math.Abs(x - t.sx)+Math.Abs(y-t.sy);
                if (distance <= t.sd)
                    return false;
            }
            return true;
        }
    }
}
