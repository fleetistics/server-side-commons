namespace exs.modelCommons.UserManagement
{ 

    public class UserSessionSource
    {
        public const short AUTO_DeviceUID = 1;
        public const short Login_Portal = 2;
        public const short Login_App = 3;

        public short Id { get; set; }
        public string Name { get; set; } = default!;

        public DateTime LatestUpdate { get; set; }
    }
}
