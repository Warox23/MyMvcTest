using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcApplication16.Models
{
    public class QuestionContext : DbContext
    {
        public QuestionContext() : base("Questions") { }

        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {

            /*modelBuilder.Entity<ResoultSaveModel>().HasKey(i => i.Id);
            modelBuilder.Entity<ResoultSaveModel>().Property(i => i.Mark);
            modelBuilder.Entity<ResoultSaveModel>().Property(i => i.UserName);
            modelBuilder.Entity<ResoultSaveModel>().Property(i => i.Started);
            modelBuilder.Entity<ResoultSaveModel>().Property(i => i.Finished);*/
          

            modelBuilder.Entity<TestModel>().HasKey(i => i.Id);
            modelBuilder.Entity<TestModel>().Property(i => i.title);
            modelBuilder.Entity<TestModel>().Property(i => i.Description);

            modelBuilder.Entity<TestModel>().HasMany(i => i.Questions)
                .WithRequired()
                .HasForeignKey(si => si.TestId);

            modelBuilder.Entity<Question>().HasKey(i => i.QuestionId);
            modelBuilder.Entity<Question>().Property(i => i.Text);

            modelBuilder.Entity<Question>().HasMany(i => i.Answers)
                .WithRequired()
                .HasForeignKey(si => si.QuestionId);

            modelBuilder.Entity<AnswerVariant>().HasKey(i => i.AnswerId);
            modelBuilder.Entity<AnswerVariant>().Property(i => i.Text);
           
        }

        public DbSet<TestModel> Tests{ get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerVariant> AnswerVariants { get; set; }
      //  public DbSet<ResoultSaveModel> SaveModel { get; set; }

    }
}