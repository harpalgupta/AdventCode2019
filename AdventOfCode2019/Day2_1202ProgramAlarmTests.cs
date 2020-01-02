using System;
using NUnit.Framework;

namespace AdventOfCode2019
{
    [TestFixture]
    public class Day2_1202ProgramAlarmTests
    {
        private Day2_1202ProgramAlarm _program;

        [SetUp]
        public void Setup()
        {
            _program = new Day2_1202ProgramAlarm();
        }

        [Test]
        public void HaltCorrectly_ReturnsInputAsOutput()
        {
            int[] instructions = {99};

            Assert.AreEqual(new[] {99}, _program.ProcessInstructions(instructions));
        }

        [Test]
        public void HaltCorrectly_ReturnsInputAsOutputEvenInputAfterHaltOpCode()
        {
            int[] instructions = {99, 1, 2, 3, 4, 5, 6};

            Assert.AreEqual(new[] {99, 1, 2, 3, 4, 5, 6}, _program.ProcessInstructions(instructions));
        }

        [Test]
        public void OpCode1_AddsNumberInPosition0TwiceAndWriteOutputInPosition0()
        {
            int[] instructions = {1, 0, 0, 0, 99};

            Assert.AreEqual(new[] {2, 0, 0, 0, 99}, _program.ProcessInstructions(instructions));
        }

        [Test]
        public void OpCode1_AddsNumberAndWriteOutputsBasedOnFollowing3Inputs()
        {
            int[] instructions = {1, 5, 3, 2, 99, 18};

            Assert.AreEqual(new[] {1, 5, 20, 2, 99, 18}, _program.ProcessInstructions(instructions));
        }

        [Test]
        public void SubsequentOpCode1_DoesTheJobUntil99()
        {
            int[] instructions = {1, 2, 3, 1, 1, 8, 9, 5, 99, 5};

            Assert.AreEqual(new[] {1, 4, 3, 1, 1, 104, 9, 5, 99, 5}, _program.ProcessInstructions(instructions));
        }

        [Test]
        public void SubsequentOpCode1_CanChangeOtherSplitsUntil99()
        {
            int[] instructions = {1, 0, 1, 4, 0, 8, 9, 1, 99, 5};

            Assert.AreEqual(new[] {1, 104, 1, 4, 1, 8, 9, 1, 99, 5}, _program.ProcessInstructions(instructions));
        }

        [Test]
        public void OpCode2_Multiply()
        {
            int[] instructions = {2, 0, 2, 1, 99, 5};

            Assert.AreEqual(new[] {2, 4, 2, 1, 99, 5}, _program.ProcessInstructions(instructions));
        }

        [Test]
        public void Test1()
        {
            int[] instructions = {1,1,1,4,99,5,6,0,99};

            Assert.AreEqual(new[] {30,1,1,4,2,5,6,0,99}, _program.ProcessInstructions(instructions));
        }
        
        [Test]
        public void Test2()
        {
            int[] instructions = {1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,10,19,1,19,5,23,2,23,6,27,1,27,5,31,2,6,31,35,1,5,35,39,2,39,9,43,1,43,5,47,1,10,47,51,1,51,6,55,1,55,10,59,1,59,6,63,2,13,63,67,1,9,67,71,2,6,71,75,1,5,75,79,1,9,79,83,2,6,83,87,1,5,87,91,2,6,91,95,2,95,9,99,1,99,6,103,1,103,13,107,2,13,107,111,2,111,10,115,1,115,6,119,1,6,119,123,2,6,123,127,1,127,5,131,2,131,6,135,1,135,2,139,1,139,9,0,99,2,14,0,0};

            var state = _program.ProcessInstructions(instructions, 12, 2);
            Assert.AreEqual(5866663, state[0]);
        }

        [Test]
        public void BreakingTeeeest()
        {
            int[] instructions = {1, 0, 0, 0, 99, 0, 0, 0, 1, 0, 0, 0, 99};

            Assert.AreEqual(new[] {2, 0, 0, 0, 99, 0, 0, 0, 1, 0, 0, 0, 99}, _program.ProcessInstructions(instructions));
        }

        [Test]
        public void ProcessDoesNotModifyInputArray()
        {
            int[] instructions = {1, 0, 0, 0, 99};
            _program.ProcessInstructions(instructions);
            
            Assert.AreEqual(new[] {1, 0, 0, 0, 99}, instructions);
        }
        
        [Test]
        public void LoopForNounAndVerb()
        {
            int[] instructions = {1,0,0,3,1,1,2,3,1,3,4,3,1,5,0,3,2,1,10,19,1,19,5,23,2,23,6,27,1,27,5,31,2,6,31,35,1,5,35,39,2,39,9,43,1,43,5,47,1,10,47,51,1,51,6,55,1,55,10,59,1,59,6,63,2,13,63,67,1,9,67,71,2,6,71,75,1,5,75,79,1,9,79,83,2,6,83,87,1,5,87,91,2,6,91,95,2,95,9,99,1,99,6,103,1,103,13,107,2,13,107,111,2,111,10,115,1,115,6,119,1,6,119,123,2,6,123,127,1,127,5,131,2,131,6,135,1,135,2,139,1,139,9,0,99,2,14,0,0};
            var result = _program.LoopNounVerbProcessUntilMatchedReturningResult(instructions, 19690720);

            Assert.AreEqual(4259, result);
        }  
        
        [Test]
        public void OpCode3TakesInputAt0AndStoresValueAtPosition5()
        {
            int[] instructions = {3,5,99};
            var result = _program.ProcessInstructions(instructions);

            Assert.AreEqual(new []{3,5,99,0,0,1}, result);
        }
        
                
        [Test]
        public void OpCode4OutPutsValueAtPosition2()
        {
            int[] instructions = {4,2,99};
            var result = _program.ProcessInstructions(instructions);

            Assert.AreEqual(_program.GetOutputValue(), 99);
        }

        [Test]
        public void CanHandleParameterModeForSumIn_AllParamsInPositionMode()
        {
            int[] instructions = {0001, 0, 0, 0, 99};
            var result = _program.ProcessInstructions(instructions);

            Assert.AreEqual(new[] {2, 0, 0, 0, 99}, _program.ProcessInstructions(instructions));
        }
        
        [Test]
        public void ParameterMode0001_AddsNumberInPosition0TwiceAndWriteOutputInPosition0()
        {
            int[] instructions = {0001, 0, 0, 0, 99};

            Assert.AreEqual(new[] {2, 0, 0, 0, 99}, _program.ProcessInstructions(instructions));
        }
        
        [Test]
        public void ParameterModeWithOpCode1_AddsNumberInParam1WithPosition0AndWriteOutputInPosition0()
        {
            int[] instructions = {10001, 1, 0, 0, 99};
            Assert.AreEqual(new[] {10001, 1, 0, 10002, 99}, _program.ProcessInstructions(instructions));
        }
        [Test]
        public void ParameterModeWithOpCode2_ProductOfNumberInParam1WithPosition0AndWriteOutputInPosition0()
        {
            int[] instructions = {00102, 2, 0, 0, 99};
            Assert.AreEqual(new[] {204, 2, 0, 0, 99}, _program.ProcessInstructions(instructions));
        }
        [Test]
        public void ParameterModeWithOpCode2_WhenAllImmediate_ProductOfNumberInParam1WithParam2AndWriteOutputInParam3()
        {
            int[] instructions = {11102, 2, 3, 0, 99};
            Assert.AreEqual(new[] {11102, 2, 3, 6, 99}, _program.ProcessInstructions(instructions));
        }
        [Test]
        public void ParameterModeWithOpCode1_AddsNegativeNumberInParam1WithPosition0AndWriteOutputInPosition0()
        {
            int[] instructions = {10101, -1, 0, 0, 99};
            Assert.AreEqual(new[] {10101, -1, 0, 10100, 99}, _program.ProcessInstructions(instructions));
        }

        [Test]
        public void Day5TestPart1Result()
        {
            int[] instructions =
            {
                3, 225, 1, 225, 6, 6, 1100, 1, 238, 225, 104, 0, 1102, 9, 19, 225, 1, 136, 139, 224, 101, -17, 224, 224,
                4, 224, 102, 8, 223, 223, 101, 6, 224, 224, 1, 223, 224, 223, 2, 218, 213, 224, 1001, 224, -4560, 224,
                4, 224, 102, 8, 223, 223, 1001, 224, 4, 224, 1, 223, 224, 223, 1102, 25, 63, 224, 101, -1575, 224, 224,
                4, 224, 102, 8, 223, 223, 1001, 224, 4, 224, 1, 223, 224, 223, 1102, 55, 31, 225, 1101, 38, 15, 225,
                1001, 13, 88, 224, 1001, 224, -97, 224, 4, 224, 102, 8, 223, 223, 101, 5, 224, 224, 1, 224, 223, 223,
                1002, 87, 88, 224, 101, -3344, 224, 224, 4, 224, 102, 8, 223, 223, 1001, 224, 7, 224, 1, 224, 223, 223,
                1102, 39, 10, 225, 1102, 7, 70, 225, 1101, 19, 47, 224, 101, -66, 224, 224, 4, 224, 1002, 223, 8, 223,
                1001, 224, 6, 224, 1, 224, 223, 223, 1102, 49, 72, 225, 102, 77, 166, 224, 101, -5544, 224, 224, 4, 224,
                102, 8, 223, 223, 1001, 224, 4, 224, 1, 223, 224, 223, 101, 32, 83, 224, 101, -87, 224, 224, 4, 224,
                102, 8, 223, 223, 1001, 224, 3, 224, 1, 224, 223, 223, 1101, 80, 5, 225, 1101, 47, 57, 225, 4, 223, 99,
                0, 0, 0, 677, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1105, 0, 99999, 1105, 227, 247, 1105, 1, 99999, 1005,
                227, 99999, 1005, 0, 256, 1105, 1, 99999, 1106, 227, 99999, 1106, 0, 265, 1105, 1, 99999, 1006, 0,
                99999, 1006, 227, 274, 1105, 1, 99999, 1105, 1, 280, 1105, 1, 99999, 1, 225, 225, 225, 1101, 294, 0, 0,
                105, 1, 0, 1105, 1, 99999, 1106, 0, 300, 1105, 1, 99999, 1, 225, 225, 225, 1101, 314, 0, 0, 106, 0, 0,
                1105, 1, 99999, 1008, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 329, 1001, 223, 1, 223, 107, 226,
                677, 224, 1002, 223, 2, 223, 1006, 224, 344, 101, 1, 223, 223, 1007, 677, 677, 224, 1002, 223, 2, 223,
                1006, 224, 359, 1001, 223, 1, 223, 8, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 374, 101, 1, 223, 223,
                108, 226, 677, 224, 102, 2, 223, 223, 1006, 224, 389, 1001, 223, 1, 223, 1008, 677, 677, 224, 1002, 223,
                2, 223, 1006, 224, 404, 1001, 223, 1, 223, 1107, 677, 677, 224, 102, 2, 223, 223, 1005, 224, 419, 1001,
                223, 1, 223, 1008, 226, 226, 224, 102, 2, 223, 223, 1005, 224, 434, 101, 1, 223, 223, 8, 226, 677, 224,
                1002, 223, 2, 223, 1006, 224, 449, 101, 1, 223, 223, 1007, 677, 226, 224, 102, 2, 223, 223, 1005, 224,
                464, 1001, 223, 1, 223, 107, 677, 677, 224, 1002, 223, 2, 223, 1005, 224, 479, 1001, 223, 1, 223, 1107,
                226, 677, 224, 1002, 223, 2, 223, 1005, 224, 494, 1001, 223, 1, 223, 7, 677, 677, 224, 102, 2, 223, 223,
                1006, 224, 509, 101, 1, 223, 223, 1007, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 524, 101, 1, 223,
                223, 7, 677, 226, 224, 102, 2, 223, 223, 1005, 224, 539, 101, 1, 223, 223, 8, 226, 226, 224, 1002, 223,
                2, 223, 1006, 224, 554, 101, 1, 223, 223, 7, 226, 677, 224, 102, 2, 223, 223, 1005, 224, 569, 101, 1,
                223, 223, 1108, 677, 226, 224, 1002, 223, 2, 223, 1005, 224, 584, 101, 1, 223, 223, 108, 677, 677, 224,
                1002, 223, 2, 223, 1006, 224, 599, 101, 1, 223, 223, 107, 226, 226, 224, 1002, 223, 2, 223, 1006, 224,
                614, 101, 1, 223, 223, 1108, 226, 226, 224, 1002, 223, 2, 223, 1005, 224, 629, 1001, 223, 1, 223, 1107,
                677, 226, 224, 1002, 223, 2, 223, 1005, 224, 644, 101, 1, 223, 223, 108, 226, 226, 224, 1002, 223, 2,
                223, 1005, 224, 659, 101, 1, 223, 223, 1108, 226, 677, 224, 1002, 223, 2, 223, 1005, 224, 674, 1001,
                223, 1, 223, 4, 223, 99, 226
            };
            _program.ProcessInstructions(instructions);
            
            Assert.AreEqual(13787043,_program.GetOutputValue());
            

        }
    }
}