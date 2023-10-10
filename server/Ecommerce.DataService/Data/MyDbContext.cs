using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DataService.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    { }

}