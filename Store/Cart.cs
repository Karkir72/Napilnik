using System;
using System.Collections.Generic;

namespace Napilnik
{
    public class Cart
    {
        private Warehouse _warehouse;
        private Dictionary<Good, int> _goods = new Dictionary<Good, int>();

        public Cart(Warehouse warehouse)
        {
            if (warehouse == null)
                throw new ArgumentNullException();

            _warehouse = warehouse;
        }

        public void Add(Good good, int count)
        {
            if (good == null)
                throw new ArgumentNullException();

            if (_warehouse.HasGoods(good, count))
                _goods.Add(good, count);
            else
                throw new Exception("Товаров нет на складе");
        }

        public void ShowAllGoods()
        {
            Console.WriteLine("Товары в корзине:");

            if (_goods.Count == 0)
                Console.WriteLine("Пусто.");
            else
                foreach (var good in _goods)
                    Console.WriteLine("\n" + good.Key.Name + " " + good.Value);
        }
    }
}
