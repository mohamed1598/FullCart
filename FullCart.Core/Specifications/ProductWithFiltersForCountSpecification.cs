using FullCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecifications<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams) : base
                    (x =>
            (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
                    (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId) &&
                    (string.IsNullOrEmpty(productParams.search) || x.Name.Contains(productParams.search!))
        )
        {

        }
    }
}
