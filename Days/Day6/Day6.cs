using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2022.Days.Day6;

internal class Day6: Day 
{
  private readonly record struct DataStream(string buffer, uint length);
  private static uint P1_NUM_UNIQUE_CHARS = 4;
  private static uint P2_NUM_UNIQUE_CHARS = 14;
    public override void SolvePart1()
  {
    DataStream stream = new DataStream(this.ReadLines()[0], P1_NUM_UNIQUE_CHARS);
    this.SolvePart(stream);
  }

  public override void SolvePart2()
  {
    DataStream stream = new DataStream(this.ReadLines()[0], P2_NUM_UNIQUE_CHARS);
    this.SolvePart(stream);
  }

  //It can be assumed that the datastream buffer will always have one unique string of a length given by this problem's input text.
  void SolvePart(DataStream stream)
  {
    var markerIndex = findFirstUniqueSubstringIndex(stream.buffer, stream.length) + stream.length;
    Console.WriteLine($"Marker Index: {markerIndex}");
  }
    //It can be assumed that the datastream buffer will always have one unique string of a length given by this problem's input text.
    private static int findFirstUniqueSubstringIndex(string substr, uint length)
  {
    var index = -1;
    for(int i = 0; i < substr.Length;i++)
    {
      if(isAllUniqueSubstring(substr.Substring(i, (int)length))){
        index = i;
        return index;
      }
    }
    return index;
  }

  private static bool isAllUniqueSubstring(string substring)
  {
    HashSet<char> set = new HashSet<char>(substring.ToCharArray());
    return set.Count == substring.Length;
  }


}