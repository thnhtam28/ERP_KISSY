using ERP_API.Filters;
using ERP_API.Operation.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_API.Models.ViewModel
{
    public class ShoppingCartItem
    {
        public int ProductID { get; set; }
        public string Photo { get; set; }
        public string ProductName { get; set; }
        public string ProductSumary { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }
        public int TypeProduct { get; set; }
        public int DealHotID { get; set; }
        public string SKU { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }

        public int? PromotionId { get; set; }
        public int? PromotionDetailId { get; set; }
        public decimal? PromotionValue { get; set; }
        public bool IsMoney { get; set; }
    }
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            ListItem = new List<ShoppingCartItem>();
           
        }
        private ShoppingCartItem ChangeImageName(ShoppingCartItem sale_Product)
        {
            if (sale_Product != null)
            {
                sale_Product.Photo = Common.GetUrlImage(sale_Product.Photo, "product-image-folder-client", "product");

            }
            return sale_Product;
        }
        public List<ShoppingCartItem> ListItem { get; set; }

        public int? PhiShip { get; set; }
        public string ThanhPho { get; set; }
        public string QuanHuyen { get; set; }


        public int? PromotionId { get; set; }
        public int? PromotionDetailId { get; set; }
        public decimal? PromotionValue { get; set; }
        public bool IsMoney { get; set; }

        public bool AddToCart(ShoppingCartItem item)
        {
            bool alreadyExists = ListItem.Any(x => x.ProductID == item.ProductID);
            if (alreadyExists)
            {
                ShoppingCartItem existsItem = ListItem.FirstOrDefault(x => x.ProductID == item.ProductID);
                if (existsItem != null)
                {
                    existsItem.Quantity = item.Quantity;
                    existsItem.Total = existsItem.Quantity * existsItem.Price;
                }
            }
            else
            {
                ListItem.Add(item);
            }
            return true;
        }
        public int CountItemCart()
        {
            int count = 0;
            count = ListItem.Count;
            return count;
        }
        public decimal GetKhuyenMaiTheoHD()
        {
            KhuyenMaiOperation khuyenMaiOperation = new KhuyenMaiOperation();
            decimal giatri = 0;
            giatri = ListItem.Sum(x => x.Total) == null ? 0 : (decimal)(ListItem.Sum(x => x.Total));
            return khuyenMaiOperation.GetKhuyenMaiTHeoHoaDon(giatri);
        }


        public decimal GetKhuyenMaiTheoHD_hoapd(ref ShoppingCartItem_hoadon km)
        {
            KhuyenMaiOperation khuyenMaiOperation = new KhuyenMaiOperation();
            decimal giatri = 0;
            giatri = ListItem.Sum(x => x.Total) == null ? 0 : (decimal)(ListItem.Sum(x => x.Total));
            return khuyenMaiOperation.GetKhuyenMaiTHeoHoaDon_hoapd(giatri,ref km);
        }
        public bool RemoveFromCart(int lngProductSellID)
        {
            ShoppingCartItem existsItem = ListItem.Where(x => x.ProductID == lngProductSellID).SingleOrDefault();
            if (existsItem != null)
            {
                ListItem.Remove(existsItem);
            }
            return true;
        }
        public ShoppingCartItem GetItem(int id)
        {
            ShoppingCartItem existsItem = ListItem.Where(x => x.ProductID == id).SingleOrDefault();
            if (existsItem != null)
                return existsItem;
            else
                return null;
        }
        public bool UpdateQuantity(int lngProductSellID, int intQuantity)
        {
            ShoppingCartItem existsItem = ListItem.Where(x => x.ProductID == lngProductSellID).SingleOrDefault();
            if (existsItem != null)
            {
                existsItem.Quantity = intQuantity;
                existsItem.Total = existsItem.Quantity * existsItem.Price;
            }
            return true;
        }
        public ShoppingCartItem_hoadon GetPriceKhuyenMai()
        {
            decimal? giatri = ListItem.Sum(x => x.Total);
            ShoppingCartItem_hoadon km = new ShoppingCartItem_hoadon();
            km.TienKM = giatri - GetKhuyenMaiTheoHD_hoapd(ref km);
            return km;
        }
        public decimal? GetTotal()
        {
            if (PhiShip == null)
                PhiShip = 0;
            decimal tienKm = GetKhuyenMaiTheoHD();
            //decimal? giatri = ListItem.Sum(x => x.Total);
            return (tienKm + PhiShip);
        }
        public decimal? GetTotalTruocKM()
        {
            if (PhiShip == null)
                PhiShip = 0;

            decimal? giatri = ListItem.Sum(x => x.Total);
            return (giatri + PhiShip);
        }

        public bool EmptyCart()
        {
            ListItem.Clear();
            return true;
        }
    }
}