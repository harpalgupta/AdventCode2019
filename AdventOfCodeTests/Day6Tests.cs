using AdventCode2019Core;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCodeTests
{

    [TestFixture]
    public class Day6Tests
    {
        private Day6 day6;

        [OneTimeSetUp]
        public void Setup()
        {
            // day6 = new Day6();

        }

        [Test]
        public void WhenPlanetDoesNotExistReturnsZero ()
        {
            var day6 = new Day6("COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L");
            var result = day6.GetOrbitsForPlanet("L");

            Assert.That(result, Is.EqualTo(7));

        }

        [Test]
        public void GetTotalOrbits()
        {
            var day6 = new Day6("COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L");
            var result = day6.GetOrbitsForPlanet("L");

            Assert.That(result, Is.EqualTo(7));

        }

        [Test]
        public void GetTotalOrbitsForInput()
        {
            var day6 = new Day6();
            var result = day6.GetTotalIndirectAndDirectOrbits();

            Assert.That(result, Is.EqualTo(314702));

        }


    }
}
