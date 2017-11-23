using Microsoft.VisualStudio.TestTools.UnitTesting;
using Morse.Library;
using System;
using System.IO;
using System.Threading;

namespace Morse.Tests {
    [TestClass]
    public class UnitTest {

        [DataTestMethod]
        [DataRow("in1.txt", "sol1.txt")]
        [TestMethod]
        public void TestDelegateParameter(string inputPath, string solPath) {
            using (StreamReader insr = new StreamReader(inputPath))
            using (StreamReader solsr = new StreamReader(solPath))
                MorseParser.Parse(insr.Read, (char c) => Assert.AreEqual(solsr.Read(), c));
        }


        [DataTestMethod]
        [Timeout(50)]
        [DataRow("in1.txt", "sol1.txt")]
        [DataRow("in2.txt", "sol2.txt")]
        [TestMethod]
        [Ignore("only successful on powerful machines")]
        public void TestSpeed(string inputPath, string solPath) {
            using (StreamReader insr = new StreamReader(inputPath))
            using (StreamReader solsr = new StreamReader(solPath))
                MorseParser.Parse(insr.Read, (char c) => Assert.AreEqual(solsr.Read(), c));
        }

        [DataTestMethod]
        [DataRow(".  ")]//2 spaces
        [DataRow(".     ")]//5 spaces
        [DataRow(".   ")]//3 spaces
        [ExpectedException(typeof(FormatException))]
        [TestMethod]
        public void TestWrongNumberOfSpaces(string text) {
            MorseParser.Parse(text);
        }

        [DataTestMethod]
        [DataRow(".g")]
        [DataRow(". k")]
        [DataRow(".    k")]
        [ExpectedException(typeof(FormatException))]
        [TestMethod]
        public void TestWrongCharacter(string text) {
            MorseParser.Parse(text);
        }


        [DataTestMethod]
        [DataRow(".-.-. ")]
        [DataRow("-...- ")]
        [DataRow("-.-.- ")]
        [ExpectedException(typeof(FormatException))]
        [TestMethod]
        public void TestWrongPattern(string text) {
            MorseParser.Parse(text);
        }
    }
}
