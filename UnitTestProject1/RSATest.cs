using Jose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jose.jwk;
using System.Security.Cryptography;

namespace UnitTestProject1
{
    [TestClass]
    public class RSATest
    {
        [TestMethod()]
        [ExpectedException(typeof(CryptographicException))]
        public void ImportFails()
        {
            JWK jwk = new JWK()
            {
                Key = JsonWebKeyTest.CreateRSA()
            };

            JWT.Encode("payload", jwk.Key, JwsAlgorithm.RS256);

            var map = JwkFactory.Factory
                .JwkAlgorithmFromKey(jwk.Key)
                .Serialize(jwk, true);
            map.Remove("p");
            map.Remove("q");
            map.Remove("dp");
            map.Remove("dq");
            map.Remove("qi");
            string json = JwkFactory.Factory.GetSettings(null)
                .JsonMapper
                .Serialize(map);
            Console.WriteLine(json);
            jwk = JWK.Parse(json);

            // cannot import RSA private key with only n, e and d
            JWT.Encode("payload", jwk.Key, JwsAlgorithm.RS256);
        }
    }
}
