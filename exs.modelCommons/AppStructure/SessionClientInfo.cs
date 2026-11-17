namespace exs.modelCommons.AppStructure
{
    /// <summary>Device/app details reported by the client on login and every refresh.</summary>
    public class SessionClientInfo
    {
        public short ClientApplicationId { get; set; }
        public string AppVersion { get; set; } = string.Empty;
        public string AppHash { get; set; } = string.Empty;
        public string PlatformDescription { get; set; } = string.Empty;
        public short PlatformId { get; set; } = ClientDevicePlatform.Unknown;
        public string FCM_FID { get; set; } = string.Empty;
    }
}
