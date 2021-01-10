using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class OrderBlock
    {
        public List<OrderDetails> Orders { get; private set; }
        public bool ExistMoreOrders { get; private set; }

        public OrderBlock(List<OrderDetails> orders, bool existMoreOrders)
        {
            Orders = orders;
            ExistMoreOrders = existMoreOrders;
        }
    }
}
