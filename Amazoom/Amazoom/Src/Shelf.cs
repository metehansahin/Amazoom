using System;
using System.Collections.Generic;

namespace Amazoom
{
    public class Shelf
    {
        private int level;
        private double weight_capacity;
        private double volume_capacity;
        private double isAtWeightCapacity;
        private double currentWeight;
        private List<Item> items;
        public int shelfId { get; private set; }

        // Shelf constructors
        public Shelf(int level, double weight_capacity, int shelfId)
        {
            this.shelfId = shelfId;
            this.level = level;
            this.weight_capacity = weight_capacity;
            items = new List<Item>();
            currentWeight = 0;
        }
        public Shelf(int shelfId)
        {
            this.shelfId = shelfId;
            this.level = 1;
            this.weight_capacity = 5000;
            this.volume_capacity = 10000;
            currentWeight = 0;
            items = new List<Item>();
        }

        // getters and setters
        // For how to use them: https://docs.microsoft.com/en-us/dotnet/api/system.reflection.propertyinfo.getaccessors?view=net-6.0
        public int Level { get => level; set => level = value; }
        public double Weight_capacity { get => weight_capacity; set => weight_capacity = value; }
        public double Volume_capacity { get => volume_capacity; set => volume_capacity = value; }
        public double CurrentWeight { get => currentWeight; set => currentWeight = value; }
        public List<Item> Items { get => items; set => items = value; }

        public bool addItemtoShelf(Item newItem)
        {
            
            if (currentWeight + newItem.ItemWeight <= weight_capacity)
            {
                items.Add(newItem);
                currentWeight =+ newItem.ItemWeight;
                return true;
            }
            else
            {
                return false;
            }

        }

        public void removeItemfromShelf(Item itemToRemove)
        {
            for(int i = 0; i < items.Count; i++)
            {
                if (items[i].ItemID == itemToRemove.ItemID)
                {
                    items.RemoveAt(i);
                    break;
                }
            }
            

        }

        public bool isAtCapacity()
        {   
            
            return (currentWeight >= weight_capacity) ;
        }
      
    }
}
