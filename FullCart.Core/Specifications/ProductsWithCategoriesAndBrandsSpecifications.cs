using FullCart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCart.Core.Specifications
{
    public class ProductsWithCategoriesAndBrandsSpecifications : BaseSpecifications<Product>
    {
        public ProductsWithCategoriesAndBrandsSpecifications(ProductSpecParams productParams) : base(x =>
        (
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
            (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId)
        ))
        {
            AddInclude(x => x.Category);
            AddInclude(x => x.Brand);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);
            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price); break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price); break;
                    default: AddOrderBy(p => p.Name); break;
                }

            }
        }
        public ProductsWithCategoriesAndBrandsSpecifications(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Category);
        }
    }
}
