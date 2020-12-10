using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ase_simplelanguage;

namespace ase_unittesting
{
    [TestClass]
    public class UnitTest1
    {
        Parser p = new Parser();
        Canvas c = new Canvas();


        /// <summary>
        /// Tests whether a valid moveto command is successful
        /// </summary>
        [TestMethod]
        public void TestParseCommandMoveToValid()
        {
            p.parseCommand("MoveTo 100,100", c, c);

            double expected = 100.00;

            Assert.AreEqual(expected, c.xPos, 0.001);
            Assert.AreEqual(expected, c.xPos, 0.001);
        }

        /// <summary>
        /// Tests whether an invalid moveto command successfully produces an exception
        /// </summary>
        [TestMethod]
        public void TestParseCommandMoveToInvalid()
        {
            string actual = "";

            try
            {
                p.parseCommand("MoveTo 100", c, c);
            } 
            catch (ApplicationException e)
            {
                actual = e.Message;
            }

            string expected = "Invalid number of parameters for moveto";

            Assert.AreEqual(expected, actual);
        }
        
        /// <summary>
        /// Tests whether an invalid predefined colour in the pen command successfully produces an exception
        /// </summary>
        [TestMethod]
        public void TestParseCommandPenColourInvalid()
        {
            string actual = "";

            try
            {
                p.parseCommand("pen colourofspace", c, c);
            }
            catch (ApplicationException e)
            {
                actual = e.Message;
            }

            string expected = "Invalid colour for pen";

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// Tests whether the fill command successfully changes the fillOn boolean
        /// </summary>
        [TestMethod]
        public void TestParseCommandFillValid()
        {
            p.parseCommand("fill on", c, c);

            Assert.IsTrue(c.fillOn);
        }


        /// <summary>
        /// Tests whether an invalid fill command successfully produces an exception
        /// </summary>
        [TestMethod]
        public void TestParseCommandFillInvalid()
        {
            string actual = "";

            try
            {
                p.parseCommand("fill no", c, c);
            }
            catch (ApplicationException e)
            {
                actual = e.Message;
            }

            string expected = "Invalid parameter for fill (must be on/off)";

            Assert.AreEqual(expected, actual);
        }


        /// <summary>
        /// Tests whether a valid var command is successful
        /// </summary>
        [TestMethod]
        public void TestParseCommandVarValid()
        {
            p.parseCommand("var iable", c, c);

            string expected = "iable";

            Assert.AreEqual(expected, p.variables[0].Value);
        }

        /// <summary>
        /// Tests whether an invalid var command successfully produces an exception
        /// </summary>
        [TestMethod]
        public void TestParseCommandVarInvalid()
        {
            string actual = "";

            try
            {
                p.parseCommand("var 300", c, c);
            }
            catch (ApplicationException e)
            {
                actual = e.Message;
            }

            string expected = "Invalid parameter type (must be string)";

            Assert.AreEqual(expected, actual);
        }
    }
}
