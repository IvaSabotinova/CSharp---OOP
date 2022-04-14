using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities
{
    public abstract class Bag:IBag
    {
        private List<Item> items;
        private int capacity;
        protected Bag(int capacity)
        {
            Capacity = 100;
            items = new List<Item>();
            Capacity = capacity;
        }
        public int Capacity
        {
            get { return capacity;}
            set
            {
                capacity = value;
            }
        }
        public int Load => Items.Sum(x => x.Weight);
        public IReadOnlyCollection<Item> Items => items;
        public void AddItem(Item item)
        {
            int result = Load + item.Weight;
            if (result > Capacity)
            {
                throw new InvalidOperationException(ExceptionMessages.ExceedMaximumBagCapacity);
            }
            items.Add(item);
        }

        public Item GetItem(string name)
        {
            if (items.Count ==0 )
            {
                throw new InvalidOperationException(ExceptionMessages.EmptyBag);
            }

            Item item = items.FirstOrDefault(x => x.GetType().Name == name);
            if (item == null)
            {
                throw new ArgumentException($"No item with name {name} in bag!");
            }

            items.Remove(item);
            return item;

        }

    }
}
