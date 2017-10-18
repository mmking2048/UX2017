using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UX2017
{
    public class WordGenerator
    {
        private readonly Random _rand;

        public WordGenerator(Random rand)
        {
            _rand = rand;
        }

        public string GetIncrease()
        {
            return "increase";
        }

        public string GetDecrease()
        {
            return "decrease";
        }
    }
}