using exs.modelCommons.AppStructure;

namespace exs.modelCommons.UserManagement
{
	public class UserSession
	{
		public int Id { get; set; }

		// Links to the account and (optionally) the physical device the session is tied to.
		public int UserId { get; set; }
		public int? MobileGpsDeviceId { get; set; }

		public SessionTokenInfo Token { get; set; } = new();
		public SessionClientInfo ClientInfo { get; set; } = new();

		public short StatusId { get; set; }
		public short SessionSourceId { get; set; }
		public DateTime LatestUpdate { get; set; }
	}
}
