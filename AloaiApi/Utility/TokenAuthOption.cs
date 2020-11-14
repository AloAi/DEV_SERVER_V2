using Microsoft.IdentityModel.Tokens;
using System;

namespace Aloai.Auth
{
    /// <summary>
    /// トークン関係を格納
    /// </summary>
    public class TokenAuthOption
    {
        public static string Audience { get; } = "http://localhost:4200/";
        public static string Issuer { get; } = "http://localhost:52245/";
        /// <summary>
        /// RSAキー
        /// </summary>
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(180);
        public static string TokenType { get; } = "Bearer";
    }
}