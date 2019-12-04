using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace lightbulbs.Controllers
{
    [Route("api/[controller]")]
    public class LightBulbController : Controller
    {
        private System.Runtime.Caching.MemoryCache cache = new System.Runtime.Caching.MemoryCache("LightBulbsCache");

        [HttpGet("[action]")]
        public Lightbulbs GetLightBulbs(int numOfLightbulbs, int people) {
            var cachedBulb = cache[numOfLightbulbs.ToString()] as SortedDictionary<int, Lightbulbs>;
            Lightbulbs lightbulbs = null;
            if (cachedBulb == null) {
                cachedBulb = new SortedDictionary<int, Lightbulbs>();
                cache[numOfLightbulbs.ToString()] = cachedBulb;
                lightbulbs = MakeLightbulbs(numOfLightbulbs, people);
                cachedBulb[people] = lightbulbs;
            }
            else {
                lightbulbs = cachedBulb[people];
                if (lightbulbs == null) {
                    KeyValuePair<int, Lightbulbs> closestMatch = cachedBulb.Where(x => x.Key < people).OrderBy(x => Math.Abs(x.Key - people)).FirstOrDefault();
                    lightbulbs = MakeLightbulbs(closestMatch, people);
                    cachedBulb[people] = lightbulbs;
                }
            }
            return lightbulbs;
        }

        /**
            Calculate lightbulbs state from a previous cached lightbulbs point
        */
        private Lightbulbs MakeLightbulbs(KeyValuePair<int, Lightbulbs> closestMatch, int people) {
            Lightbulbs lightbulbs = new Lightbulbs(closestMatch.Value);
            AdjustLightbulbs(ref lightbulbs, people, closestMatch.Key + 1);
            return lightbulbs;
        }
        /**
            Calculate lightbulbs state from scratch
        */
        private Lightbulbs MakeLightbulbs(int numOfLightbulbs, int people) {
            Lightbulbs lightbulbs = new Lightbulbs(numOfLightbulbs);
            if (numOfLightbulbs == people) //Constraint for smart optimized approach
                AdjustLightbulbsOptimized(ref lightbulbs, people);
            else
                AdjustLightbulbs(ref lightbulbs, people);
            return lightbulbs;
        }

        /** Standard Brute Force Approach */
        private void AdjustLightbulbs(ref Lightbulbs lightbulbs, int people, int start = 1) {
            int numOfLightbulbs = lightbulbs.bulbs.Count();
            for (int i = start; i <= people; i++) {
                int people_ = i;
                while (people_ < numOfLightbulbs) {
                    lightbulbs.bulbs[people_] ^= true;
                    people_ += i;
                }
            }
            lightbulbs.bulbsOn = lightbulbs.bulbs.Count( lb => lb ); //Sets the number of bulbs that are true / switched on;
        }

        /** Optimized Square Numbers Approach */
        private void AdjustLightbulbsOptimized(ref Lightbulbs lightbulbs, int people) {
            int numOfLightbulbs = lightbulbs.bulbs.Count();
            Array.Clear(lightbulbs.bulbs, 0, lightbulbs.bulbs.Length);
            for (int i = 1, n = i*i; i <= people; i++) {
                n += 2*i + 1;
                lightbulbs.bulbs[n] = true;
            }
            lightbulbs.bulbsOn = lightbulbs.bulbs.Count( lb => lb ); //Sets the number of bulbs that are true / switched on;
        }

        public class Lightbulbs {
            public int bulbsOn { get; set; } //< Number of lightbulbs that are switched on
            public bool[] bulbs { get; set; } //< The state of the lightbulbs
            public Lightbulbs(int numOfLightbulbs) {
                bulbs = new bool[numOfLightbulbs]; //By default, all bulbs will be false / switched off
            }
            // Copy Constructor
            public Lightbulbs(Lightbulbs lightbulbs) {
                bulbsOn = lightbulbs.bulbsOn;
                bulbs = (bool[])lightbulbs.bulbs.Clone();
            }
        }
    }
}
