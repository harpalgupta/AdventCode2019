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
            int[] instructions = {1, 1, 1, 4, 99, 5, 6, 0, 99};

            Assert.AreEqual(new[] {30, 1, 1, 4, 2, 5, 6, 0, 99}, _program.ProcessInstructions(instructions));
        }

        [Test]
        public void Test2()
        {
            int[] instructions =
            {
                1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 1, 10, 19, 1, 19, 5, 23, 2, 23, 6, 27, 1, 27, 5, 31,
                2, 6, 31, 35, 1, 5, 35, 39, 2, 39, 9, 43, 1, 43, 5, 47, 1, 10, 47, 51, 1, 51, 6, 55, 1, 55, 10, 59, 1,
                59, 6, 63, 2, 13, 63, 67, 1, 9, 67, 71, 2, 6, 71, 75, 1, 5, 75, 79, 1, 9, 79, 83, 2, 6, 83, 87, 1, 5,
                87, 91, 2, 6, 91, 95, 2, 95, 9, 99, 1, 99, 6, 103, 1, 103, 13, 107, 2, 13, 107, 111, 2, 111, 10, 115, 1,
                115, 6, 119, 1, 6, 119, 123, 2, 6, 123, 127, 1, 127, 5, 131, 2, 131, 6, 135, 1, 135, 2, 139, 1, 139, 9,
                0, 99, 2, 14, 0, 0
            };

            var state = _program.ProcessInstructions(instructions, 12, 2);
            Assert.AreEqual(5866663, state[0]);
        }

        [Test]
        public void BreakingTeeeest()
        {
            int[] instructions = {1, 0, 0, 0, 99, 0, 0, 0, 1, 0, 0, 0, 99};

            Assert.AreEqual(new[] {2, 0, 0, 0, 99, 0, 0, 0, 1, 0, 0, 0, 99},
                _program.ProcessInstructions(instructions));
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
            int[] instructions =
            {
                1, 0, 0, 3, 1, 1, 2, 3, 1, 3, 4, 3, 1, 5, 0, 3, 2, 1, 10, 19, 1, 19, 5, 23, 2, 23, 6, 27, 1, 27, 5, 31,
                2, 6, 31, 35, 1, 5, 35, 39, 2, 39, 9, 43, 1, 43, 5, 47, 1, 10, 47, 51, 1, 51, 6, 55, 1, 55, 10, 59, 1,
                59, 6, 63, 2, 13, 63, 67, 1, 9, 67, 71, 2, 6, 71, 75, 1, 5, 75, 79, 1, 9, 79, 83, 2, 6, 83, 87, 1, 5,
                87, 91, 2, 6, 91, 95, 2, 95, 9, 99, 1, 99, 6, 103, 1, 103, 13, 107, 2, 13, 107, 111, 2, 111, 10, 115, 1,
                115, 6, 119, 1, 6, 119, 123, 2, 6, 123, 127, 1, 127, 5, 131, 2, 131, 6, 135, 1, 135, 2, 139, 1, 139, 9,
                0, 99, 2, 14, 0, 0
            };
            var result = _program.LoopNounVerbProcessUntilMatchedReturningResult(instructions, 19690720);

            Assert.AreEqual(4259, result);
        }

    }
}