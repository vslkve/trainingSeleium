using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumTests
{
    public class CountryList : IComparable<CountryList>
    {
        public string CountryName { get; set; }

        public CountryList(string name)
        {
            CountryName = name;
        }
        int IComparable<CountryList>.CompareTo(CountryList other)
        {
            return CountryName.CompareTo(other.CountryName);
        }

        public override bool Equals(object country)
        {
            var toCompareWith = country as CountryList;
            if (toCompareWith == null)
                return false;
            return                
                this.CountryName == toCompareWith.CountryName;
        }
    }
}
