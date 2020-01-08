using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019
{
    public class Day1
    {
        private IEnumerable<int> fileInput;

        public decimal MassCalculateListAndSum()
        {
            return fileInput.Select(m => MassCalculate(m)).Sum();
        }

        public decimal MassCalculateWithFuelListAndSum()
        {
            return fileInput.Select(m => MassCalculateWithFuel(m)).Sum();
        }
        
        public Day1(string fileName)
        {
            string fileNameWithPath = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\"+fileName);
            fileInput = System.IO.File.ReadLines(fileNameWithPath).Select(s => Int32.Parse(s));
        }
    
        public decimal MassCalculate(decimal mass)
        {
            return Math.Floor( mass / 3) - 2;
        }

        public decimal MassCalculateWithFuel (decimal mass)
        {
            var tmpMass = mass;
            decimal total = 0;
         
            while (tmpMass > 2)
            {
                tmpMass = MassCalculate(tmpMass);
                if (tmpMass > 0) total += tmpMass;
            }
            return total;
        }
          
    }
}
