﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode2019
{
    public class Day2_1202ProgramAlarm
    {
        private readonly ProgramAlarm _programAlarm = new ProgramAlarm();
        private static int _inputValue;
        private static int _outputValue;
        private static ProcessMode[] _paramModes;

        public int[] ProcessInstructions(int[] instructions)
        {
            _inputValue = 1;
            _outputValue = 0;
            _programAlarm.Process(instructions,_inputValue);

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

            public void Process(int[] instructions, int? input)
            {
                State = instructions.ToArray();

                for (_pointer = 0, _isProcessing = true; _isProcessing;)
                {
                    var paramCodeString = ParamCode.ToString();
                    var opCode = ParamCode;
                    if (ParamCode.ToString().Length > 1)
                    {
                        opCode = Int32.Parse(paramCodeString.Substring(paramCodeString.Length-2));
                    }
                    
                    if (_operations.ContainsKey(opCode))
                    {
                        _operations[opCode]();
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
            private void Store()
            {
                var state = State.ToArray();
                if (Param(1) >= state.Length)
                {
                    Array.Resize(ref state,Param(1)+1);
                }
                state[Param(1)] = _inputValue;
                State = state;
                MovePointer(2);
            }
            private void Output()
            {
                _outputValue = State[Param(1)];
                MovePointer(2);
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
                _operations = new Dictionary<int,Action>
                {
                    { 1, Sum },
                    { 2, Product },
                    { 3, Store},
                    { 4, Output},
                    { 99, Stop }
                };
            }
        }
    }
}