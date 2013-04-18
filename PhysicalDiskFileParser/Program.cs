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

            var ignoreList = new[] { ' ', '.', ',', '(', ')', '[', ']', '{', '}', '+', '-', '*', '/', ';', '<', '>', '"' };
            foreach (var file in Directory.EnumerateFiles(root))
            {
                var keywords = new Dictionary<string, Keyword>();
                var lineNumber = 0;
                foreach (var line in File.ReadLines(file))
                {
                    var words = line.Split(ignoreList, StringSplitOptions.RemoveEmptyEntries).Distinct();
                    lineNumber++;

                    foreach (var word in words)
                    {
                        if (!keywords.ContainsKey(word))
                        {
                            keywords.Add(word, new Keyword
                            {
                                Word = word,
                                PhysicalPath = file,
                                LineNumbers = new HashSet<int> { lineNumber }
                            });
                        }
                        else
                        {
                            keywords[word].LineNumbers.Add(lineNumber);
                        }
                    }
                }

                foreach (var pair in keywords)
                {
                    //ElasticSearch.Index(pair.Key, pair.Value);
                    Console.WriteLine("keyword: {0}  path: {1}  lines: {2}", pair.Key, pair.Value.PhysicalPath, string.Join(",", pair.Value.LineNumbers));
                }
            } 

            Console.WriteLine("finished");
        }
    }
}
