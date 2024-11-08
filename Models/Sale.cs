using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeekendSales.Models
{
    public class Sale
    {
        required public string ItemName { get; set; }
        required public DateTime Date { get; set; }
        required public double Price { get; set; }
        required public double Tax { get; set; }
    }
}
