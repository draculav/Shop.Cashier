using Shop.Cashier.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Cashier.Core
{
    public class Goods
    {
        public int Id { get; set; }

        [Discount(UsedBy = "UnitPrice", UsedByDesc = "单价")]
        public decimal UnitPrice { get; set; }

        [Discount(UsedBy = "Kind", UsedByDesc = "种类")]
        public string Kind { get; set; }

        [Discount(UsedBy = "Count", UsedByDesc = "数量")]
        public int Count { get; set; }

        public string Barcode { get; set; }

        [Discount(UsedBy = "Price", UsedByDesc = "总价")]
        public decimal Pay { get; set; }

        public decimal SaveCost { get; set; }
    }
}
