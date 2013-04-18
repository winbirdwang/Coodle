using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PhysicalDiskFileParser
{
    public class Keyword
    {
        public string Word { get; set; }

        public string PhysicalPath { get; set; }

        public HashSet<int> LineNumbers { get; set; }
    }
}
