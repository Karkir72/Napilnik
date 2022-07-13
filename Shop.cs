using System;

namespace Napilnik
{
    public class Shop
    {
        private Warehouse _warehouse;
        private Cart _cart;

        public Shop(Warehouse warehouse)
        {
            _warehouse = warehouse;
        }

        public Cart Cart()
        {
            _cart = new Cart(_warehouse);
            return _cart;
        }
    }
}
