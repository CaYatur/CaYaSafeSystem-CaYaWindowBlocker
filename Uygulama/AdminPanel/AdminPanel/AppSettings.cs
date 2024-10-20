namespace AdminPanel
{
    /// <summary>
    /// Uygulama genelindeki yapılandırma sabitleri.
    /// Kendi sunucu adresinize göre güncelleyin.
    /// </summary>
    internal static class AppSettings
    {
        /// <summary>
        /// TwoFA WebSocket sunucusunun adresi.
        /// Örnek: ws://192.168.1.100:303  veya  ws://your-domain.com:303
        /// </summary>
        public const string TwoFactorAuthServerUrl = "ws://YOUR_SERVER_ADDRESS:303";
    }
}
