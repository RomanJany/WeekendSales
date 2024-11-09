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
            // Text
            if (reader.NodeType == XmlNodeType.Text &&
                reader.Depth == 3)
            {
                text = reader.Value;
            }
            else
            {
                text = "";
            }

            if (reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement)
            {
                // Sales element
                if (reader.Depth == 0 &&
                    reader.Name == "sales")
                {
                    return reader.NodeType == XmlNodeType.Element ? _salesElement.Sales : _salesElement.EndSales;
                }

                // Sale element
                if (reader.Depth == 1 &&
                    reader.Name == "sale")
                {
                    return reader.NodeType == XmlNodeType.Element ? _salesElement.Sale : _salesElement.EndSale;
                }

                if (reader.Depth == 2)
                {
                    // ItemName element
                    if (reader.Name == "itemName")
                    {
                        return reader.NodeType == XmlNodeType.Element ? _salesElement.ItemName : _salesElement.EndItemName;
                    }

                    // Date element
                    if (reader.Name == "date")
                    {
                        return reader.NodeType == XmlNodeType.Element ? _salesElement.Date : _salesElement.EndDate;
                    }

                    // Price element
                    if (reader.Name == "price")
                    {
                        return reader.NodeType == XmlNodeType.Element ? _salesElement.Price : _salesElement.EndPrice;
                    }

                    // Tax element
                    if (reader.Name == "tax")
                    {
                        return reader.NodeType == XmlNodeType.Element ? _salesElement.Tax : _salesElement.EndTax;
                    }
                }
            }


            return _salesElement.InvalidElement;
        }

        public int Count => _collection.Count;
    }
}
