using Jose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestProject1.jwk;

namespace UnitTestProject1
{
    [TestClass]
    public class RFC7515_A3
    {
        [TestMethod]
        public void A3()
        {
            const string KEY = @"
                {""kty"":""EC"",
                 ""crv"":""P-256"",
                 ""x"":""f83OJ3D2xF1Bg8vub9tLe1gHMzV76e8Tus9uPHvRVEU"",
                 ""y"":""x_FEzRu9m36HLN_tue659LNpXW6pCyStikYjKIWI5a0"",
                 ""d"":""jpsQnnGQmL-YBIffH1136cspYG6-0iY7X1fCE9-E9LI""
                }";
            const string TOKEN = ""
                + "eyJhbGciOiJFUzI1NiJ9"
                + "."
                + "eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFt"
                + "cGxlLmNvbS9pc19yb290Ijp0cnVlfQ"
                + "."
                + "DtEhU3ljbEg8L38VWAfUAqOyKAM6-Xx-F4GawxaepmXFCgfTjDxw5djxLa8ISlSA"
                + "pmWQxfKTUJqPP3-Kg6NU1Q";
            JWK jwk = JWK.Parse(KEY);
            string s = JWT.Decode(TOKEN, jwk.Key);
            Assert.IsNotNull(s);
            Console.WriteLine(s);
        }
    }
}
