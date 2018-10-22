using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EssentialTools.Models;

namespace EssentialTools.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private IDiscountHelper getTestObject()
        {
            return new MinimumDiscountHelper();
        }

        [TestMethod]
        public void Discount_50M_DefaultDiscountHelper()
        {
            IDiscountHelper target = new DefaultDiscountHelper(50M);
            decimal total = 200;
            var discountedTotal = target.ApplyDiscount(total);
            Assert.AreEqual(total - (50M / 100m * total), discountedTotal);
        }

        [TestMethod]
        public void Discount_Above_100()
        {
            // arrange 
            IDiscountHelper target = getTestObject();
            decimal total = 200;

            // act
            var discountedTotal = target.ApplyDiscount(total);
            // assert
            Assert.AreEqual(total * 0.9M, discountedTotal); 
        }

        [TestMethod]
        public void Discount_Between_10_And_100()
        {
            //arrange            
            IDiscountHelper target = getTestObject();

            // act
            decimal TenDollarDiscount = target.ApplyDiscount(10);
            decimal HundredDollarDiscount = target.ApplyDiscount(100);
            decimal FiftyDollarDiscount = target.ApplyDiscount(50);
            // assert
            Assert.AreEqual(5, TenDollarDiscount, "$10 discount is wrong");
            Assert.AreEqual(95, HundredDollarDiscount, "$100 discount is wrong");
            Assert.AreEqual(45, FiftyDollarDiscount, "$50 discount is wrong"); 
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Discount_Negative_Total()
        {
            //arrange
            IDiscountHelper target = getTestObject();
            // act
            target.ApplyDiscount(-1); 
        }
    }
}
