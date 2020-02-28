using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp1
{
    public class efcore : DbContext
    {
        public efcore(DbContextOptions<efcore> options)
                  : base(options)
        { }

        public DbSet<Policy_Master> Policy_Master { get; set; }
    }
    //public class BloggingContextFactory : IDesignTimeDbContextFactory<efcore>
    //{
    //    public efcore CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<efcore>();
    //        optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("SqlConnectionString"));

    //        return new efcore(optionsBuilder.Options);
    //    }
    //}
}
