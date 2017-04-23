using auctionwebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
        }
    }
}