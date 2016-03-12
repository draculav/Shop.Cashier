using Shop.Cashier.Core;
using Shop.Cashier.Core.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Cashier.Plugins
{
    public class PriceRebate : IDiscountCalculate
    {
        /// <summary>
        /// calculate amount of the rebate
        /// </summary>
        /// <param name="goodsOne"></param>
        /// <param name="condition">{"col1":"Price","discount":"0.95"}</param>
        public void exec(Goods goodsOne, string condition)
        {
            string property = Util.JsonHelper.GetValueInJson(condition, "col1");
            if (property.Equals("")) { return; }

            decimal discount = Util.JsonHelper.GetIntValueInJson(condition, "discount");
            if (discount <= 0) { return; }

            try
            {
                calculate(goodsOne, property, discount);
            }
            catch
            {
                //log 
            }
        }

        private void calculate(Goods goodsOne, string property, decimal discount)
        {
            ParameterExpression obj = Expression.Parameter(typeof(Goods), "g");
            ConstantExpression para = Expression.Constant(discount);
            MemberExpression propertyValue = Expression.PropertyOrField(obj, property);

            var query = Expression.Multiply(propertyValue, para);
            var lambda = Expression.Lambda<Func<Goods, int>>(query, obj);
            var quotient = lambda.Compile().Invoke(goodsOne);

            goodsOne.SaveCost = goodsOne.Pay - quotient;
            goodsOne.Pay = quotient;
        }
    }
}
