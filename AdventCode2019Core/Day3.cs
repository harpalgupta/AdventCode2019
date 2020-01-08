using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day3
    {
        public IEnumerable<CoOrdinates> GetLogFromInstruction(CoOrdinates startingPoint, string instruction)
        {
            var convertedInstruction = TransformInstructionToDistance(instruction);
            var coOrdinatesLog = new List<CoOrdinates>();
            var currentPosition = startingPoint;

            var stepsX = convertedInstruction.X; 
            var stepsY = convertedInstruction.Y; 
            
            for (int i = 0; i < (Math.Abs(stepsX+stepsY)); i++)
            {
                var temporaryLog = new CoOrdinates(){X=currentPosition.X,Y=currentPosition.Y};
                if (stepsX > 0)
                {
                    temporaryLog.X = currentPosition.X + 1;
                }
                else if (stepsX < 0)
                {
                    temporaryLog.X = currentPosition.X - 1;
                }
                else if (stepsY > 0)
                {
                    temporaryLog.Y = currentPosition.Y + 1;
                }
                else if (stepsY < 0)
                {
                    temporaryLog.Y = currentPosition.Y - 1;
                }
                
                coOrdinatesLog.Add(temporaryLog);
                currentPosition = coOrdinatesLog.Last();
            }
            return coOrdinatesLog;
        }
        
        public IEnumerable<string> GetLogFromMultipleInstructionsAsString(CoOrdinates centralPort, string stringOfInstructions)
        {
            char[] delimiterChars = { ',' };
            var instructions = stringOfInstructions.Split(delimiterChars);
          
            
            var start = new CoOrdinates() {X = centralPort.X, Y = centralPort.Y};
            var log = new List<string>();
            foreach (var instruction in instructions)
            {
                IEnumerable<CoOrdinates> result = GetLogFromInstruction(start, instruction).ToList();
                log.AddRange(result.Select(x => $"{x.X},{x.Y}"));
                start.X = result.Last().X;
                start.Y = result.Last().Y;
            }

            return log;
        }
        
        
        public Dictionary<string,int> GetLogFromMultipleInstructionsAsDictionary(CoOrdinates centralPort, string instructions)
        {
            var stringResult = GetLogFromMultipleInstructionsAsString(centralPort, instructions); 
            return stringResult.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
        }

        public CoOrdinates TransformInstructionToDistance(string instruction)
        {
            var result = new CoOrdinates();
            var direction = instruction[0].ToString();
            var distance = Int32.Parse(instruction.Substring(1));
            
            switch (direction)
            {
            case "R":
                result.X = distance;
                break;
            case "U":
                result.Y = distance;
                break;
            case "L":
                result.X = Math.Abs(distance)*(-1);
                break;
            case "D":
                result.Y = Math.Abs(distance)*(-1);
                break;
            }

            return result;
        }

        public Dictionary<string, int> GetIntersections(Dictionary<string, int> result1,Dictionary<string, int>result2)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            foreach (KeyValuePair<string, int> point in result1)
            {
                if (result2.Contains(point) /* || point.Value > 1*/)
                {
                    if (point.Key != "0,0")
                    {
                        result.Add(point.Key, point.Value);
                    }

                }
            }
            return result;

        }
       
        public int ShortestDistance(Dictionary<string, int> intersections)
        {
            var distances = intersections
                .Select(x => x.Key.Split(',')
                    .Select(s => Math.Abs(Int32.Parse(s))).Sum())
                .OrderBy(i => i);
            
            return distances.First();
        }
        
        //check what step in Log intersections occur

        public IEnumerable<int> GetStepsForIntersections(IEnumerable<string> log, IEnumerable<string>intersections)
        {
            
            var steps = intersections.Select(i=>GetStepsForIntersection(i, log));
            return steps.Select(s => GetDistanceForAllSteps(s));
        }

        public IEnumerable<string> GetStepsForIntersection(string interSection, IEnumerable<string> log)
        {
            var positionOfIntersection = GetPositionOfIntersection(log, interSection);

            var filteredLog = log.ToArray().Take(positionOfIntersection+1);

            return filteredLog;

        }
        
        public int GetLowestTotalledSteps(IEnumerable<int> stepsA, IEnumerable<int> stepsB)
        {
            var totalledSteps = new List<int>();
            var intersectionsCount = stepsA.Count();

            for (int i = 0; i < intersectionsCount; i++)
            {
                totalledSteps.Add(stepsA.ElementAt(i) + stepsB.ElementAt(i));
            }

            return totalledSteps.Min();
        }

        public int GetDistanceForAllSteps(IEnumerable<string> log)
        {
            var previousX = 0;
            var previousY = 0;
            var total = 0;
            

                var xValues = log.Select(i=>Int32.Parse(i.Split(',').First()));
                var yValues = log.Select(i=>Int32.Parse(i.Split(',').ElementAt(1)));

                foreach (var value in xValues)
                {
                    if (value != previousX)
                    {
                        var difference = Math.Abs(previousX - value);
                        total++;
                        previousX = value;

                    }
                }
                
                foreach (var value in yValues)
                {
                    if (value != previousY)
                    {
                        var difference = Math.Abs(previousY - value);
                        total++;
                        previousY = value;


                    }
                }

                return total;


        }

        public  int GetPositionOfIntersection(IEnumerable<string> log,string interSection)
        {
            return Array.IndexOf(log.ToArray(), interSection);
        }
        
    }
    
    

    public class CoOrdinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        
    }
}