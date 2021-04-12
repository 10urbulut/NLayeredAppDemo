using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Northwind.Bussiness.Abstract;
using Northwind.Bussiness.Utilities;
using Northwind.Bussiness.ValidationRules.FluentValidation;
using Northwind.DataAccess.Abstract;
using Northwind.DataAccess.Concrete;
using Northwind.DataAccess.Concrete.EntityFramework0;
using Northwind.Entities.Concrete;

namespace Northwind.Bussiness.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            
            _productDal = productDal;
        }

        public void Add(Product product)
        {
            ValidationTool.Validate(new ProductValidator(),product);
            _productDal.Add(product);
        }

        public void Delete(Product product)
        {

            _productDal.Delete(product);


        }

        public List<Product> GetAll()
        {
            // bussiness code
            return _productDal.GetAll();
        }

        public List<Product> GetByProductName(string productName)
        {
            return _productDal.GetAll(p => p.ProductName.ToLower().
            Contains(productName.ToLower()));
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productDal.GetAll(p => p.CategoryId == categoryId);
        }

        public void Update(Product product)
        {
            ValidationTool.Validate(new ProductValidator(), product);
            _productDal.Update(product);
        }
    }
}
