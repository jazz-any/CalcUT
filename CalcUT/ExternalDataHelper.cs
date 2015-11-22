using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace CalcUT
{
    public static class ExternalDataHelper
    {
        public static List<List<string>> Tests { get; private set; }

        //static ExternalDataHelper()
        //{
        //    const string pathToTestData = "/CalcTestData.csv";

        //    var testLines = File.ReadAllLines(pathToTestData);

        //    Tests = testLines.Select(testString => testString.Split(';').ToList()).ToList();
        //}

        public static IEnumerable<XElement> Parser()
        {
            var testFile = XDocument.Load(@"D:\users\anastasiia.vasenko\Documents\Visual Studio 2015\Projects\CalcUT\CalcUT\TestCasesData.xml");

            return testFile.Root.Elements("Test");
        }

    }
}