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

        public decimal FuelCalculateListAndSum()
        {
            return fileInput.Select(m => FuelCalculate(m)).Sum();
        }

        public decimal MassCalculateWithFuelWeightListAndSum()
        {
            return fileInput.Select(m => MassCalculateWithFuelWeight(m)).Sum();
        }
        
        public Day1(string fileName)
        {
            string fileNameWithPath = Path.GetFullPath(Directory.GetCurrentDirectory() +Path.DirectorySeparatorChar+fileName);
            fileInput = File.ReadLines(fileNameWithPath).Select(s => Int32.Parse(s));
        }
    
        public decimal FuelCalculate(decimal mass)
        {
            return Math.Floor( mass / 3) - 2;
        }

        public decimal MassCalculateWithFuelWeight (decimal mass)
        {
            var tmpMass = mass;
            decimal total = 0;
         
            while (tmpMass > 2)
            {
                tmpMass = FuelCalculate(tmpMass);
                if (tmpMass > 0) total += tmpMass;
            }
            return total;
        }
          
    }
}
