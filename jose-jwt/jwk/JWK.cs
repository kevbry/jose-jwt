using System.Collections.Generic;
using Jose.jwk.util;

namespace Jose.jwk
{
    public class JWK
    {
        public static JWK Parse(string json, JwtSettings settings = null)
        {
            settings = settings ?? JWT.DefaultSettings;
            IDictionary<string, object> header = settings.JsonMapper
                .Parse<Dictionary<string, object>>(json);
            return settings
                .JwkAlgorithmFromHeader(header.GetString("kty"))
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
            settings = settings ?? JWT.DefaultSettings;
            IDictionary<string,object> header = settings
                .JwkAlgorithmFromKey(Key)
                .Serialize(this, includePrivateParameters, settings);
            return settings.JsonMapper.Serialize(header);
        }
    }
}
