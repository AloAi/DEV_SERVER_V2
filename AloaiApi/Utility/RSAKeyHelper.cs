using System.Security.Cryptography;

namespace Aloai.Auth
{
    /// <summary>
    /// RSAの暗号と複合を行うクラス
    /// </summary>
    public class RSAKeyHelper
    {
        public static RSAParameters GenerateKey()
        {
            using (var key = new RSACryptoServiceProvider(2048))
            {
                return key.ExportParameters(true);
            }
        }
    }
}