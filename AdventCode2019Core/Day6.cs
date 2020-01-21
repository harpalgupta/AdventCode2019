using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventCode2019Core
{
    public class Subject
    {
        public string SubjectName { get; set; }
        public List<string> ThingsThatOrbitSubject { get; set; }
        public int DirectOrbitCount  { get; set; }

    }

public class Day6
    {
        private List<KeyValuePair<string, string>> map;
        private Dictionary<string,Subject> subjectDictionary;

        public Day6(string map)
        {

            this.map = map.Split(",").Select(o => o.Split(")"))
                .Select(o => new KeyValuePair<string, string>(o.ElementAt(0).ToString(), o.ElementAt(1)
                .ToString())).OrderBy(o=>o.Key).ToList();

            this.subjectDictionary = new Dictionary<string, Subject>();
        }

        public Day6()
        {
            var fileInput = System.IO.File.ReadLines("Day6Input.txt");

            this.map = fileInput.Select(o => o.Split(")"))
             .Select(o => new KeyValuePair<string, string>(o.ElementAt(0).ToString(), o.ElementAt(1)
             .ToString())).OrderBy(o => o.Value).ToList();
            this.subjectDictionary = new Dictionary<string, Subject>();


        }

        public int GetOrbitsForPlanet(string planet)
        {
            var transformedMap = map;
            var orbits = new List<KeyValuePair<string, string>>();
            var currentPlanet = planet;


            var firstDirectOrbits = GetDirectOrbits(transformedMap, currentPlanet);

            var totalOrbits = firstDirectOrbits.Select(x => x).ToList();
            IEnumerable<KeyValuePair<string, string>> orbitsForCurrent;
            var reachedEnd = false;
            foreach (var orbit in firstDirectOrbits)
            {
                var nextOrbitKey = orbit.Key;

                while (!reachedEnd)
                {
                    orbitsForCurrent = GetDirectOrbits(transformedMap, nextOrbitKey);
                    if (orbitsForCurrent.Count() > 0)
                    {
                        totalOrbits.AddRange(orbitsForCurrent);
                        nextOrbitKey = orbitsForCurrent.First().Key;
                    }
                    else
                    {
                        reachedEnd = true;
                    }

                }

            }




            return totalOrbits.Count();

        }

        private IEnumerable<KeyValuePair<string, string>> GetDirectOrbits(List<KeyValuePair<string, string>> transformedMap, string currentPlanet)
        {
            var currentSubject = new Subject {SubjectName= currentPlanet }; 
            var directOrbits = transformedMap.Where(o => o.Value == currentPlanet);
            currentSubject.ThingsThatOrbitSubject = directOrbits.Select(d => d.Key).Distinct().ToList();

            if ( subjectDictionary.ContainsKey(currentPlanet) ) {
                subjectDictionary[currentPlanet].ThingsThatOrbitSubject.AddRange(currentSubject.ThingsThatOrbitSubject);
                subjectDictionary[currentPlanet].ThingsThatOrbitSubject = subjectDictionary[currentPlanet].ThingsThatOrbitSubject.Distinct().ToList;
                }
            else
            {
                subjectDictionary.Add(currentPlanet, currentSubject);
            }


            return directOrbits;
        }

        public int GetTotalIndirectAndDirectOrbits()
        {
            return map.Select(x => GetOrbitsForPlanet(x.Value)).Sum();


        }


    }
}
