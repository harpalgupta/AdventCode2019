using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var day1 = new Day1("day1Input.txt");
            var part1Result = day1.MassCalculateListAndSum();
            var part2Result = day1.MassCalculateWithFuelListAndSum();
        }


    }
}