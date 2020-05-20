﻿using MetX.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MetX.Tests
{


    [TestClass]
    public class StringExtensionsTests
    {
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod]
        public void TokensAroundTest()
        {
            const string target = "aaa((bb))ccc";
            Assert.AreEqual("aaa)ccc", target.TokensAround("(", ")"));
            Assert.AreEqual("aaaccc", target.TokensAround("((", "))"));
            Assert.AreEqual("aaa()ccc", target.TokensAround("(b", "b)"));
        }

        [TestMethod]
        public void MidTest()
        {
            const string target = "abcdefg";

            string actual = target.Mid(3, 3);
            Assert.AreEqual("def", actual);

            actual = target.Mid(6, 3);
            Assert.AreEqual("g", actual);

            actual = target.Mid(0, 3);
            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void LeftTest()
        {
            const string target = "abcdefg";

            Assert.AreEqual("abc", target.Left(3));
            Assert.AreEqual("", target.Left(0));
            Assert.AreEqual("abcdefg", target.Left(10));
            Assert.AreEqual(target, target.Left(7));            
        }
    }
}
