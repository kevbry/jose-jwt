using System.Collections.Generic;
using Jose;
using UnitTestProject1.jwk.util;

namespace UnitTestProject1.jwk
{
    public class JwkOct : IJwkAlgorithm
    {
        // kty=oct
        // k

        public JWK Parse(IDictionary<string, object> header, JwtSettings settings = null)
        {
            return new JWK()
            {
                Header = header,
                Key = header.GetBytes("k")
            };
        }

        public IDictionary<string, object> Serialize(JWK jwk, bool includePrivateParameters, JwtSettings settings = null)
        {
            Dictionary<string, object> header = new Dictionary<string, object>(jwk.Header);
            header.Set("kty", "oct");
            if (includePrivateParameters)
            {
                header.Set("k", jwk.Key as byte[]);
            }
            return header;
        }
    }
}
