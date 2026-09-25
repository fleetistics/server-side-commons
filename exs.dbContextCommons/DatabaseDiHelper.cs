using exs.Database.Commons.Impl;
using exs.Database.Commons.Interfaces;
using exs.databaseCommons.Impl;
using exs.databaseCommons.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace exs.dbContextCommons
{
	public static class DatabaseDiHelper
	{
		public static IServiceCollection AddDbServices<C>(this IServiceCollection services, IConfiguration configuration) where C : DbContext
		{
			services.AddSingleton<IRepositoryFactory, EntityFrameworkRepositoryFactory>()
				.AddScoped<IRepository, EntityFrameworkRepository<C>>()
				.AddTransient<IScopedRepository, ScopedEntityFrameworkRepository<C>>(sp =>
					new ScopedEntityFrameworkRepository<C>(sp.GetRequiredService<C>(), sp.CreateScope()))
				.AddDbContext<C>(options =>
					options.UseNpgsql(configuration["ConnectionStrings:MainDatabase"], opts =>
					{
						opts.UseNetTopologySuite()
							.EnableRetryOnFailure(
								maxRetryCount: 5,
								maxRetryDelay: TimeSpan.FromSeconds(10),
								errorCodesToAdd: null)
							.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);

					})
					.ReplaceService<IMigrationsSqlGenerator, LatestUpdateTriggerSqlGenerator>()
					.ReplaceService<IMigrationsModelDiffer, NoDropMigrationsModelDiffer>(),
					ServiceLifetime.Transient);
			return services;
		}
	}
}
