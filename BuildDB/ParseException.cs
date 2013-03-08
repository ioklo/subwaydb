using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubwayDB
{
    class ParseException : Exception
    {
        public ParseException(string msg, Exception innerException)
            : base(msg, innerException)
        {

        }
    }
}
