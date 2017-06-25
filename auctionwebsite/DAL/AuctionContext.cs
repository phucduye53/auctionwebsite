using auctionwebsite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace auctionwebsite.DAL
{
    public class AuctionContext : DbContext
    {
        public AuctionContext(): base("AuctionContext")
        {

        }
        public DbSet<Cate> Cates { get; set; }
        public DbSet<Cateparent> Cateparents { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<FileDetail> FileDetails { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Bidding> Biddings { get; set; }
        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Configurations.Add(new Rate.RateMapping());
        }
    }
}