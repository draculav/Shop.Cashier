using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Cashier.Util
{
    public enum OrderState
    {
        WaitReceipt = 1,
        Receipted = 2,
        Canceled = 3
    }
}
