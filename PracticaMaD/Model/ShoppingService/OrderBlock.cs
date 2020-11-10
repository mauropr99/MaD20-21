using System.Collections.Generic;


namespace Es.Udc.DotNet.PracticaMaD.Model.ShoppingService
{
    public class OrderBlock
    {
        public List<Order_Table> Orders { get; private set; }
        public bool ExistMoreOrders { get; private set; }

        public OrderBlock(List<Order_Table> orders, bool existMoreOrders)
        {
            this.Orders = orders;
            this.ExistMoreOrders = existMoreOrders;
        }
    }
}
