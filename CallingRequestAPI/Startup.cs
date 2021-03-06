﻿using CallingRequestAPI.Businness;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CallingRequestAPI
{
    public class Startup
    {
        /// <summary>
        /// Configures services.
        /// This method gets called by the runtime which uses this method to add services to the container.
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new CallingRequest());
            services.AddSingleton(new CallProcess());

            // adding MVC Core,authorization and JSON formatting
            services.AddMvcCore()
                    .AddAuthorization()
                    .AddJsonFormatters();

            // adding authentication info
            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = "http://10.27.249.82:59447";
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "CallingRequestAPI";
                    });
        }

        /// <summary>
        /// Configures app and environment.
        /// This method gets called by the runtime which uses this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">App</param>
        /// <param name="env">Environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

