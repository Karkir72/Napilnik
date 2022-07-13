using System;

namespace Napilnik
{
    public class Store
    {
        public void Start()
        {
            Good iPhone12 = new Good("iPhone 12");
            Good iPhone11 = new Good("iPhone 11");

            Warehouse warehouse = new Warehouse();

            Shop shop = new Shop(warehouse);

            warehouse.Delive(iPhone12, 10);
            warehouse.Delive(iPhone11, 1);

            warehouse.ShowAllGoods();

            Cart cart = shop.Cart();
            cart.Add(iPhone12, 4);

            cart.ShowAllGoods();
        }
    }
}
