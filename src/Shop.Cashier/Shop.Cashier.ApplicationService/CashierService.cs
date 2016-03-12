using Shop.Cashier.Core;
using Shop.Cashier.Core.Buy;
using Shop.Cashier.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Cashier.ApplicationService
{
    public class CashierService
    {
        public Order CreateNewTrade()
        {
            return new Order();
        }

        public Goods AddInOrder(long goodsId, int count, Order order)
        {
            //get goods by id in database
            Goods goodsOne = GoodsQuery.Get(goodsId);
            goodsOne.Count = count;
            goodsOne.Pay = goodsOne.UnitPrice * count;

            BuyService.AddInOrderAndDiscount(goodsOne, order);
            return goodsOne;
        }
    }
}
