using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.Unity;

using AngelList;
using AngelList.Query;


namespace LoadInvestorIndustries.Test
{
    [TestClass]
    public class QueryParameterParserUnitTest
    {

        [TestMethod]
        public void RestrictIntRange_Success()
        {
            var paramParser = new QueryParameterParser(2, 2);

            int end = paramParser.RestrictIntRange(1, 1);
            Assert.AreEqual(1, end);

            end = paramParser.RestrictIntRange(1, Int32.MaxValue);
            Assert.AreEqual(4, end);

            end = paramParser.RestrictIntRange(1, paramParser.MaxRangeLength);
            Assert.AreEqual(2, end);

            end = paramParser.RestrictIntRange(1, paramParser.MaxRangeLength + 1);
            Assert.AreEqual(paramParser.MaxRangeLength + 1, end);

            end = paramParser.RestrictIntRange(1, paramParser.MaxRangeLength * paramParser.MaxRanges);
            Assert.AreEqual(paramParser.MaxRangeLength * paramParser.MaxRanges, end);

            end = paramParser.RestrictIntRange(1, paramParser.MaxRangeLength * paramParser.MaxRanges + 1);
            Assert.AreEqual(paramParser.MaxRangeLength * paramParser.MaxRanges, end);

        }

        [TestMethod]
        public void ParseIntRange_MaxRange_Success()
        {
            var paramParser = new QueryParameterParser(2, 2);

            List<List<int>> ranges = paramParser.ParseIntRange(1, paramParser.MaxRangeLength * paramParser.MaxRanges);

            Assert.IsNotNull(ranges);
            Assert.AreEqual(paramParser.MaxRanges, ranges.Count);

            foreach (var range in ranges)
            {
                Assert.AreEqual(paramParser.MaxRangeLength, range.Count);
            }

        }

        [TestMethod]
        public void ParseIntRange_1_1_Success()
        {
            var paramParser = new QueryParameterParser(2, 2);

            List<List<int>> ranges = paramParser.ParseIntRange(1, 1);

            Assert.IsNotNull(ranges);
            Assert.AreEqual(1, ranges.Count);
            Assert.AreEqual(1, ranges[0].Count);
        }

        [TestMethod]
        public void ParseIntRange_1_2_Success()
        {
            var paramParser = new QueryParameterParser(2, 2);

            List<List<int>> ranges = paramParser.ParseIntRange(1, 2);

            Assert.IsNotNull(ranges);
            Assert.AreEqual(1, ranges.Count);
            Assert.AreEqual(2, ranges[0].Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseIntRange_MaxRangesExceed_Exception()
        {
            var paramParser = new QueryParameterParser(2, 2);

            List<List<int>> ranges = paramParser.ParseIntRange(1, 1 + paramParser.MaxRangeLength * paramParser.MaxRanges);

        }
    }
}
