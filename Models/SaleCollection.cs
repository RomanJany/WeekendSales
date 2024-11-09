using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml;

namespace WeekendSales.Models
{
    /// <summary>
    /// Serves as a read-only collection of sale objects.
    /// </summary>
    public class SaleCollection
    {
        private Collection<Sale> _saleCollection;
        
        /// <summary>
        /// Initializes a new SaleCollection filled with data read from a supplied path.
        /// </summary>
        /// <param name="path">A path to an XML file containing sales.</param>
        public SaleCollection(string path)
        {
            _saleCollection = new Collection<Sale>();
            Open(path);
        }

        /// <summary>
        /// Gets the sale at the specified index.
        /// </summary>
        public Sale this[int index]
        {
            get => _saleCollection[index];
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
            EndTax,
            Text
        }

        /// <summary>
        /// Fills the collection containing sale with data contained inside a XML file.
        /// </summary>
        /// <param name="path">A path to an XML file containing sales.</param>
        private void Open(string path)
        {            
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;

            using XmlReader reader = XmlReader.Create(path, settings);

            // Read Sales
            if (!reader.Read() || 
                ReadElement(reader, out string text) != _salesElement.Sales)
            {
                throw new InvalidDataException();
            }

            Collection<Sale> saleCollection = new Collection<Sale>();
            while (reader.Read())
            {
                // Read end Sales (done reading)
                if (ReadElement(reader, out text) == _salesElement.EndSales)
                {
                    _saleCollection = saleCollection;
                    return;
                }

                // Read Sale
                if (ReadElement(reader, out text) != _salesElement.Sale)
                {
                    throw new InvalidDataException();
                }

                // Read itemName
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.ItemName)
                {
                    throw new InvalidDataException();
                }

                // Read itemName text
                if (!reader.Read() ||
                    ReadElement(reader, out string itemName) != _salesElement.Text)
                {
                    throw new InvalidDataException();
                }

                // Read end itemName
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.EndItemName)
                {
                    throw new InvalidDataException();
                }

                // Read date
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.Date)
                {
                    throw new InvalidDataException();
                }

                // Read date text
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.Text)
                {
                    throw new InvalidDataException();
                }
                DateTime date = DateTime.Parse(text, new CultureInfo("cs"));

                // Read end date
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.EndDate)
                {
                    throw new InvalidDataException();
                }

                // Read price
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.Price)
                {
                    throw new InvalidDataException();
                }

                // Read price text
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.Text)
                {
                    throw new InvalidDataException();
                }
                double price = Convert.ToDouble(text);

                // Read end price
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.EndPrice)
                {
                    throw new InvalidDataException();
                }

                // Read tax
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.Tax)
                {
                    throw new InvalidDataException();
                }

                // Read tax text
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.Text)
                {
                    throw new InvalidDataException();
                }
                double tax = Convert.ToDouble(text);

                // Read end tax
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.EndTax)
                {
                    throw new InvalidDataException();
                }

                // Read end Sale
                if (!reader.Read() ||
                    ReadElement(reader, out text) != _salesElement.EndSale)
                {
                    throw new InvalidDataException();
                }

                // Save the current sale
                saleCollection.Add(new Sale { ItemName=itemName, Date=date, Price=price, Tax=tax });
            }
            
            // exited while without ending with end Sales
            throw new InvalidDataException();
        }

        /// <summary>
        /// Reads current node from the reader and checks the correct depth.
        /// </summary>
        /// <param name="reader">An XmlReader which is used to read the current node</param>
        /// <param name="text">Used for outputting text, is set to "" unless _salesElement.Text is returned</param>
        /// <returns>Returns the type of an element found, or InvalidElement on error.</returns>
        private _salesElement ReadElement(XmlReader reader, out string text)
        {
            // Text
            if (reader.NodeType == XmlNodeType.Text &&
                reader.Depth == 3)
            {
                text = reader.Value;
                return _salesElement.Text;
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

        /// <summary>
        /// Gets the number of elements contained in the SaleCollection.
        /// </summary>
        public int Count => _saleCollection.Count;
    }
}
