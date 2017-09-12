using Jose;
using System;
using System.Security.Cryptography;

namespace UnitTestProject1.jwk
{
    public class JwkFactory
    {
        private static readonly JwkFactory _instance = new JwkFactory();

        public static JwkFactory Factory { get => _instance; }

        public virtual JwtSettings GetSettings(JwtSettings settings)
        {
            return settings ?? JWT.DefaultSettings;
        }

        public virtual IJwkAlgorithm JwkAlgorithmFromHeader(string kty, JwtSettings settings = null)
        {
            switch (kty)
            {
                case "oct": return new JwkOct();
                case "RSA": return new JwkRsa();
                case "EC": return new JwkEc();
                default: throw new ArgumentOutOfRangeException("kty", kty, "Invalid");
            }
        }

        public virtual IJwkAlgorithm JwkAlgorithmFromKey(object key, JwtSettings settings = null)
        {
            if (key is byte[])
            {
                return new JwkOct();
            }
            if (key is RSA)
            {
                return new JwkRsa();
            }
            if (key is ECDsa)
            {
                return new JwkEc();
            }
            throw new ArgumentOutOfRangeException("key", key, "Invalid");
        }
    }
}
