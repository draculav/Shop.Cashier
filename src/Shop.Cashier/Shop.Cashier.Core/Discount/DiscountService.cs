using Shop.Cashier.Core;
using Shop.Cashier.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Cashier.Core.Discount
{
    public class DiscountService
    {
        public static Goods CalculateDiscount(Goods goodsOne)
        {
            //改为注入
            CalculateTwoGiveOne(goodsOne);

            return goodsOne;
        }

        private static void CalculateTwoGiveOne(Goods goodsOne
            , string property = "Count", int countAttain = 3, int countGive = 1)
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

        /// <summary>
        /// get entries which use by setting discount on Goods 
        /// </summary>
        /// <returns>entries key and desc</returns>
        public Dictionary<string, string> GetDiscountSettingEntry()
        {
            Dictionary<string, string> entries = new Dictionary<string, string>();
            DiscountAttribute[] attrs = DiscountAttribute.Gets(typeof(Goods));
            foreach (var one in attrs)
            {
                entries.Add(one.UsedBy, one.UsedByDesc);
            }
            return entries;
        }

        //disable the one which priority level is lower  if two discount condition are conflict
    }
}
