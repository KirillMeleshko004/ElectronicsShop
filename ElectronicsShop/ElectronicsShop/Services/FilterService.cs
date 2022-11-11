using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicsShop.Services
{
    public class FilterService
    {
        public List<Product> FilterProducts(Product filterProduct)
        {
            return TempServer.FilterProducts(filterProduct);
        }
    }
}
