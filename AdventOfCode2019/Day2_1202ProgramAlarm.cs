using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day2_1202ProgramAlarm
    {
        private readonly ProgramAlarm _programAlarm = new ProgramAlarm();
        private static int _inputIdValue;
        private static int _outputValue;
        private static ProcessMode[] _paramModes;

        public int[] ProcessInstructions(int[] instructions, int idValue = 1)
        {
            _inputIdValue = idValue;
            _outputValue = 0;
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

        public int GetOutputValue()
        {
            return _outputValue;
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

            // private int OpCode => State[_pointer];
            private int ParamCode => State[_pointer];

            public ProgramAlarm()
            {
                BuildOperations();
            }

            public void Process(int[] instructions)
            {
                State = instructions.ToArray();

                for (_pointer = 0, _isProcessing = true; _isProcessing;)
                {
                    var paramCodeString = ParamCode.ToString().PadLeft(5, Convert.ToChar("0"));
                    var paramCodeArray = paramCodeString.ToArray();

                    _paramModes = new ProcessMode[3];

                    string lastTwoFromParamCode = paramCodeArray[3] + paramCodeArray[4].ToString();
                    _paramModes[0] = GetProcessModeForParam(paramCodeArray[2]);
                    _paramModes[1] = GetProcessModeForParam(paramCodeArray[1]);
                    _paramModes[2] = GetProcessModeForParam(paramCodeArray[0]);

                    var opCode = Int32.Parse(lastTwoFromParamCode);


                    if (_operations.ContainsKey(opCode))
                    {
                        _operations[opCode]();
                    }
                    else
                    {
                        //TODO Skip if Opcode not valid?
                        //MovePointer(1);
                        //throw new Exception($"{opCode} OpCode Not Found");
                    }
                }
            }

            private static ProcessMode GetProcessModeForParam(char paramCode)
            {
                if (Int32.Parse(paramCode.ToString()) == 1)
                {
                    return ProcessMode.Immediate;
                }

                return ProcessMode.Position;
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

            private void Store()
            {
                var state = State.ToArray();
                if (Param(1) >= state.Length)
                {
                    Array.Resize(ref state, Param(1) + 1);
                }

                state[Param(1)] = _inputIdValue;
                State = state;
                MovePointer(2);
            }

            private void Output()
            {
                _outputValue = State[Param(1)];
                Console.WriteLine(_outputValue);
                MovePointer(2);
            }

            private void Stop()
            {
                _isProcessing = false;
            }

            private int Param(int number)
            {
                if (_paramModes[number - 1] == ProcessMode.Immediate)
                {
                    return _pointer + number;
                }

                return State[_pointer + number];
            }

            private void MovePointer(int positions)
            {
                _pointer += positions;
            }
            private void LessThenCheck()
            {
                State[Param(3)] = State[Param(1)] < State[Param(2)] ? 1 : 0;
                MovePointer(4);
            }

            private void EqualToCheck()
            {
                State[Param(3)] = State[Param(1)] == State[Param(2)] ? 1 : 0;
                MovePointer(4);
            }

            private void JumpIfTrue()
            {
                if (State[Param(1)] != 0)
                {
                    _pointer = State[Param(2)];
                }
                else
                {
                    MovePointer(3);
                }
            }

            private void JumpIfFalse()
            {
                if (State[Param(1)] == 0)
                {
                    _pointer = State[Param(2)];
                }
                else
                {
                    MovePointer(3);
                }
            }

            private void BuildOperations()
            {
                _operations = new Dictionary<int, Action>
                {
                    {1, Sum},
                    {2, Product},
                    {3, Store},
                    {4, Output},
                    {5, JumpIfTrue},
                    {6, JumpIfFalse},
                    {7, LessThenCheck},
                    {8, EqualToCheck},
                    {99, Stop}
                };
            }

        
        }
    }
}