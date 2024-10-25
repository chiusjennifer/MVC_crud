using Microsoft.EntityFrameworkCore;
using MVC_crud.Models.Entities;
namespace MVC_crud.Data
{
    public class MVCDBContext : DbContext
    {
        public MVCDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet <Customer> Customers{ get;set; }
    }
}
