using auctionwebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using auctionwebsite.Helpers;

namespace auctionwebsite.DAL
{
    public class AuctionDataInitializer :System.Data.Entity.DropCreateDatabaseIfModelChanges<AuctionContext>
    {
        protected override void Seed(AuctionContext context)
        {
            var Cateparents = new List<Cateparent>
           {
               new Cateparent{CateparentName="Tủ sách"},
               new Cateparent{CateparentName="Điện gia dụng"},
               new Cateparent{CateparentName="Phụ kiện - Thiết bị số"},
               new Cateparent{CateparentName="Điện thoại - Máy tính bảng"},
               new Cateparent{CateparentName="Máy ảnh - Máy quay phim"}
           };
            Cateparents.ForEach(s => context.Cateparents.Add(s));
            context.SaveChanges();
            var Cates = new List<Cate>
            {
                new Cate{CateName="Sách thiếu nhi",CateparentID=1},
                new Cate{CateName="Sách kinh tế",CateparentID=1},
                new Cate{CateName="Sách văn học",CateparentID=1},
                new Cate{CateName="Sách kỹ năng sống",CateparentID=1},
                new Cate{CateName="Quạt điện",CateparentID=2},
                new Cate{CateName="Tủ lạnh",CateparentID=2},
                new Cate{CateName="Máy giặt",CateparentID=2},
                new Cate{CateName="Máy nước nóng",CateparentID=2},
                new Cate{CateName="Loa máy tính",CateparentID=3},
                new Cate{CateName="Tai nghe",CateparentID=3},
                new Cate{CateName="USB",CateparentID=3},
                new Cate{CateName="Thẻ nhớ",CateparentID=3},
                new Cate{CateName="Nokia",CateparentID=4},
                new Cate{CateName="Iphone",CateparentID=4},
                new Cate{CateName="Samsung",CateparentID=4},
                new Cate{CateName="Asus",CateparentID=4},
                new Cate{CateName="Canon",CateparentID=5},
                new Cate{CateName="Fujifilm",CateparentID=5},
                new Cate{CateName="Sony",CateparentID=5},
                new Cate{CateName="Nikon",CateparentID=5}
            };
            Cates.ForEach(s => context.Cates.Add(s));
            context.SaveChanges();
            string userpassword2 = Helpers.Helpers.EncodePasswordMd5("0919061624");
            var Users = new List<User>
            {
                new User{UserName="phucduye54",Password=userpassword2,UserLevel=2,UserAddress="HCM",UserFirstName="Le",UserEmail="phucduye54@gmail.com",UserCash=1000,UserCity="HCM",UserPassword="0919061624",ConfirmPassword="0919061624",OldPassword="0919061624",UserDOB="9/12/1996",UserLastName="Truc"},
                new User{UserName="phucduye55",Password=userpassword2,UserLevel=1,UserAddress="HCM",UserFirstName="Le",UserEmail="phucduye55@gmail.com",UserCash=1000,UserCity="HCM",UserPassword="0919061624",ConfirmPassword="0919061624",OldPassword="0919061624",UserDOB="9/12/1996",UserLastName="Truc"},
                new User{UserName="phucduye56",Password=userpassword2,UserLevel=0,UserAddress="HCM",UserFirstName="Le",UserEmail="phucduye56@gmail.com",UserCash=1000,UserCity="HCM",UserPassword="0919061624",ConfirmPassword="0919061624",OldPassword="0919061624",UserDOB="9/12/1996",UserLastName="Truc"},
                new User{UserName="phucduye57",Password=userpassword2,UserLevel=0,UserAddress="HCM",UserFirstName="Le",UserEmail="phucduye57@gmail.com",UserCash=1000,UserCity="HCM",UserPassword="0919061624",ConfirmPassword="0919061624",OldPassword="0919061624",UserDOB="9/12/1996",UserLastName="Truc"}
            };
            Users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
            var Products = new List<Product>
            {
                new Product{ProductName="Quạt máy xịn pro",ProductDateSold="2017/07/01 03:00",ProductPrice=200000,ProductPicName="quat1.png",ProductPicExtension=".png",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=5,CateparentID=2,UserID=1},
                new Product{ProductName="Máy ảnh xịn",ProductDateSold="2017/07/01 03:00",ProductPrice=200000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=3,CateID=20,CateparentID=5,UserID=2},
                new Product{ProductName="Máy ảnh rất xịn",ProductDateSold="2017/07/02 03:00",ProductPrice=200000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=1},
                new Product{ProductName="Máy ảnh xịn cấp 2",ProductDateSold="2017/07/03 03:00",ProductPrice=400000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=1},
                new Product{ProductName="Máy ảnh xịn cấp 3",ProductDateSold="2017/07/04 03:00",ProductPrice=300000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=1},
                new Product{ProductName="Máy ảnh xịn cấp 4",ProductDateSold="2017/07/04 03:00",ProductPrice=200000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=1},
                new Product{ProductName="Máy ảnh xịn cấp 5",ProductDateSold="2017/07/05 03:00",ProductPrice=500000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=2},
                new Product{ProductName="Máy ảnh xịn cấp 6",ProductDateSold="2017/07/04 03:00",ProductPrice=300000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=1},
                new Product{ProductName="Máy ảnh xịn cấp 7",ProductDateSold="2017/07/04 03:00",ProductPrice=200000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=2},
                new Product{ProductName="Máy ảnh xịn cấp 8",ProductDateSold="2017/07/05 03:00",ProductPrice=500000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=2},
                new Product{ProductName="Máy ảnh dỏm cấp 1",ProductDateSold="2017/07/04 03:00",ProductPrice=300000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=1},
                new Product{ProductName="Máy ảnh dỏm cấp 2",ProductDateSold="2017/07/04 03:00",ProductPrice=200000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=2},
                new Product{ProductName="Máy ảnh dỏm cấp 3",ProductDateSold="2017/07/05 03:00",ProductPrice=500000,ProductPicName="mayanh.jpg",ProductPicExtension=".jpg",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=20,CateparentID=5,UserID=2},
                new Product{ProductName="Quạt máy xịn cấp 1",ProductDateSold="2017/07/01 03:00",ProductPrice=500000,ProductPicName="quat1.png",ProductPicExtension=".png",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=5,CateparentID=2,UserID=1},
                new Product{ProductName="Quạt máy xịn cấp 2",ProductDateSold="2017/07/01 03:00",ProductPrice=300000,ProductPicName="quat1.png",ProductPicExtension=".png",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=5,CateparentID=2,UserID=3},
                new Product{ProductName="Quạt máy xịn cấp 3",ProductDateSold="2017/07/02 03:00",ProductPrice=200000,ProductPicName="quat1.png",ProductPicExtension=".png",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=5,CateparentID=2,UserID=3},
                new Product{ProductName="Quạt máy dỏm cấp 1",ProductDateSold="2017/07/03 03:00",ProductPrice=100000,ProductPicName="quat1.png",ProductPicExtension=".png",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=5,CateparentID=2,UserID=3},
                new Product{ProductName="Quạt máy dỏm cấp 2",ProductDateSold="2017/07/01 03:00",ProductPrice=200000,ProductPicName="quat1.png",ProductPicExtension=".png",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=5,CateparentID=2,UserID=2},
                new Product{ProductName="Quạt máy dỏm cấp 3",ProductDateSold="2017/07/02 03:00",ProductPrice=300000,ProductPicName="quat1.png",ProductPicExtension=".png",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=5,CateparentID=2,UserID=2},
                new Product{ProductName="Quạt máy dỏm cấp 4",ProductDateSold="2017/07/03 03:00",ProductPrice=250000,ProductPicName="quat1.png",ProductPicExtension=".png",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=5,CateparentID=2,UserID=1},
                new Product{ProductName="Quạt máy dỏm cấp 5",ProductDateSold="2017/07/01 03:00",ProductPrice=220000,ProductPicName="quat1.png",ProductPicExtension=".png",ProductDes="Đẹp",ProductPointRequired=100,ProductTickSize=10000,ProductStatus=1,CateID=5,CateparentID=2,UserID=1}
            };
            Products.ForEach(s => context.Products.Add(s));
            context.SaveChanges();
            var FileDetails = new List<FileDetail>
            {
                new FileDetail{FileName="quat2.png",Extension=".png",ProductID=1},
                new FileDetail{FileName="quat3.png",Extension=".png",ProductID=1},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=2},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=2},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=2},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=3},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=3},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=3},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=4},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=4},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=4},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=5},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=5},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=5},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=6},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=6},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=6},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=7},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=7},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=7},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=8},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=8},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=8},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=9},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=9},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=9},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=10},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=10},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=10},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=11},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=11},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=11},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=12},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=12},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=12},
                new FileDetail{FileName="mayanh3.jpg",Extension=".jpg",ProductID=13},
                new FileDetail{FileName="mayanh4.jpg",Extension=".jpg",ProductID=13},
                new FileDetail{FileName="mayanh5.jpg",Extension=".jpg",ProductID=13},
                new FileDetail{FileName="quat2.png",Extension=".png",ProductID=14},
                new FileDetail{FileName="quat3.png",Extension=".png",ProductID=14},
                new FileDetail{FileName="quat2.png",Extension=".png",ProductID=15},
                new FileDetail{FileName="quat3.png",Extension=".png",ProductID=15},
                new FileDetail{FileName="quat2.png",Extension=".png",ProductID=16},
                new FileDetail{FileName="quat3.png",Extension=".png",ProductID=16},
                new FileDetail{FileName="quat2.png",Extension=".png",ProductID=17},
                new FileDetail{FileName="quat3.png",Extension=".png",ProductID=17},
                new FileDetail{FileName="quat2.png",Extension=".png",ProductID=18},
                new FileDetail{FileName="quat3.png",Extension=".png",ProductID=18},
                new FileDetail{FileName="quat2.png",Extension=".png",ProductID=19},
                new FileDetail{FileName="quat3.png",Extension=".png",ProductID=19},
                new FileDetail{FileName="quat2.png",Extension=".png",ProductID=20},
                new FileDetail{FileName="quat3.png",Extension=".png",ProductID=20},
                new FileDetail{FileName="quat2.png",Extension=".png",ProductID=21},
                new FileDetail{FileName="quat3.png",Extension=".png",ProductID=21},

            };
            FileDetails.ForEach(s => context.FileDetails.Add(s));
            context.SaveChanges();
        }

        
        
    }
}