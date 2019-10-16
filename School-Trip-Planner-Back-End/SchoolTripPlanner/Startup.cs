using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolTripPlanner.Data.Contracts;
using SchoolTripPlanner.Data.Persistence;
using SchoolTripPlanner.Data.Repositories;
using SchoolTripPlanner.Domain.DTOs.AutoMapperProfiles;

namespace SchoolTripPlanner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<Data.Persistence.SchoolTripPlannerContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("SchoolTripPlannerConnection"))
            );

            services.AddAutoMapper(typeof(GeneralProfile));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISchoolTripPlannerContext, SchoolTripPlannerContext>();
            services.AddTransient<ITeacherRepository, TeacherRepository>();
            services.AddTransient<ISchoolTripRepository, SchoolTripRepository>();
            services.AddTransient<IClassRepository, ClassRepository>();
            services.AddTransient<IScanRepository, ScanRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}