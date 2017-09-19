using Jose.jwk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject1.util;

namespace UnitTestProject1
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc7517#appendix-A
    /// </summary>
    [TestClass]
    public class RFC7517_A_JWK_Examples
    {
        /// <summary>
        /// https://tools.ietf.org/html/rfc7517#appendix-A.1
        /// </summary>
        [TestMethod]
        public void A1()
        {
            var json = this.GetResource("RFC7517_A1.json");
            var jwks = JWKS.Parse(json).ToList();

            Assert.AreEqual(2, jwks.Count);

            Assert.IsInstanceOfType(jwks[0].Key, typeof(ECDsa));
            Assert.IsTrue(jwks[0].IsEncryption);
            Assert.IsFalse(jwks[0].IsSignature);
            Assert.AreEqual("1", jwks[0].Id);

            Assert.IsInstanceOfType(jwks[1].Key, typeof(RSA));
            Assert.IsTrue(jwks[1].IsEncryption);
            Assert.IsTrue(jwks[1].IsSignature);
            Assert.AreEqual("RS256", jwks[1].Header["alg"]);
            Assert.AreEqual("2011-04-29", jwks[1].Id);
        }
        /// <summary>
        /// https://tools.ietf.org/html/rfc7517#appendix-A.2
        /// </summary>
        [TestMethod]
        public void A2()
        {
            var json = this.GetResource("RFC7517_A2.json");
            var jwks = JWKS.Parse(json).ToList();

            Assert.AreEqual(2, jwks.Count);

            Assert.IsInstanceOfType(jwks[0].Key, typeof(ECDsa));
            Assert.IsTrue(jwks[0].IsEncryption);
            Assert.IsFalse(jwks[0].IsSignature);
            Assert.AreEqual("1", jwks[0].Id);

            Assert.IsInstanceOfType(jwks[1].Key, typeof(RSA));
            Assert.IsTrue(jwks[1].IsEncryption);
            Assert.IsTrue(jwks[1].IsSignature);
            Assert.AreEqual("RS256", jwks[1].Header["alg"]);
            Assert.AreEqual("2011-04-29", jwks[1].Id);
        }
        /// <summary>
        /// https://tools.ietf.org/html/rfc7517#appendix-A.3
        /// </summary>
        [TestMethod]
        public void A3()
        {
            var json = this.GetResource("RFC7517_A3.json");
            var jwks = JWKS.Parse(json).ToList();

            Assert.AreEqual(2, jwks.Count);

            Assert.IsInstanceOfType(jwks[0].Key, typeof(byte[]));
            Assert.IsTrue(jwks[0].IsEncryption);
            Assert.IsTrue(jwks[0].IsSignature);

            Assert.IsInstanceOfType(jwks[1].Key, typeof(byte[]));
            Assert.IsTrue(jwks[1].IsEncryption);
            Assert.IsTrue(jwks[1].IsSignature);
            Assert.AreEqual("HMAC key used in JWS spec Appendix A.1 example", jwks[1].Id);
        }
    }
}
