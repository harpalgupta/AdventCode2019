using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day2_1202ProgramAlarm
    {
        private readonly ProgramAlarm _programAlarm = new ProgramAlarm();
        
        public int[] ProcessInstructions(int[] instructions)
        {
            _programAlarm.Process(instructions);

            return _programAlarm.State;
        }
        
        public int[] ProcessInstructions(int[] instructions, int noun, int verb)
        {
            instructions = instructions.ToArray();
            
            instructions[1] = noun;
            instructions[2] = verb;

            return ProcessInstructions(instructions);
        }

        public int LoopNounVerbProcessUntilMatchedReturningResult(int[] instructions, int matchValue)
        {
            instructions = instructions.ToArray();

            for (int noun = 0; noun <= 99; noun++)
            {
                for (int verb = 0; verb <= 99; verb++)
                {
                    int[] state = ProcessInstructions(instructions, noun, verb);

                    if (state[0] == matchValue)
                    {
                        return 100 * noun + verb;
                    }
                }
            }

            throw new Exception("Match not found");
        }

        private class ProgramAlarm
        {
            public int[] State { get; private set; }
            private int _pointer;
            private Dictionary<int, Action> _operations;
            private bool _isProcessing;

            private int OpCode => State[_pointer];

            public ProgramAlarm()
            {
                BuildOperations();
            }

            public void Process(int[] instructions)
            {
                State = instructions.ToArray();

                for (_pointer = 0, _isProcessing = true; _isProcessing;)
                {
                    if (_operations.ContainsKey(OpCode))
                    {
                        _operations[OpCode]();
                    }
                }
            }

            private void Sum()
            {
                State[Param(3)] = State[Param(1)] + State[Param(2)];
                MovePointer(4);
            }

            private void Product()
            {
                State[Param(3)] = State[Param(1)] * State[Param(2)];
                MovePointer(4);
            }

            private void Stop()
            {
                _isProcessing = false;
            }

            private int Param(int number)
            {
                return State[_pointer + number];
            }

            private void MovePointer(int positions)
            {
                _pointer += positions;
            }

            private void BuildOperations()
            {
                _operations = new Dictionary<int, Action>
                {
                    { 1, Sum },
                    { 2, Product },
                    { 99, Stop }
                };
            }
        }
    }
}