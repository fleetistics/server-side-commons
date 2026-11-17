using exs.modelCommons.AppStructure;
using exs.modelCommons.UserManagement;
using Microsoft.EntityFrameworkCore;

namespace exs.dbContextCommons
{
	public static class CommonModelBuilder
	{
		public static void CreateModel(ModelBuilder modelBuilder)
		{
			// Per model, not per process: see NotificationsModelBuilder.CreateNotificationModel.
			if (modelBuilder.Model.FindEntityType(typeof(UserSession)) != null) return;

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
				entity.HasIndex(e => e.SessionKey).IsUnique();
				entity.ComplexProperty(e => e.Token);
				entity.ComplexProperty(e => e.ClientInfo);
			});
		}
	}
}
