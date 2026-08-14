using AvemCMS.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AvemCMS.Api.Data
{
    public class AvemDbContext : DbContext
    {
        public AvemDbContext(DbContextOptions<AvemDbContext> options) : base(options) { }

        // Khai báo bảng Articles
        public DbSet<Article> Articles { get; set; }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<CaseStudy> CaseStudies { get; set; }
    }
}