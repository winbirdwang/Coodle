using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Core;

namespace PhysicalDiskFileParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = args[0];
            Console.WriteLine("start parsing all files recursively at " + root);

            var ignoreList = new[] { ' ', '.', '(', ')', '[', ']', '{', '}', '+', '-', '*', '/', ';' };
            foreach (var file in Directory.EnumerateFiles(root))
            {                
                foreach (var line in File.ReadLines(file))
                {
                    var words = line.Split(ignoreList, StringSplitOptions.RemoveEmptyEntries).Distinct();
                    foreach (var word in words)
                    {
                        ElasticSearch.Index(word, file);
                    }
                }
            } 

            Console.WriteLine("finished");
        }
    }
}
