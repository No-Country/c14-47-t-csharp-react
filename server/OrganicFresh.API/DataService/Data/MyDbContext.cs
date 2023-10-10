using Microsoft.EntityFrameworkCore;


namespace OrganicFresh.API.DataService.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    { }

}