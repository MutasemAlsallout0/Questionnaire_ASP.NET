using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Questionnaire.Models;

namespace Questionnaire.Data
{
    public class QuestionDbContext : IdentityDbContext
    {
        public QuestionDbContext()
        {

        }

        public QuestionDbContext(DbContextOptions<QuestionDbContext> options) : base(options)
        {

        }

        public DbSet<Questionnaire.Models.QuestionsView> QuestionsView { get; set; }
        public DbSet<Questionnaire.Models.Roles> Roless { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer(@"Data Source=MUTASEM\SQLEXPRESS;Initial Catalog=Qussss;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;");
            }
        }


        public DbSet<Questionnaire.Models.Users> Users { get; set; }
    }






}
