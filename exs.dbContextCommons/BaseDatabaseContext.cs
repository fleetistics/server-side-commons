using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace exs.dbContextCommons
{
	public class BaseDatabaseContext : DbContext
	{
		public BaseDatabaseContext(DbContextOptions options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			CommonModelBuilder.CreateModel(modelBuilder);
			createModel(modelBuilder);

			// Every optimistic-lock column: DB stamps it via DEFAULT on insert and the
			// update_latestupdate_column trigger (LatestUpdateTriggerSqlGenerator) on update, so EF
			// never writes a value itself and must re-read it after every write.
			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				var property = entityType.FindProperty("LatestUpdate");
				if (property != null && property.ClrType == typeof(DateTime))
				{
					property.SetDefaultValueSql("now()");
					property.ValueGenerated = ValueGenerated.OnAddOrUpdate;
					property.IsConcurrencyToken = true;
				}
			}
		}

		protected virtual void createModel(ModelBuilder modelBuilder)
		{
		}
	}
}
