using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ERP_API
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "camnhankhachhang",
            url: "cam-nhan-khach-hang",
            defaults: new { controller = "View_PLayout", action = "camnhankhachhang"}
        );
            routes.MapRoute(
             name: "tintuckhuyenmaichitiet",
             url: "tin-khuyen-mai-chi-tiet/kmtt{id}",
             defaults: new { controller = "View_PLayout", action = "tintuckhuyenmaichitiet", id = UrlParameter.Optional }
         );

            routes.MapRoute(
             name: "tinkhuyenmai",
             url: "tin-khuyen-mai",
             defaults: new { controller = "View_PLayout", action = "tinkhuyenmai" }
         );
            routes.MapRoute(
                name: "tintucnoibatkhuyenmai",
                url: "tin-tuc-noi-bat-va-khuyen-mai",
                defaults: new { controller = "View_PLayout", action = "tintucnoibatkhuyenmai"}
            );
            routes.MapRoute(
                name: "tinnoibat",
                url: "tin-noi-bat",
                defaults: new { controller = "View_PLayout", action = "tinnoibat" }
            );
            routes.MapRoute(
                name: "nhomtintuc",
                url: "nhom-tin/nt{id}/{slug}",
                defaults: new { controller = "View_PLayout", action = "nhomtintuc", id = UrlParameter.Optional, slug = UrlParameter.Optional }
            );

            
            routes.MapRoute(
                name: "tintucchitiet",
                url: "chi-tiet-tin-tuc/tt{id}/{slug}",
                defaults: new { controller = "View_PLayout", action = "tintucchitiet", id = UrlParameter.Optional, slug = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "tintucchitietkissy",
                url: "chi-tiet-tin-tuc-kissy/tt{id}/{slug}",
                defaults: new { controller = "PageDetails", action = "tintucchitietkissy", id = UrlParameter.Optional, slug = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "tintuc",
                url: "tin-tuc",
                defaults: new { controller = "View_PLayout", action = "tintuc" }
            );
            routes.MapRoute(
                name: "Giohang",
                url: "gio-hang",
                defaults: new { controller = "PageDetails", action = "cart" }
            );
            routes.MapRoute(
                  name: "LoginandtRegister",
                  url: "dang-nhap",
                  defaults: new { controller = "View_PLayout", action = "login" }
              );

            routes.MapRoute(
                  name: "Register",
                  url: "dang-ky",
                  defaults: new { controller = "View_PLayout", action = "register" }
              );
            routes.MapRoute(
                 name: "ChitietBaiviet",
                 url: "gioi-thieu-yhl/gt{id}",
                 defaults: new { controller = "View_PLayout", action = "newdetail", id = UrlParameter.Optional }
             );
            routes.MapRoute(
                name: "listBaiviet",
                url: "danh-sach-bai-viet",
                defaults: new { controller = "View_PLayout", action = "lstNew" }
            );

            routes.MapRoute(
             name: "Chitietsanpham",
             url: "Chi-tiet-san-pham-sp-{id}/{slug}",
             defaults: new { controller = "PageDetails", action = "sanphamchitiet", id = UrlParameter.Optional, slug = UrlParameter.Optional }
         );


            routes.MapRoute(
             name: "Loaisanpham",
             url: "Loai-san-pham-{id}/{slug}",
             defaults: new { controller = "View_PLayout", action = "lstproductloaisanpham", id = UrlParameter.Optional, slug = UrlParameter.Optional }
         );

            routes.MapRoute(
              name: "ListSanPhamAll",
              url: "danh-sach-tat-ca-san-pham",
              defaults: new { controller = "View_PLayout", action = "lstAllProduct" }
          );


            routes.MapRoute(
             name: "phuongthucdathang",
             url: "phuong-thuc-dat-hang",
             defaults: new { controller = "View_PLayout", action = "phuongthucdathang" }
         );
            routes.MapRoute(
             name: "chinhsachvanchuyen",
             url: "chinh-sach-van-chuyen",
             defaults: new { controller = "View_PLayout", action = "chinhsachvanchuyen" }
         );
            routes.MapRoute(
           name: "chinhsachdoitra",
           url: "chinh-sach-doi-tra",
           defaults: new { controller = "View_PLayout", action = "chinhsachdoitra" }
       );
            routes.MapRoute(
          name: "lienhe",
          url: "lien-he",
          defaults: new { controller = "View_PLayout", action = "lienhe" }
      );
            routes.MapRoute(
          name: "gioithieuyhl",
          url: "gioi-thieu-yhl",
          defaults: new { controller = "View_PLayout", action = "gioithieuyhl" }
      );
            routes.MapRoute(
         name: "vanhoa",
         url: "van-hoa",
         defaults: new { controller = "View_PLayout", action = "vanhoa" }
     );
            routes.MapRoute(
         name: "dieukhoanmientru",
         url: "dieu-khoan-mien-tru",
         defaults: new { controller = "View_PLayout", action = "dieukhoanmientru" }
     );
            routes.MapRoute(
         name: "chinhsachbaomat",
         url: "chinh-sach-bao-mat",
         defaults: new { controller = "View_PLayout", action = "chinhsachbaomat" }
     );
            routes.MapRoute(
         name: "tuyendung",
         url: "tuyen-dung",
         defaults: new { controller = "View_PLayout", action = "tuyendung" }
     );

            routes.MapRoute(
                name: "ListSanPhamTheoMenu",
                url: "danh-sach-san-pham/ls{id}/{slug}",
                defaults: new { controller = "GroupDetails", action = "lstproduct", id = UrlParameter.Optional, slug = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
