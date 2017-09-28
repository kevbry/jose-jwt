using Jose;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace UnitTestProject2
{
    public class PlatformTest
    {
        [Fact]
        public void RsaTest()
        {
            JweAlgorithm[] algorithms = {
                JweAlgorithm.RSA1_5,
                JweAlgorithm.RSA_OAEP,
                //JweAlgorithm.RSA_OAEP_256
            };
            JweEncryption[] content =
            {
                JweEncryption.A256CBC_HS512,
                JweEncryption.A192CBC_HS384,
                JweEncryption.A128CBC_HS256,
                //JweEncryption.A128GCM,
            };
            using (var key = RSA.Create(2048))
            {
                foreach (JweAlgorithm alg in algorithms)
                {
                    foreach (JweEncryption enc in content)
                    {
                        try
                        {
                            string jws = JWT.Encode("hello world", key, JwsAlgorithm.RS256);
                            string jwe = JWT.Encode(jws, key, alg, enc);
                            Assert.NotNull(jwe);
                        } catch(Exception e)
                        {
                            throw new ApplicationException("alg=" + alg + ",enc=" + enc, e);
                        }
                    }
                }
            }
        }
    }
}
