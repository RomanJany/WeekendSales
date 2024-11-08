using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WeekendSales.Models
{
    public class SaleCollection
    {
        private Collection<Sale> _collection;
        
        public SaleCollection(string path)
        {
            _collection = new Collection<Sale>();
            Open();
        }

        public Sale this[int index]
        {
            get => _collection[index];
        }

        private enum _salesElement
        {
            InvalidElement,
            Sales,
            EndSales,
            Sale,
            EndSale,
            ItemName,
            EndItemName,
            Date,
            EndDate,
            Price,
            EndPrice,
            Tax,
            EndTax
        }

        private void Open()
        {

        }

        private _salesElement ReadElement(XmlReader reader, out string text)
        {
            throw new NotImplementedException();
        }

        public int Count => _collection.Count;
    }
}
