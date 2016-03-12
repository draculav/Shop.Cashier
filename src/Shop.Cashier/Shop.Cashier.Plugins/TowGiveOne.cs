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
    public class TowGiveOne : IDiscountCalculate
    {
        /// <summary>
        /// calculate Saving amount from the three for two
        /// </summary>
        /// <param name="goodsOne"></param>
        /// <param name="condition">{"col1":"Count","attain":"3","give":"1"}</param>
        public void exec(Goods goodsOne, string condition)
        {
            string property = Util.JsonHelper.GetValueInJson(condition, "col1");
            if (property.Equals("")) { return; }

            int countAttain = Util.JsonHelper.GetIntValueInJson(condition, "attain");
            if (countAttain <= 0) { return; }

            int countGive = Util.JsonHelper.GetIntValueInJson(condition, "give");
            if (countAttain <= 0) { return; }

            try
            {
                calculate(goodsOne, property, countAttain, countGive);
            }
            catch
            { 
                //log 
            }
        }

        private void calculate(Goods goodsOne, string property, int countAttain, int countGive)
        {
            ParameterExpression obj = Expression.Parameter(typeof(Goods), "g");
            ConstantExpression para = Expression.Constant(countAttain);
            MemberExpression propertyValue = Expression.PropertyOrField(obj, property);

            var query = Expression.Divide(propertyValue, para);
            var lambda = Expression.Lambda<Func<Goods, int>>(query, obj);
            var quotient = lambda.Compile().Invoke(goodsOne);

            goodsOne.SaveCost = quotient * goodsOne.UnitPrice * countGive;
            goodsOne.Pay = goodsOne.Pay - goodsOne.SaveCost;
        }
    }
}
