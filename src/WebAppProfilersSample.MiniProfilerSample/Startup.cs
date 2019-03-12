using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using System.Text;

namespace WebAppProfilersSample.MiniProfilerSample
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddConfiguredMvc()
                .AddDb(_config);

            services
                .AddMiniProfiler(options =>
                {
                    options.RouteBasePath = "/profiler";
                })
                .AddEntityFramework();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MiniProfiler Test API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiniProfiler Test API");
                c.IndexStream = () =>
                {
                    //return File.OpenRead("index.html");
                    // haaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaack 😛
                    var mockContext = new DefaultHttpContext();
                    var miniProfiler = MiniProfiler.StartNew("dummy");
                    var miniProfilerScriptInclude =
                        miniProfiler.RenderIncludes(mockContext, RenderPosition.Left, true, true, 15, true, false).Value.Replace("<script", "<script data-authorized=\"true\"");

                    using (var reader = new StreamReader(File.OpenRead("index.html")))
                    {
                        var fileContent = reader.ReadToEnd();
                        var editedContent = fileContent.Replace("%replace_with_miniprofiler%", miniProfilerScriptInclude);
                        return new MemoryStream(Encoding.UTF8.GetBytes(editedContent));
                    }
                };
            });

            app.UseMiniProfiler();
            app.UseMvc();
        }
    }
}
