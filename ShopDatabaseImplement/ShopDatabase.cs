using Microsoft.EntityFrameworkCore;
using ShopDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDatabaseImplement
{
    public class ShopDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-CM2EFFR\SQLEXPRESS;Initial Catalog=ShopDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<PickupPoint> PickupPoints { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
    }
}
