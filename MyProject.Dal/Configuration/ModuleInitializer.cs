using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyProject.Common.Repositories;
using MyProject.Dal.Repositories;
using MyProject.Dal.SqlContext;

namespace MyProject.Dal.Configuration
{
	public static class ModuleInitializer
	{
		public static IServiceCollection ConfigureDal(this IServiceCollection services, IConfiguration configuration)
		{
			//SetSettings(services, configuration);
			AddDependenciesToContainer(services);

			return services;
		}

		private static void SetSettings(IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<MyProjectContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
				//options.UseSqlite(configuration.GetConnectionString("BeatMeDatabase"));
			});
		}

		private static void AddDependenciesToContainer(IServiceCollection services)
		{
			services.AddTransient<IBookRepository, BookRepository>();
			services.AddTransient<IGenreRepository, GenreRepository>();
			services.AddTransient<IUserRepository, UserRepository>();

		}
	}
}
