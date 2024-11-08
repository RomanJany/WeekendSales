using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;

namespace WeekendSales.Models
{
    public class SaleCollection
    {
        private Collection<Sale> _collection;
        
        public SaleCollection(string path)
        {
            _collection = new Collection<Sale>();
        }

        public Sale this[int index]
        {
            get => _collection[index];
        }
    }
}
