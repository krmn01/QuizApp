using Microsoft.EntityFrameworkCore;
using QuizApp.Data;
using QuizApp.Services;
using QuizApp.Services.Interfaces;

namespace QuizApp
{   public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<IQuestionService, QuestionService>();

            services.AddDbContext<AppDb>(builder =>
            {
                builder.UseSqlServer(@"Data source=(localdb)\MSSQLLocalDB;Initial Catalog=QuizDb;Integrated Security=True");
            });
           
            services.AddRazorPages();
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();
            app.Run();
        }
    }
}
