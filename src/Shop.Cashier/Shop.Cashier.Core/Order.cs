using Shop.Cashier.Core;
using Shop.Cashier.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Cashier.Core
{
    public class Order
    {
        public OrderState State { get; set; }

        public LinkedList<Goods> GoodsList { get; set; }

        public Goods AddInOrder(Goods goodsOne)
        {
            GoodsList.AddLast(goodsOne);

            return goodsOne;
        }

        public void WaitReceipt()
        {
            State = OrderState.WaitReceipt;

            //check out subtotal
        }

        public void Receipted()
        {
            State = OrderState.Receipted;

            //print
        }

        public void Canceled()
        {
            State = OrderState.Canceled;
        }
    }
}
