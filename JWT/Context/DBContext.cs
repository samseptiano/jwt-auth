using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JWT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace JWT.Context
{
    public class DBContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            optionsBuilder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            
        }

        //inisialisasi entity dari database
        public DbSet<User> User { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventPeserta> EventPeserta { get; set; }
        public DbSet<EventPesertaAbsen> EventPesertaAbsen { get; set; }
        public DbSet<EventSession> EventSession { get; set; }
        //public DbSet<EventSessionInstructor> EventSessionInstructor { get; set; }
        //public DbSet<Instructor> Instructor { get; set; }
        public DbSet<Survey> Survey { get; set; }
        public DbSet<SurveyAnswer> SurveyAnswer { get; set; }
        public DbSet<SurveyQuestion> SurveyQuestion { get; set; }
        public DbSet<QuestionType> QuestionType { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswer { get; set; }
        public DbSet<SurveyAnswerPeserta> SurveyAnswerPeserta { get; set; }
        public DbSet<SurveyPeserta> SurveyPeserta { get; set; }
        //public DbSet<NewsTable> newsTable { get; set; } EventPesertaAbsen  SurveyPeserta


    }
}
