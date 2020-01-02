using NUnit.Framework;

namespace AdventOfCode2019
{
    public class Day4Tests
    {
        private Day4 day4;

        [SetUp]
        public void Setup()
        {
            day4= new Day4();
        }

        [Test]
        public void CheckContainsDouble()
        {
            var password = "111122";
            
            Assert.True(day4.ContainsDouble(password));
            

        }
        
        [Test]
        public void CheckFor3OrMoreConsecutiveDuplicates()
        {
            var password = 111255;
            
            Assert.True(day4.ContainsThreeOrMore(password.ToString()));
            

        }

        [Test]
        public void CheckForDecreasingDigits()
        {
            int password = 133556;
            Assert.True(day4.PreviousDigitsLessOrEqualTo(password));

            int badPassword = 123123;
            Assert.False(day4.PreviousDigitsLessOrEqualTo(badPassword));
        }

        [Test]
        public void CheckGetPasswords()
        {
            //1723 is too high
            //1694 is correct part A
            
            //780 is too low part B
            //71208 too high
            //809 too low
            //1584 not right?
            //1148 correct PartB
            
            int result = day4.GetPassword();
            Assert.AreEqual(1148,result);
        }
    }
}