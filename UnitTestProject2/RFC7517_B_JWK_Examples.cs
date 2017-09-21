using Jose.jwk;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject2.util;
using Jose.jwk.util;
using Xunit;

namespace UnitTestProject2
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc7517#appendix-B
    /// </summary>
    public class RFC7517_B_JWK_Examples
    {
        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public void B()
        {
            var json = this.GetResource("RFC7517_B.json");
            var jwks = JWKS.Parse(json).ToList();

            Assert.Equal(1, jwks.Count);

            Assert.IsAssignableFrom<RSA>(jwks[0].Key);
            Assert.False(jwks[0].IsEncryption);
            Assert.True(jwks[0].IsSignature);
            Assert.Equal("1b94c", jwks[0].Id);

            Assert.IsAssignableFrom<IList>(jwks[0].Header["x5c"]);

            IList x5c = jwks[0].Header["x5c"] as IList;
            var list = x5c.OfType<string>()
                .Select(t => Convert.FromBase64String(t))
                .Select(t => new X509Certificate2(t))
                .ToList();

            Assert.Equal(1, list.Count);

            Assert.IsAssignableFrom<X509Certificate2>(list[0]);
            Assert.IsAssignableFrom<RSA>(list[0].GetRSAPublicKey());

            Assert.Equal(
                (jwks[0].Key as RSA).ExportParameters(false).Modulus,
                list[0].GetRSAPublicKey().ExportParameters(false).Modulus);

        }
    }
}
