using Jose;
using System.Collections.Generic;
using static Jose.jwk.JwkFactory;
using Jose.jwk.util;

namespace Jose.jwk
{
    public class JWK
    {
        public static JWK Parse(string json, JwtSettings settings = null)
        {
            settings = Factory.GetSettings(settings);
            IDictionary<string, object> header = settings.JsonMapper
                .Parse<Dictionary<string, object>>(json);
            return Factory
                .JwkAlgorithmFromHeader(header.GetString("kty"), settings)
                .Parse(header, settings);
        }
        public JWK()
        {
            Header = new Dictionary<string,object>();
        }
        public IDictionary<string,object> Header { get; internal set; }
        public string KeyType { get => Header.GetString("kty"); }
        public string Use { get => Header.GetString("use"); }
        public bool IsSignature { get => string.Equals("sig", Use ?? "sig"); }
        public bool IsEncryption { get => string.Equals("enc", Use ?? "enc"); }
        public string Id { get => Header.GetString("kid"); }
        public object Key { get; set; }
        public string Serialize(bool includePrivateParameters = false, JwtSettings settings = null)
        {
            settings = Factory.GetSettings(settings);
            IDictionary<string,object> header = Factory
                .JwkAlgorithmFromKey(Key, settings)
                .Serialize(this, includePrivateParameters, settings);
            return settings.JsonMapper.Serialize(header);
        }
    }
}
