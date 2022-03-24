using Erp.Domain.Sale.Entities;
using Erp.Domain.Sale.Helper;
using Erp.Domain.Sale.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Erp.Domain.Sale.Repositories
{
    public class ProductOrServiceRepository : GenericRepository<ErpSaleDbContext, Product>, IProductOrServiceRepository
    {
        public ProductOrServiceRepository(ErpSaleDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Get all Product
        /// </summary>
        /// <returns>Product list</returns>
        /// 
        public IQueryable<Product> GetAllProduct()
        {
            return Context.Product.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public List<Product> GetlistAllProduct()
        {
            return Context.Product.Where(item => (item.IsDeleted == null || item.IsDeleted == false)).ToList();
        }
        
        public Product GetProductByCode(string code)
        {
            return Context.Product.FirstOrDefault(x => x.Code.Equals(code) && (x.IsDeleted == null || x.IsDeleted == false));
        }
        public IQueryable<vwProductAndService> GetAllvwProductAndService()
        {
            return Context.vwProductAndService.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwProduct> GetAllvwProduct()
        {
            return Context.vwProduct.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public List<vwProduct> GetAllvwProductList()
        {
            return Context.vwProduct.Where(item => (item.IsDeleted == null || item.IsDeleted == false)).ToList();
        }


        public IQueryable<vwProduct1> GetAllvwProduct1()
        {
            return Context.vwProduct1.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }


        public IQueryable<vwProduct2> GetAllvwProduct2()
        {
            return Context.vwProduct2.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<vwProductsamegroup> GetAllvwProductsamegroup()
        {
            return Context.vwProductsamegroup;
        }
        public IQueryable<vwProductsamesize> GetAllvwProductsamesize()
        {
            return Context.vwProductsamesize;
        }

        public IQueryable<vwService> GetAllvwService()
        {
            return Context.vwService.Where(item => (item.IsDeleted == null || item.IsDeleted == false));
        }
        public IQueryable<Product> GetAllProductByType(string type)
        {
            return Context.Product.Where(item => item.Type.Contains(type) && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public IQueryable<vwProduct> GetAllvwProductByType(string type)
        {
            return Context.vwProduct.Where(item => item.Type.Contains(type) && (item.IsDeleted == null || item.IsDeleted == false));
        }


        /// <summary>
        /// Get Product information by specific id
        /// </summary>
        /// <param name="ProductId">Id of Product</param>
        /// <returns></returns>
        
        public Product GetProductById(int Id)
        {
            return Context.Product.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public Sale_Product_samegroup GetSale_Product_samegroupById(int Id)
        {
            return Context.Sale_Product_samegroup.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public Sale_Product_samegroup GetProduct_samegroupById(int Id)
        {
            return Context.Sale_Product_samegroup.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public Sale_Product_samesize GetProduct_samesizeById(int Id)
        {
            return Context.Sale_Product_samesize.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public vwProduct_PromotionNew GetAllvwProduct_Promotion(int Id)
        {
            return Context.vwProduct_Promotion.Where(item => item.ProductId == Id).FirstOrDefault();
        }
        public vwProduct GetvwProductById(int Id)
        {
            return Context.vwProduct.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public decimal GetvwProduc_PromotiontById(int Id)
        {
            //vwProduct product = Context.vwProduct.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
            var data = SqlHelper.QuerySP<decimal>("GETProductSalesPriceid", new
            {
                id = Id
              
            }).First().ToString();
            return decimal.Parse(data);
          
        }
        public vwService GetvwServiceById(int Id)
        {
            return Context.vwService.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        /// <summary>
        /// Insert Product into database
        /// </summary>
        /// <param name="Product">Object infomation</param>
        public void InsertProduct(Product Product)
        {
            Context.Product.Add(Product);
            Context.Entry(Product).State = EntityState.Added;
            Context.SaveChanges();
        }


        public void InsertProduct_samegroupt(Sale_Product_samegroup product_samegroup)
        {
            Context.Sale_Product_samegroup.Add(product_samegroup);
            Context.Entry(product_samegroup).State = EntityState.Added;
            Context.SaveChanges();
        }

        public void InsertProduct_samesize(Sale_Product_samesize product_samesize)
        {
            Context.Sale_Product_samesize.Add(product_samesize);
            Context.Entry(product_samesize).State = EntityState.Added;
            Context.SaveChanges();
        }
        public void InsertService(Product Service)
        {
            Context.Product.Add(Service);
            Context.Entry(Service).State = EntityState.Added;
            Context.SaveChanges();
        }


        /// <summary>
        /// Delete Product with specific id
        /// </summary>
        /// <param name="Id">Product Id</param>
        public void DeleteProduct(int Id)
        {
            Product deletedProduct = GetProductById(Id);
            Context.Product.Remove(deletedProduct);
            Context.Entry(deletedProduct).State = EntityState.Deleted;
            Context.SaveChanges();
        }


        public void DeleteProductsamegroupt(int Id)
        {
            Sale_Product_samegroup deletedProduct = GetProduct_samegroupById(Id);
            Context.Sale_Product_samegroup.Remove(deletedProduct);
            Context.Entry(deletedProduct).State = EntityState.Deleted;
            Context.SaveChanges();
        }


        public void DeleteProductsamesize(int Id)
        {
            Sale_Product_samesize deletedProduct = GetProduct_samesizeById(Id);
            Context.Sale_Product_samesize.Remove(deletedProduct);
            Context.Entry(deletedProduct).State = EntityState.Deleted;
            Context.SaveChanges();
        }

      


        public void DeleteService(int Id)
        {
            Product deletedService = GetProductById(Id);
            Context.Product.Remove(deletedService);
            Context.Entry(deletedService).State = EntityState.Deleted;
            Context.SaveChanges();
        }
        /// <summary>
        /// Delete a Product with its Id and Update IsDeleted IF that Product has relationship with others
        /// </summary>
        /// <param name="ProductId">Id of Product</param>
        public void DeleteProductRs(int Id)
        {
            Product deleteProductRs = GetProductById(Id);
            deleteProductRs.IsDeleted = true;
            UpdateProduct(deleteProductRs);
        }
        public void DeleteServiceRs(int Id)
        {
            Product deleteServiceRs = GetProductById(Id);
            deleteServiceRs.IsDeleted = true;
            UpdateService(deleteServiceRs);
        }
        /// <summary>
        /// Update Product into database
        /// </summary>
        /// <param name="Product">Product object</param>
        public void UpdateProduct(Product Product)
        {
            Context.Entry(Product).State = EntityState.Modified;
            Context.SaveChanges();
        }
        public void UpdateService(Product Service)
        {
            Context.Entry(Service).State = EntityState.Modified;
            Context.SaveChanges();
        }


        public void InsertServiceCombo(vwService Service, List<ServiceCombo> orderDetails)
        {
            Context.vwService.Add(Service);
            Context.Entry(Service).State = EntityState.Added;
            Context.SaveChanges();
            for (int i = 0; i < orderDetails.Count; i++)
            {
                orderDetails[i].ComboId = Service.Id;
                InsertServiceCombo(orderDetails[i]);
            }

            //return ServiceInvoice.Id;
        }
        public void InsertServiceCombo(ServiceCombo ServiceCombo)
        {
            Context.ServiceCombo.Add(ServiceCombo);
            Context.Entry(ServiceCombo).State = EntityState.Added;
            Context.SaveChanges();
        }
        public ServiceCombo GetServiceComboById(int Id)
        {
            return Context.ServiceCombo.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }
        public void DeleteServiceCombo(IEnumerable<ServiceCombo> list)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                ServiceCombo deletedServiceCombo = GetServiceComboById(list.ElementAt(i).Id);
                Context.ServiceCombo.Remove(deletedServiceCombo);
                Context.Entry(deletedServiceCombo).State = EntityState.Deleted;
            }
            Context.SaveChanges();
        }

        public Sale_Product_SKU GetProduct_SKUById(int Id)
        {
            return Context.Sale_Product_sku.SingleOrDefault(item => item.Id == Id && (item.IsDeleted == null || item.IsDeleted == false));
        }

        public void DeleteProductSKU(int Id)
        {
            Sale_Product_SKU deletedProduct = GetProduct_SKUById(Id);
            Context.Sale_Product_sku.Remove(deletedProduct);
            Context.Entry(deletedProduct).State = EntityState.Deleted;
            Context.SaveChanges();
        }

        public IQueryable<vwProductSKU> GetAllvwProductSKU()
        {
            return Context.vwProductsku;
        }

        public void InsertProduct_SKU(Sale_Product_SKU product_sku)
        {
            Context.Sale_Product_sku.Add(product_sku);
            Context.Entry(product_sku).State = EntityState.Added;
            Context.SaveChanges();
        }
    }
}
