namespace SiteEngelleyiciArayüz
{
    /// <summary>
    /// Uygulama genelindeki yapılandırma sabitleri.
    /// Kendi sunucu ve veritabanı bilgilerinize göre güncelleyin.
    /// </summary>
    internal static class AppSettings
    {
        /// <summary>
        /// MongoDB Atlas bağlantı dizesi.
        /// Örnek: mongodb+srv://KULLANICI:SIFRE@cluster.mongodb.net/?retryWrites=true&w=majority
        /// </summary>
        public const string MongoDbConnectionString =
            "mongodb+srv://KULLANICI_ADINIZ:SIFRENIZ@cluster.mongodb.net/?retryWrites=true&w=majority";

        /// <summary>
        /// TwoFA WebSocket sunucusunun adresi.
        /// Örnek: ws://192.168.1.100:303  veya  ws://your-domain.com:303
        /// </summary>
        public const string TwoFactorAuthServerUrl = "ws://YOUR_SERVER_ADDRESS:303";
    }
}
