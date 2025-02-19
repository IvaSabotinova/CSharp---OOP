﻿using System;
using System.Collections.Generic;
using System.Text;
using OnlineShop.Models.Products.Peripherals;

namespace OnlineShop.Models.Products.PeriferalsModels
{
  public  abstract class Peripheral : Product, IPeripheral
  {
      private string connectionType;
        protected Peripheral(int id, string manufacturer, string model, decimal price, double overallPerformance, string connectionType) : base(id, manufacturer, model, price, overallPerformance)
        {
            ConnectionType = connectionType;
        }

        public string ConnectionType
        {
            get { return connectionType; }
            private set { connectionType = value; }
        }

        public override string ToString()
        {
            return base.ToString() + $" Connection Type: {ConnectionType}";
        }
  }
}
