using Jose;
using System.Collections.Generic;

namespace Jose.jwk
{
    public interface IJwkAlgorithm
    {
        JWK Parse(IDictionary<string, object> header, JwtSettings settings = null);
        IDictionary<string, object> Serialize(JWK jwk, bool includePrivateParameters, JwtSettings settings = null);
    }
}
