using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2019
{
    public class Day4
    {
        
        //password is 6 digits
        //2 digits are the same
        //left to right increase

        public int GetPassword()
        {
         int start = 156218;
         int end = 652527;

         var matchingPasswords = new List<int>();

         for (int i = start; i < end+1; i++)
         {
             var numberString = i.ToString();
             if (!PreviousDigitsLessOrEqualTo(i))
             {
                 continue;
             }
             
             if (!ContainsDouble(numberString))
             {
                 continue;
             };
             
             if (!ContainsThreeOrMoreAndDouble(numberString)&&ContainsThreeOrMore(numberString))
             {
                 continue;
             }
           
             if (ContainsThreeOrMoreAndDouble(numberString))
             {
                 matchingPasswords.Add(i);
             }
            
             else if  (ContainsDouble(numberString))
             {
                 matchingPasswords.Add(i);
             };
         }

         return matchingPasswords.Count();
        }

        public bool ContainsDouble(string password)
        {
            Regex rx = new Regex(@"([0-9])\1{1,1}");
            return rx.IsMatch(password);
        }  
        public bool ContainsThreeOrMore(string password)
        {
            Regex rx = new Regex(@"([0-9])\1{2,5}");
            return rx.IsMatch(password);
        }
        
        public bool ContainsThreeOrMoreAndDouble(string password)
        {
            Regex rxTripleMore = new Regex(@"([0-9])\1{2,5}");
            var tempPassword = "";
            if (rxTripleMore.IsMatch(password))
            {
                tempPassword= rxTripleMore.Replace(password, "x");
            }
            if (ContainsDouble(tempPassword) && tempPassword.Contains("x"))
            {
                return true;
            }

            return false;
        }

        public bool PreviousDigitsLessOrEqualTo(int password)
        {
            int[] passwordArray =password.ToString().Select(c => Convert.ToInt32(c.ToString())).ToArray();
        
            //if middle 4 are same return false early

            if (passwordArray[2] == passwordArray[1] && passwordArray[3] == passwordArray[1]&& passwordArray[4] == passwordArray[1]) 
                return false;
            
            for (int i = passwordArray.Length-1; i >= 1; i--)
            {
                if (passwordArray[i-1] > passwordArray[i])
                {
                    return false;
                }
            }

            return true;

        }

    }
}