﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exercise.main
{
    public class Basket : Inventory

    {
        private bool _isManager;
        private bool _isPurchased;

        private int _capacity;
        private List<InventoryProducts> _items = new List<InventoryProducts>();
        private Inventory _inventory = new Inventory();

        public bool AddToBasket(InventoryProducts bagel)
        {

            if (_capacity > 0)
            {
               _items.Add(bagel);
                _capacity--;

                return true;
            }
            else
            {
                return false;
            }
        }


        public bool RemoveFromBasket(InventoryProducts bagel)
        {
            bool removed = false;


            if (_items.Contains(bagel))
            {
                _items.Remove(bagel);
                _capacity++;
                removed = true;

            }

            return removed;
        }



        public void ChooseFilling(InventoryProducts filling)
        {
            if (filling.Name == "Filling" && _capacity > 0)
            {
                foreach (var item in Inventory.Products)
                {
                    if (item.SKU == filling.SKU)
                    {

                        AddToBasket(filling);
                        _capacity -= 1;

                    }

                }
            }
        }

        

        public void ChangeBasketCapacity(int cap)
        {

            if (_isManager == true)
            {
                _capacity = cap;
            }

        }

        public bool IsFull()
        {

            if (_capacity == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double TotalCost()
        {
            double total = 0;
            foreach (var item in _items)
            {
                total += item.Price;
            }

            return total;
        }

        
        //Extension Task 2
      
        public List<Purchase> ShouldListItems()

        { 
            Dictionary<InventoryProducts, int> products = new Dictionary<InventoryProducts, int>();
            List<Purchase> receipt = new List<Purchase>();

            //Go trhough _items in basket and add them as purchase objects
            foreach (var item in _items)
            {
                if (products.ContainsKey(item))
                {
                    products[item] += 1;

                }
                else
                {
                    products.Add(item, 1);
                }
            }

            foreach(var prod in products)
            {
                prod.Key.Price *= prod.Value;
            }

          foreach (var p in products)
            {
                receipt.Add(new Purchase(p.Key.Variant, p.Key.Name, p.Value, p.Key.Price));
            }


            return receipt;      
        }




        
        public List<InventoryProducts> Items { get { return _items; } }
        public int Capacity { get { return _capacity; } set { _capacity = value; } }
        public bool IsManager { get { return _isManager; } set { _isManager = value; } }
        public bool IsPurchased { get{ return _isPurchased; } set { _isPurchased = value; } }
        public Inventory Inventory { get { return _inventory; } }
    }
}
