using Jose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject1.jwk;

namespace UnitTestProject1
{
    [TestClass]
    public class RSATest
    {
        [TestMethod]
        public void ExportImport()
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

            JWT.Encode("payload", jwk.Key, JwsAlgorithm.RS256);
        }
    }
}
