namespace exs.modelCommons.AppStructure
{
    /// <summary>Device/app details reported by the client on login and every refresh.</summary>
    public class SessionClientInfo
    {
        public string AppUID { get; set; } = string.Empty;
        public string AppVersion { get; set; } = string.Empty;
        public string DeviceUID { get; set; } = string.Empty;
        public string CodeVersion { get; set; } = string.Empty;
        public short PlatformId { get; set; } = ClientDevicePlatform.Unknown;
        public string FCM_FID { get; set; } = string.Empty;
    }
}
