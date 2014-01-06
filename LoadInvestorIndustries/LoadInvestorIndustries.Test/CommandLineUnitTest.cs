using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using LoadInvestorIndustries;

namespace LoadInvestorIndustries.Test
{
    [TestClass]
    public class CommandLineUnitTest
    {
        [TestMethod]
        public void Parse_Blank_Success()
        {
            int start;
            int end;
            bool parsed = CommandLine.TryParse(new string[0], out start, out end);

            Assert.IsTrue(parsed);
            Assert.AreEqual(1, start);
            Assert.AreEqual(Int32.MaxValue, end);
        }

        [TestMethod]
        public void Parse_1_Blank_Success()
        {
            int start;
            int end;
            string[] args = new string[] { "1" };
            bool parsed = CommandLine.TryParse(args, out start, out end);

            Assert.IsTrue(parsed);
            Assert.AreEqual(1, start);
            Assert.AreEqual(Int32.MaxValue, end);
        }

        [TestMethod]
        public void Parse_1_11_Success()
        {
            int start;
            int end;
            string[] args = new string[] { "1", "11" };
            bool parsed = CommandLine.TryParse(args, out start, out end);

            Assert.IsTrue(parsed);
            Assert.AreEqual(1, start);
            Assert.AreEqual(11, end);
        }


        [TestMethod]
        public void Parse_Help_Success()
        {
            int start;
            int end;
            string[] args = new string[] { "-?" };
            bool parsed = CommandLine.TryParse(args, out start, out end);

            Assert.IsFalse(parsed);
        }

    }
}
