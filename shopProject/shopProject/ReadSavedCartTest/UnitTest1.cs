using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using shopProject;
using System.IO;
namespace ReadSavedCartTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ReadSavedCartCorrect()
        {
            // Arrange
            Customer customer = new Customer("test.txt");

            // Act


            // Assert
            Assert.AreEqual(customer.Cart.Count, 3);
        }

        [TestMethod]
        public void FileNonExist()
        {
            // Arrange
            Customer customer = new Customer("1234.txt");

            // Act

            // Assert
            Assert.AreEqual(customer.Cart.Count, 0);
        }

        [TestMethod]
        public void ReadCorruptFile()
        {
            // Arrange
            Customer customer = new Customer("test.txt");

            // Act


            // Assert
            Assert.AreEqual(customer.Cart.Count, 3);
        }
        [TestMethod]
        public void ReadValidDiscount()
        {
            // Arrange
            Customer customer = new Customer("1234.txt");

            // Act
           

            // Assert
            Assert.AreEqual(customer.ReadDiscount("nintendo"), true);
            Assert.AreEqual(customer.Discount, 0, 0.85);
        }
        public void ReadNonValidDiscount()
        {
            // Arrange
            Customer customer = new Customer("1234.txt");

            // Act


            // Assert
            Assert.AreEqual(customer.ReadDiscount("xxx"), false);
            Assert.AreEqual(customer.Discount, 1);
        }

    }
}
