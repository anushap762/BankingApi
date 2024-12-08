using BankingApi.Repository.Concrete;
using BankingApi.Repository;
using BankingApi.Services.Concrete;
using BankingApi.Services.Interfaces;
using BankingApi.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
namespace BankingApi
{
    public class Program
    {
        private readonly IConfiguration _configuration;

        public Program(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BankingDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IAccountRepository, AccountRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
            builder.Services.AddScoped<IAccountService,AccountService>();
            builder.Services.AddScoped<ITransactionService,TransactionService>();
            builder.Services.AddScoped<IInterestRuleRepository, InterestRuleRepository>();
            builder.Services.AddScoped<IInterestRuleService,InterestRuleService>();
            builder.Services.AddScoped<IStatementService, StatementService>();
            builder.Services.AddScoped<IStatementRepository, StatementRepository>();
            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularApp", policy =>
                {
                    policy.AllowAnyOrigin() 
                          .AllowAnyMethod() 
                          .AllowAnyHeader();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(options =>
                //{
                //    options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //    options.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
                //});
            }

            app.UseHttpsRedirection();

            // Ensure routing is configured
            app.UseRouting();

            // Configure CORS policy (e.g., AllowAngularApp should be defined in services)
            app.UseCors("AllowAngularApp");

            app.UseAuthorization();

            // Map controllers after setting up middleware
            app.MapControllers();

            app.Run();

        }
    }
}
