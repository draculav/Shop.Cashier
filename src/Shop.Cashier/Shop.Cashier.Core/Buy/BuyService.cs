using Shop.Cashier.Core.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Cashier.Core.Buy
{
    public class BuyService
    {
        public static Goods AddInOrderAndDiscount(Goods goodsOne, Order order)
        {
            //send id & count & unitprice to discountservice
            DiscountService.CalculateDiscount(goodsOne);

            order.AddInOrder(goodsOne);
            return goodsOne;
        }
    }
}
