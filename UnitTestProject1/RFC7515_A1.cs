using Jose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestProject1.jwk;

namespace UnitTestProject1
{
    [TestClass]
    public class RFC7515_A1
    {
        [TestMethod]
        public void A1()
        {
            const string KEY = @"
                {""kty"":""oct"",
                ""k"":""AyM1SysPpbyDfgZld3umj1qzKObwVMkoqQ-EstJQLr_T-1qS0gZH75
                        aKtMN3Yj0iPS4hcgUuTwjAzZr1Z9CAow""
                }";
            const string TOKEN = ""
                + "eyJ0eXAiOiJKV1QiLA0KICJhbGciOiJIUzI1NiJ9"
                + "."
                + "eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFt"
                + "cGxlLmNvbS9pc19yb290Ijp0cnVlfQ"
                + "."
                + "dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk";
            JWK jwk = JWK.Parse(KEY);
            Assert.IsNotNull(jwk);
            string s = JWT.Decode(TOKEN, jwk.Key);
            Assert.IsNotNull(s);
            Console.WriteLine(s);
        }
    }
}
