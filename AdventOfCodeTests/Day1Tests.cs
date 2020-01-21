using NUnit.Framework;

namespace AdventOfCode2019
{
    [TestFixture]
    public class Day1Tests
    {
        private Day1 day1;

        [OneTimeSetUp]
        public void Setup()
        {
            day1 = new Day1("day1Input.txt");
        }

        [Test]
        public void GivenAMassCalculateFuelRequired()
        {
            var expectedFuelForMass12 = 2;
            var expectedFuelForMass14 = 2;
            var expectedFuelForMass1969 = 654;
            var expectedFuelForMass100756 = 33583;

            Assert.AreEqual(expectedFuelForMass12, day1.FuelCalculate(12));
            Assert.AreEqual(expectedFuelForMass14, day1.FuelCalculate(14));
            Assert.AreEqual(expectedFuelForMass1969, day1.FuelCalculate(1969));
            Assert.AreEqual(expectedFuelForMass100756, day1.FuelCalculate(100756));
        }

        [Test]
        public void GivenAListOfMassesGetSumOfFuelRequired()
        {
            var expectedPart1Result = 3184233;
            Assert.AreEqual(expectedPart1Result, day1.FuelCalculateListAndSum());
        }

        [Test]
        public void GivenAMassCalculateFuelIncludingMassOfFuelRequired()
        {
            var expectedFuelForMass14 = 2;
            var expectedFuelForMass1969 = 966;
            var expectedFuelForMass100756 = 50346;

            Assert.AreEqual(expectedFuelForMass14, day1.MassCalculateWithFuelWeight(14));
            Assert.AreEqual(expectedFuelForMass1969, day1.MassCalculateWithFuelWeight(1969));
            Assert.AreEqual(expectedFuelForMass100756, day1.MassCalculateWithFuelWeight(100756));
        }

        [Test]
        public void GivenAListOfMassesGetSumOfFuelRequiredWithMassOfFuel()
        {
            var expectedPart1Result = 4773483;
            Assert.AreEqual(expectedPart1Result, day1.MassCalculateWithFuelWeightListAndSum());
        }
    }
}