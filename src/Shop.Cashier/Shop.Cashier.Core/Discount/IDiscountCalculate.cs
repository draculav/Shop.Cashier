using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Cashier.Core.Discount
{
    public interface IDiscountCalculate
    {
        void exec(Goods goodsOne, string condition);
    }
}
