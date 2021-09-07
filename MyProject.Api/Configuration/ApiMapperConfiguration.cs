using AutoMapper;
using MyProject.Common.Models;
using MyProject.Common.Models.ClientModel;
using MyProject.Common.Models.Domain;
using System;
using System.Linq;

namespace MyProject.Api.Configuration
{
	public static class ApiMapperConfiguration
	{
		public static void ConfigureMappings(IMapperConfigurationExpression config)
		{
			if (config == null)
			{
				throw new ArgumentNullException(nameof(config));
			}

			config.CreateMap<BookViewModel, Book>()
				.ForMember(item => item.Id, exp => exp.MapFrom(item => item.Id))
				.ForMember(item => item.Name, exp => exp.MapFrom(item => item.Name))
				.ForMember(item => item.Author, exp => exp.MapFrom(item => item.Author))
				.ForMember(item => item.Genres, exp => exp.Ignore());

			config.CreateMap<Book, BookViewModel>()
				.ForMember(item => item.Id, exp => exp.MapFrom(item => item.Id))
				.ForMember(item => item.Name, exp => exp.MapFrom(item => item.Name))
				.ForMember(item => item.Author, exp => exp.MapFrom(item => item.Author))
				.ForMember(item => item.GenresNames, exp => exp.MapFrom(item=>item.Genres.Select(item=>item.Name)))
				.ForMember(item => item.GenreIds, exp => exp.Ignore());

			config.CreateMap<Genre, GenreViewModel>()
				.ForMember(item => item.Id, exp => exp.MapFrom(item => item.Id))
				.ForMember(item => item.Name, exp => exp.MapFrom(item => item.Name))
				.ForMember(item => item.Books, exp => exp.MapFrom(item => item.Books));
				




			//config.CreateMap<PositionRequestModel, Position>()
			//.ForMember(item => item.Id, exp => exp.MapFrom(item => item.Coordinates[0]))
			//.ForMember(item => item.Name, exp => exp.MapFrom(item => item.Name))
			//.ForMember(item => item.Lat1, exp => exp.MapFrom(item => item.Coordinates[1]))
			//.ForMember(item => item.Lng1, exp => exp.MapFrom(item => item.Coordinates[2]))
			//.ForMember(item => item.Lat2, exp => exp.MapFrom(item => item.Coordinates[3]))
			//.ForMember(item => item.Lng2, exp => exp.MapFrom(item => item.Coordinates[4]))
			//.ForMember(item => item.Lat3, exp => exp.MapFrom(item => item.Coordinates[5]))
			//.ForMember(item => item.Lng3, exp => exp.MapFrom(item => item.Coordinates[6]))
			//.ForMember(item => item.Lat4, exp => exp.MapFrom(item => item.Coordinates[7] != 0 ? item.Coordinates[7] : 0))
			//.ForMember(item => item.Lng4, exp => exp.MapFrom(item => item.Coordinates[8] != 0 ? item.Coordinates[8] : 0))
			//.ForMember(item => item.Lat5, exp => exp.MapFrom(item => item.Coordinates[9] != 0 ? item.Coordinates[9] : 0))
			//.ForMember(item => item.Lng5, exp => exp.MapFrom(item => item.Coordinates[10] != 0 ? item.Coordinates[10] : 0))
			//.ForMember(item => item.Lat6, exp => exp.MapFrom(item => item.Coordinates[11] != 0 ? item.Coordinates[11] : 0))
			//.ForMember(item => item.Lng6, exp => exp.MapFrom(item => item.Coordinates[12] != 0 ? item.Coordinates[12] : 0));



		}
	}
}
