using System.Collections.Generic;
using System.Xml.Linq;

namespace CalcUT
{
    public static class ExternalDataHelper
    {
        public static IEnumerable<XElement> GetTestCaseData()
        {
            var testFile = XDocument.Load(@"d:\users\anastasiia.vasenko\documents\visual studio 2015\projects\calcut\calcut\testcasesdata.xml");

            return testFile.Root.Elements("Test");
        }

    }
}