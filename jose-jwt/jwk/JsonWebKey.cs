using System;
using System.Collections.Generic;
using System.Text;
using Jose.jwk.util;

namespace Jose.jwk
{
    public class JsonWebKey
    {
        public static JsonWebKey Parse(string json, JwtSettings settings = null)
        {
            settings = settings ?? JWT.DefaultSettings;
            IDictionary<string, object> header = settings.JsonMapper
                .Parse<IDictionary<string, object>>(json);
            return new JsonWebKey(header);
        }
        public JsonWebKey(IDictionary<string,object> header)
        {
            Header = header;
        }
        public IDictionary<string, object> Header { get; private set; }
        public string KeyType
        {
            get => Header.GetString("kty");
            //set => Header.Set("kty", value);
        }
        public string Use
        {
            get => Header.GetString("use");
            //set => Header.Set("use", value);
        }
        public bool IsSignature { get => string.Equals("sig", Use ?? "sig"); }
        public bool IsEncryption { get => string.Equals("enc", Use ?? "enc"); }
        public string Id
        {
            get => Header.GetString("kid");
            //set => Header.Set("kid", value);
        }
        public bool TryGetSignatureKey<T>(out T key)
        {
            key = default(T);
            return false;
        }
        public bool TryGetEncryptionKey<T>(out T key)
        {
            key = default(T);
            return false;
        }
    }
}
