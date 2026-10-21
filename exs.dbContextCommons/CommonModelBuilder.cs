using exs.modelCommons.AppStructure;
using exs.modelCommons.UserManagement;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace exs.dbContextCommons
{
	public static class CommonModelBuilder
	{
		public static void CreateModel(ModelBuilder modelBuilder)
		{
			if (!_built.TryAdd(modelBuilder, null)) return;

			modelBuilder.Entity<ClientDevicePlatform>(entity =>
			{
				entity.ToTable("client_device_platform");
				entity.HasKey(e => new { e.Id });
			});
			modelBuilder.Entity<UserSession>(entity =>
			{
				entity.ToTable("user_session");
				entity.HasKey(e => new { e.Id });
				entity.Property(e => e.Id).ValueGeneratedOnAdd();
				entity.ComplexProperty(e => e.Token);
				entity.ComplexProperty(e => e.ClientInfo);
			});
		}

		// Per-ModelBuilder rather than a process-wide flag: EF builds the model more than once
		// (e.g. Migrate() builds it twice to detect a non-deterministic model), and a static flag
		// made every build after the first come out without these entities.
		private static readonly ConditionalWeakTable<ModelBuilder, object?> _built = new();
	}
}
