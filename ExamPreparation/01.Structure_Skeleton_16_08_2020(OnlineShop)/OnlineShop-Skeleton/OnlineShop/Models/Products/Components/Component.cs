using System;
using System.Collections.Generic;
using System.Text;
using OnlineShop.Models.Products.Components;

namespace OnlineShop.Models.Products
{
   public abstract class Component: Product, IComponent
   {
       private int generation;
     
     
       protected Component(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation) : base(id, manufacturer, model, price, overallPerformance)
       {
           Generation = generation;
       }

       public int Generation
       {
           get { return generation; }
           private set { generation = value; }
       }
        public override string ToString()
        {
            return base.ToString() + $" Generation: {Generation}";
        }
    }
}
