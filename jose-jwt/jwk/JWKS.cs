using Jose;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using static Jose.jwk.JwkFactory;
using Jose.jwk.util;

namespace Jose.jwk
{
    public class JWKS
    {
        public static IEnumerable<JWK> Parse(string json, JwtSettings settings = null)
        {
            settings = Factory.GetSettings(settings);
            Dictionary<string, object> jwks = settings.JsonMapper
                .Parse<Dictionary<string, object>>(json);
            if (jwks.TryGet<IList>("keys", out IList keys))
            {
                var t =
                    from header in keys.OfType<IDictionary<string, object>>()
                    select Factory
                        .JwkAlgorithmFromHeader(header.GetString("kty"), settings)
                        .Parse(header, settings);
                foreach(var i in t)
                {
                    yield return i;
                }
            }
            else
            {
                yield return Factory
                    .JwkAlgorithmFromHeader(jwks.GetString("kty"), settings)
                    .Parse(jwks, settings);
            }
        }
        public static string Serialize(IEnumerable<JWK> jwks, bool includePrivateParameters = false, JwtSettings settings = null)
        {
            settings = Factory.GetSettings(settings);
            var keys =
                from jwk in jwks
                select Factory
                    .JwkAlgorithmFromKey(jwk.Key, settings)
                    .Serialize(jwk, includePrivateParameters, settings);
            IDictionary <string, object> t = new Dictionary<string, object>()
            {
                { "keys", keys },
            };
            return settings.JsonMapper.Serialize(t);
        }
    }
}
