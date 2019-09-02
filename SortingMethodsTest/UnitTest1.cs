using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WindowsFormsApp1.modelo;

namespace SortingMethodsTest
{
    [TestClass]
    public class UnitTest1
    {
        PrincipalProcess principal = new PrincipalProcess();

        [TestMethod]
        public void TestTimsort1()
        {
            int[] actual = { 20, 15, 25, 32, 10, 2, 4, 19 };
            int[] expected = { 2, 4, 10, 15, 19, 20, 25, 32 };
            principal.timSort(ref actual, actual.Length);
           
            
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMergeSort1()
        {
            int[] actual = { 20, 15, 25, 32, 10, 2, 4, 19 };
            int[] expected = { 2, 4, 10, 15, 19, 20, 25, 32 };
            principal.mergeSort(ref actual, 0, actual.Length - 1);


            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
