using Jose.jwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject2.util;
using Xunit;

namespace UnitTestProject2
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc7517#appendix-A
    /// </summary>
    public class RFC7517_A_JWK_Examples
    {
        /// <summary>
        /// https://tools.ietf.org/html/rfc7517#appendix-A.1
        /// </summary>
        [Fact]
        public void A1()
        {
            var json = this.GetResource("RFC7517_A1.json");
            var jwks = JWKS.Parse(json).ToList();

            Assert.Equal(2, jwks.Count);

            Assert.IsAssignableFrom<ECDsa>(jwks[0].Key);
            Assert.True(jwks[0].IsEncryption);
            Assert.False(jwks[0].IsSignature);
            Assert.Equal("1", jwks[0].Id);

            Assert.IsAssignableFrom<RSA>(jwks[1].Key);
            Assert.True(jwks[1].IsEncryption);
            Assert.True(jwks[1].IsSignature);
            Assert.Equal("RS256", jwks[1].Header["alg"]);
            Assert.Equal("2011-04-29", jwks[1].Id);
        }
        /// <summary>
        /// https://tools.ietf.org/html/rfc7517#appendix-A.2
        /// </summary>
        [Fact]
        public void A2()
        {
            var json = this.GetResource("RFC7517_A2.json");
            var jwks = JWKS.Parse(json).ToList();

            Assert.Equal(2, jwks.Count);

            Assert.IsAssignableFrom<ECDsa>(jwks[0].Key);
            Assert.True(jwks[0].IsEncryption);
            Assert.False(jwks[0].IsSignature);
            Assert.Equal("1", jwks[0].Id);

            Assert.IsAssignableFrom<RSA>(jwks[1].Key);
            Assert.True(jwks[1].IsEncryption);
            Assert.True(jwks[1].IsSignature);
            Assert.Equal("RS256", jwks[1].Header["alg"]);
            Assert.Equal("2011-04-29", jwks[1].Id);
        }
        /// <summary>
        /// https://tools.ietf.org/html/rfc7517#appendix-A.3
        /// </summary>
        [Fact]
        public void A3()
        {
            var json = this.GetResource("RFC7517_A3.json");
            var jwks = JWKS.Parse(json).ToList();

            Assert.Equal(2, jwks.Count);

            Assert.IsAssignableFrom<byte[]>(jwks[0].Key);
            Assert.True(jwks[0].IsEncryption);
            Assert.True(jwks[0].IsSignature);

            Assert.IsAssignableFrom<byte[]>(jwks[1].Key);
            Assert.True(jwks[1].IsEncryption);
            Assert.True(jwks[1].IsSignature);
            Assert.Equal("HMAC key used in JWS spec Appendix A.1 example", jwks[1].Id);
        }
    }
}
