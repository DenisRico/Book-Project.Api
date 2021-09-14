using Microsoft.Extensions.DependencyInjection;
using MyProject.BL.Services;
using MyProject.BL.TokenGenerators;
using MyProject.BL.TokenValidators;
using MyProject.Common.Models;
using MyProject.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.BL.Configuration
{
    public static class ModuleInitializer
    {
        public static IServiceCollection ConfigureBL(this IServiceCollection services)
        {
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPasswordHasherService, PasswordHasherService>();
            services.AddTransient<IAuthentificator, Authentificator>();
            services.AddSingleton<IRefreshTokenService, RefreshTokenService>();
            services.AddTransient<IAccesTokenGenerator, AccesTokenGenerator>();
            services.AddTransient<IRefreshTokenGenerator, RefreshTokenGenerator>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<RefreshTokenValidator>();


            return services;
        }
    }
}
