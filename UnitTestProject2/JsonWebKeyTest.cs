using Jose;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Jose.jwk;
using Xunit;

namespace UnitTestProject2
{
    public class JsonWebKeyTest
    {
        public static JwtSettings Settings { get => JWT.DefaultSettings; }
        public static RSA CreateRSA(int keySize = 2048)
        {
#if !NOT
            CspParameters csp = new CspParameters()
            {
                Flags = CspProviderFlags.CreateEphemeralKey
            };
            var rsa = new RSACryptoServiceProvider(keySize, csp)
            {
                PersistKeyInCsp = false
            };
#else
            var rsa = new RSACng(keySize);
#endif
            return rsa;
        }
        public static ECDsa CreateEC(ECCurve curve)
        {
            var ec = ECDsa.Create();
            ec.GenerateKey(curve);
            return ec;
        }
        [Fact]
        public void ExportRSA()
        {
            /*
            CspParameters csp = new CspParameters();
            csp.Flags = CspProviderFlags.CreateEphemeralKey;
            RSA rsa = new RSACryptoServiceProvider(csp);
            */
            using (RSA rsa = RSA.Create())
            {
                rsa.KeySize = 2048;
                RSAParameters parameters = rsa.ExportParameters(true);
                Assert.NotNull(parameters);
            }
        }
        [Fact]
        public void ExportEC()
        {
            using (ECDsa ec = ECDsa.Create())
            {
                ec.GenerateKey(ECCurve.NamedCurves.nistP256);
                ECParameters parameters = ec.ExportParameters(true);
                Assert.NotNull(parameters);
            }
        }
        [Fact]
        public void ParseJWKS()
        {
            // {"keys":[...]}
            const string KEYS = @"{""keys"":[{""kty"":""RSA""}]}";

            Dictionary<string, object> obj = Settings.JsonMapper.Parse<Dictionary<string, object>>(KEYS);
            Assert.Equal(1, obj.Count);
            Assert.IsAssignableFrom<IList>(obj["keys"]);

            IList list = (IList)obj["keys"];
            Assert.Equal(1, list.Count);
            Assert.IsAssignableFrom<IDictionary<string, object>>(list[0]);

            IDictionary<string, object> rsa = (IDictionary<string, object>)list[0];
            Assert.Equal(1, rsa.Count);
            Assert.IsType<string>(rsa["kty"]);
        }
        [Fact]
        public void ParseJWK()
        {
            // {"kty":"RSA",...}
            const string RSA = @"{""kty"":""RSA""}";

            Dictionary<string, object> obj = Settings.JsonMapper.Parse<Dictionary<string, object>>(RSA);
            Assert.Equal(1, obj.Count);
            Assert.IsType<string>(obj["kty"]);
        }
        [Fact]
        public void JwkRsa1()
        {
            JWK jwk1 = new JWK()
            {
                Key = CreateRSA()
            };
            string json = jwk1.Serialize(true);
            JWK jwk2 = JWK.Parse(json);
            string jws1 = JWT.Encode("payload", jwk1.Key, JwsAlgorithm.RS256);
            string payload = JWT.Decode(jws1, jwk2.Key);
            Assert.Equal("payload", payload);
            string jws2 = JWT.Encode("payload", jwk2.Key, JwsAlgorithm.RS256);
            payload = JWT.Decode(jws2, jwk1.Key);
            Assert.Equal("payload", payload);
        }
        [Fact]
        public void JwkEc1()
        {
            JWK jwk1 = new JWK()
            {
                Key = CreateEC(ECCurve.NamedCurves.nistP256)
            };
            string json = jwk1.Serialize(true);
            JWK jwk2 = JWK.Parse(json);
            string jws1 = JWT.Encode("payload", jwk1.Key, JwsAlgorithm.ES256);
            string payload = JWT.Decode(jws1, jwk2.Key);
            Assert.Equal("payload", payload);
            string jws2 = JWT.Encode("payload", jwk2.Key, JwsAlgorithm.ES256);
            payload = JWT.Decode(jws2, jwk1.Key);
            Assert.Equal("payload", payload);
        }
        [Fact]
        public void Jwks1()
        {
            JWK jwk1 = new JWK()
            {
                Key = new byte[20]
            };
            JWK jwk2 = new JWK()
            {
                Key = new byte[20]
            };
            string jwks = JWKS.Serialize(new JWK[] { jwk1,jwk2 }, true);
            Console.WriteLine(jwks);
            IList<JWK> list = JWKS.Parse(jwks).ToList();
            Assert.Equal(2, list.Count);
        }
    }
}
