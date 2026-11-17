using exs.modelCommons.AppStructure;

namespace exs.modelCommons.UserManagement
{
	public class UserSession
	{
		public int Id { get; set; }

		// Links to the account and (optionally) the physical device the session is tied to.
		public int UserId { get; set; }
		public int? CompanyId { get; set; }
        public int? MobileGpsDeviceId { get; set; }

        // Random, unguessable handle of the session (256 bits, base64url), generated at login. It is the first part of
        // the refresh key "{SessionKey}.{counter}.{mac}" and what the session is looked up by, so the integer Id never
        // travels in that key. A secret: never log it. Null for sessions that never use a refresh key (auto-device)
        // and for rows created before it existed. Unique index (nulls allowed).
        public string SessionKey { get; set; }

		public SessionTokenInfo Token { get; set; } = new();
		public SessionClientInfo ClientInfo { get; set; } = new();

		public short StatusId { get; set; }
		public short SessionSourceId { get; set; }
		public DateTime LatestUpdate { get; set; }
	}
}
