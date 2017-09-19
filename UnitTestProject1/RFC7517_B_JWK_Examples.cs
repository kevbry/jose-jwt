using Jose.jwk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject1.util;
using Jose.jwk.util;

namespace UnitTestProject1
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc7517#appendix-B
    /// </summary>
    [TestClass]
    public class RFC7517_B_JWK_Examples
    {
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void B()
        {
            var json = this.GetResource("RFC7517_B.json");
            var jwks = JWKS.Parse(json).ToList();

            Assert.AreEqual(1, jwks.Count);

            Assert.IsInstanceOfType(jwks[0].Key, typeof(RSA));
            Assert.IsFalse(jwks[0].IsEncryption);
            Assert.IsTrue(jwks[0].IsSignature);
            Assert.AreEqual("1b94c", jwks[0].Id);

            Assert.IsTrue(jwks[0].Header.TryGet<IList>("x5c", out IList x5c));

            var list = x5c.OfType<string>()
                .Select(t => Convert.FromBase64String(t))
                .Select(t => new X509Certificate2(t))
                .ToList();

            Assert.AreEqual(1, list.Count);

            Assert.IsInstanceOfType(list[0], typeof(X509Certificate2));
            Assert.IsInstanceOfType(list[0].GetRSAPublicKey(), typeof(RSA));

            Assert.AreEqual(
                (jwks[0].Key as RSA).ToXmlString(false),
                list[0].GetRSAPublicKey().ToXmlString(false));

        }
    }
}
