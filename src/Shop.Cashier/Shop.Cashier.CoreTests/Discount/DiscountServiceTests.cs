using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Cashier.Core.Discount;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Shop.Cashier.Core.Discount.Tests
{
    [TestClass()]
    public class DiscountServiceTests
    {
        [TestMethod()]
        public void CalculateDiscountTest()
        {
            decimal unitPrice = 2;
            //int count = 5;
            //decimal result = 2;

            int count = 7;
            decimal result = 4;
            decimal payResult = 10;

            //int count = 2;
            //decimal result = 0;
            //decimal payResult = 4;

            Goods goodsOne = new Goods();
            goodsOne.Count = count;
            goodsOne.UnitPrice = unitPrice;
            goodsOne.Pay = goodsOne.UnitPrice * count;
            goodsOne.UnitPrice = unitPrice;

            DiscountService.CalculateDiscount(goodsOne);

            Assert.AreEqual(goodsOne.SaveCost, result);
            Assert.AreEqual(goodsOne.Pay, payResult);
        }
    }
}
