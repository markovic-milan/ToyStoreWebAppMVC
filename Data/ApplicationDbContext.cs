using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToyStoreWebAppMVC.Entities;

namespace ToyStoreWebAppMVC.Data
{

    public class ApplicationUser : IdentityUser
    {
        [StringLength(200)]
        public string FirstName { get; set; }
        [StringLength(200)]
        public string LastName { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        [StringLength(50)]
        public string PostCode { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<UserOrder> UserOrder { get; set; }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Toy> Toy { get; set; }
        public DbSet<UserOrder> UserOrder { get; set; }

    }
}
