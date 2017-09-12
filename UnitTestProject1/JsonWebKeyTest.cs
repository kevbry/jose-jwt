using Jose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnitTestProject1.jwk;

namespace UnitTestProject1
{
    [TestClass]
    public class JsonWebKeyTest
    {
        public static JwtSettings Settings { get { return JWT.DefaultSettings; } }
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
            var ec = new ECDsaCng();
            ec.GenerateKey(curve);
            return ec;
        }
        [TestMethod]
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
                Assert.IsNotNull(parameters);
            }
        }
        [TestMethod]
        public void ExportEC()
        {
            using (ECDsa ec = ECDsa.Create())
            {
                ec.GenerateKey(ECCurve.NamedCurves.nistP256);
                ECParameters parameters = ec.ExportParameters(true);
                Assert.IsNotNull(parameters);
            }
        }
        [TestMethod]
        public void ParseJWKS()
        {
            // {"keys":[...]}
            const string KEYS = @"{""keys"":[{""kty"":""RSA""}]}";

            Dictionary<string, object> obj = Settings.JsonMapper.Parse<Dictionary<string, object>>(KEYS);
            Assert.AreEqual(1, obj.Count);
            Assert.IsInstanceOfType(obj["keys"], typeof(IList));

            IList list = (IList)obj["keys"];
            Assert.AreEqual(1, list.Count);
            Assert.IsInstanceOfType(list[0], typeof(IDictionary<string, object>));

            IDictionary<string, object> rsa = (IDictionary<string, object>)list[0];
            Assert.AreEqual(1, rsa.Count);
            Assert.IsInstanceOfType(rsa["kty"], typeof(string));
        }
        [TestMethod]
        public void ParseJWK()
        {
            // {"kty":"RSA",...}
            const string RSA = @"{""kty"":""RSA""}";

            Dictionary<string, object> obj = Settings.JsonMapper.Parse<Dictionary<string, object>>(RSA);
            Assert.AreEqual(1, obj.Count);
            Assert.IsInstanceOfType(obj["kty"], typeof(string));
        }
        [TestMethod]
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
            Assert.AreEqual("payload", payload);
            string jws2 = JWT.Encode("payload", jwk2.Key, JwsAlgorithm.RS256);
            payload = JWT.Decode(jws2, jwk1.Key);
            Assert.AreEqual("payload", payload);
        }
        [TestMethod]
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
            Assert.AreEqual("payload", payload);
            string jws2 = JWT.Encode("payload", jwk2.Key, JwsAlgorithm.ES256);
            payload = JWT.Decode(jws2, jwk1.Key);
            Assert.AreEqual("payload", payload);
        }
        [TestMethod]
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
            Assert.AreEqual(2, list.Count);
        }
    }
}
