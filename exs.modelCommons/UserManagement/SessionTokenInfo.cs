using System;

namespace exs.modelCommons.UserManagement
{
    /// <summary>Refresh-token bookkeeping for a session: keys, lifetime, and rotation.</summary>
    public class SessionTokenInfo
    {
        // Per-session rotation state of the refresh cookie. The cookie value is
        // "{sessionId}.{RotationCounter}.{mac}", where the mac is derived on the server from a secret
        // it holds plus KeySalt, so nothing stored here can be replayed as a credential. The counter
        // goes up by one on every rotation (see AuthService). Auto-device sessions never use the
        // cookie: they only count rotations and leave KeySalt empty.
        public int RotationCounter { get; set; }

        // Random per-session salt (base64url), generated at login. Useless without the server secret.
        public string KeySalt { get; set; } = string.Empty;

        // True: fixed lifetime ending at ExpirationTime, persistent cookie (login with "remember me",
        // auto-device sessions). False: idle lifetime that every refresh pushes forward, capped at
        // CreationTime + max lifetime, session cookie. Defaults to true so existing rows keep behaving as before.
        public bool IsPersistent { get; set; } = true;

        public DateTime CreationTime { get; set; }
        public DateTime ExpirationTime { get; set; }
        public DateTime LatestRefreshTime { get; set; }
    }
}
