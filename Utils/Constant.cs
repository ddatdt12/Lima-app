using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Utils
{
    class Constant
    {
    }
    public class Rules
    {
        private Rules(string value) { Value = value; }

        public string Value { get; private set; }

        public static Rules MAX_AGE { get { return new Rules("MAX_AGE"); } }
        public static Rules MIN_AGE { get { return new Rules("MIN_AGE"); } }
        public static Rules YEAR_PUBLICATION_PERIOD { get { return new Rules("YEAR_PUBLICATION_PERIOD"); } }
        public static Rules ALLOWED_BOOK_MAXIMUM { get { return new Rules("ALLOWED_BOOK_MAXIMUM"); } }
        public static Rules MAXIMUM_NUMBER_OF_DAYS_TO_BORROW { get { return new Rules("MAXIMUM_NUMBER_OF_DAYS_TO_BORROW"); } }
        public static Rules FINE { get { return new Rules("FINE"); } }

    }
    //public static class Rules
    //{
    //    const  string MAX_AGE = "MAX_AGE";
    //    const string MIN_AGE = "MIN_AGE";
    //    const string YEAR_PUBLICATION_PERIOD = "YEAR_PUBLICATION_PERIOD";
    //    const string ALLOWED_BOOK_MAXIMUM = "ALLOWED_BOOK_MAXIMUM";
    //    const string MAXIMUM_NUMBER_OF_DAYS_TO_BORROW = "MAXIMUM_NUMBER_OF_DAYS_TO_BORROW";
    //}
}
