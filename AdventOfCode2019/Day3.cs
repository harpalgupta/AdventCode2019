using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day3
    {
        public IEnumerable<CoOrdinates> GetLogFromInstruction(CoOrdinates startingPoint, string instruction)
        {
            //R8
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

        public IEnumerable<CoOrdinates> GetLogFromMultipleInstructions(CoOrdinates centralPort, string[] instructions)
        {
            var start = new CoOrdinates() {X = centralPort.X, Y = centralPort.Y};
            var log = new List<CoOrdinates>();
            foreach (var instruction in instructions)
            {
                var result = GetLogFromInstruction(start, instruction);
                log.AddRange(result);
                start.X = log.Last().X;
                start.Y = log.Last().Y;
            }

            return log;
        }

        public IEnumerable<string> GetLogFromMultipleInstructionsAsString(CoOrdinates centralPort, string[] instructions)
        {
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
        
        
        public Dictionary<string,int> GetLogFromMultipleInstructionsAsDictionary(CoOrdinates centralPort, string[] instructions)
        {
            var start = new CoOrdinates() {X = centralPort.X, Y = centralPort.Y};
            var log = new Dictionary<string,int>();
            foreach (var instruction in instructions)
            {
                IEnumerable<CoOrdinates> result = GetLogFromInstruction(start, instruction).ToList();

             

                foreach (var coord in result)
                {
                    var key = $"{coord.X},{coord.Y}";

                    if (log.ContainsKey(key))
                    {
                        log[key]++;
                    }
                    else
                    {
                        log[key] = 1;
                    }
                    
                    
                }
    
                start.X = result.Last().X;
                start.Y = result.Last().Y;
            }

            return log;
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
    }

    public class CoOrdinates
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        
    }
}